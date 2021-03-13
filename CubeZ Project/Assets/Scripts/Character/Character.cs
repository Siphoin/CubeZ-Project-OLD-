using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterStatsController))]
public class Character : MonoBehaviour, IAnimatiomStateController, ICheckerStats, IInvokerMono, IGeterHit, ICharacter
{
    private Animator animator;
    private CharacterAnimatorObserver animatorObserver;
    private Rigidbody _rb;
    private CharacterData characterData;



    private const string  VERTICAL_INPUT_NAME = "Vertical";
    private const string HORIZONTAL_INPUT_NAME = "Horizontal";
    private const string PATH_CHARACTER_SETTINGS = "Character/CharacterSettings";
    private const string ZOMBIE_TAG = "Zombie";
    private const string ATTACK_NAME_ANIM = "AttackGeneric";
    private const string DEAD_PLAYER_TAG = "DeadPlayer";
    private const string DEAD_ANIM_NAME = "Death";


    private TypeAnimation animationState = TypeAnimation.Idle2;

    [SerializeField, ReadOnlyField] CharacterDataSettings characterDataSettings;

    public CharacterData CharacterStats { get => characterData; }

    private CharacterStatsDataNeed healthStats;
    private CharacterStatsDataNeed runStats;

    private bool characterActive = true;

    public float Speed { get => characterData.speed; }

    public int Health { get => healthStats.value; }
    public bool IsDead { get => isDead; }

    private bool isDead = false;

    private bool isFrezzed = false;




    // Start is called before the first frame update

    private void Awake()
    {
        characterDataSettings = Resources.Load<CharacterDataSettings>(PATH_CHARACTER_SETTINGS);
        characterData = new CharacterData(characterDataSettings.GetData());
        healthStats = characterData.GetDictonaryNeeds()[NeedCharacterType.Health];
        runStats = characterData.GetDictonaryNeeds()[NeedCharacterType.Run];
        if (characterDataSettings == null)
        {
            throw new CharacterException("character settings is null");
        }
        if (characterData.speed <= 0)
        {
            throw new CharacterException("Speed <= 0");
        }

    }
    void Start()
    {

#if UNITY_EDITOR
        CheckValidStats();

#endif
        animator = transform.GetChild(0).GetComponent<Animator>();

        animatorObserver = transform.GetChild(0).GetComponent<CharacterAnimatorObserver>();
        _rb = GetComponent<Rigidbody>();

        animatorObserver.onAttackEvent += Damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        if (characterActive && !isFrezzed)
        {
            LookAtMouse();
        }
       else
        {
            _rb.velocity = Vector3.zero;

        }
        ActionsCharactersCheck();

    }

    private void ActionsCharactersCheck()
    {
        StandLockInput();
    }

    private void StandLockInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            characterActive = !characterActive;
            if (characterActive == false)
            {
                SetAnimationState(TypeAnimation.SitandLook);
            }

        }
    }

    private void LookAtMouse()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);

            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, characterData.rotateSpeed * Time.deltaTime);

        }
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }
        if (characterActive && !isFrezzed)
        {
        Control();
        }

    }

    private void Control()
    {


        float speed = 0;

        if (!CharacterIsStand())
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                speed = Walk();
            }

            else
            {
                if (runStats.value > 0)
                {
                speed = Run();
                }

                else
                {

                    SetAnimationState(TypeAnimation.Idle2);
                }

            }
        }


        else
        {

            SetAnimationState(TypeAnimation.Idle2);

            if (Input.GetMouseButtonDown(0))
            {
                int varintsAttack = Random.Range(1, 4);

                string animName = $"{ATTACK_NAME_ANIM}{varintsAttack}";
                SetAnimationState(animName.ToEnum<TypeAnimation>());
            }
        }



        Vector3 tempVect = transform.forward;
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        _rb.MovePosition(transform.position + tempVect);
    }

    private float Walk()
    {
        float speed = characterData.speed;
        SetAnimationState(TypeAnimation.Walk);
        return speed;
    }

    private float Run()
    {
        float speed = characterData.speed + 1;
        SetAnimationState(TypeAnimation.Run);
        return speed;
    }

    public void SetAnimationState(TypeAnimation type)
    {
        if (type != animationState)
        {
            for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
            {
                animator.SetBool(animator.runtimeAnimatorController.animationClips[i].name, false);
            }
            animator.SetBool(type.ToString(), true);
            animationState = type;
        }
    }

    public void CheckValidStats()
    {
        foreach (var prop in characterData.GetType().GetFields())
        {
            try
            {
                ValidatorData.CheckValidFieldStats(prop, characterData);
            }
            catch (ValidatorDataException)
            {

                throw;
            }
        }

        foreach (var prop in characterData.GetType().GetProperties())
        {
            try
            {
                ValidatorData.CheckValidPropertyStats(prop, characterData);
            }
            catch (ValidatorDataException)
            {

                throw;
            }
        }
    }

    public void Damage ()
    {
        
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, 1))
        {
            if (raycastHit.collider.tag == ZOMBIE_TAG)
            {
                BaseZombie target = raycastHit.collider.GetComponent<BaseZombie>();
                target.Hit(characterData.damage);
            }
        }


            
        }
    

    public void Hit (int hitValue, bool playHitAnim = true)
    {
        if (healthStats.value - hitValue > 0)
        {
            healthStats.value -= hitValue;
            if (playHitAnim)
            {
                if (Random.Range(0, 10) > 7)
                {
                    SetAnimationState(TypeAnimation.GetHit);


                    characterActive = true;
                }
            }

        }

        else
        {
            isDead = true;
            healthStats.value = 0;
            tag = DEAD_PLAYER_TAG;
            animator.SetBool(DEAD_ANIM_NAME, true);
            characterActive = false;
            Dead();
        }



        healthStats.CallOnValueChanged();
    }

    private void Dead()
    {

        animator.Play("Death");
        _rb.isKinematic = true;
        enabled = false;
    }

    public bool CharacterIsStand ()
    {
        float _Hor = Input.GetAxis(HORIZONTAL_INPUT_NAME);
        float _Ver = Input.GetAxis(VERTICAL_INPUT_NAME);

        Vector3 mov = new Vector3(_Hor, 0, _Ver);

        return mov == Vector3.zero;
    }

    public bool IsRunning()
    {
        return animationState == TypeAnimation.Run;
    }

    public void CallInvokingEveryMethod(System.Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    public void CallInvokingMethod(System.Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }

    private void SetStateFrezze (bool state)
    {
        isFrezzed = state;
    }

    public void ActivateCharacter ()
    {
        SetStateFrezze(false);
    }

    public void FrezzeCharacter()
    {
        SetStateFrezze(true);
    }
}
