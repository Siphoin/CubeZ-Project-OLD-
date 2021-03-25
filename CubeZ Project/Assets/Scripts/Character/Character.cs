using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterStatsController))]
public class Character : MonoBehaviour, IAnimatiomStateController, ICheckerStats, IInvokerMono, IGeterHit, ICharacter
{
    private Animator animator;
    private CharacterAnimatorObserver animatorObserver;
    private Rigidbody _rb;
    private CharacterData characterData;

    private WeaponItem currentWeapon = null;


    private const string VERTICAL_INPUT_NAME = "Vertical";
    private const string HORIZONTAL_INPUT_NAME = "Horizontal";
    private const string PATH_CHARACTER_SETTINGS = "Character/CharacterSettings";
    private const string ZOMBIE_TAG = "Zombie";
    private const string ATTACK_NAME_ANIM = "AttackGeneric";
    private const string DEAD_PLAYER_TAG = "DeadPlayer";
    private const string DEAD_ANIM_NAME = "Death";
    private const string KEY_CODE_OFF_SLEEP_NAME = "offSleep";

    private TypeAnimation animationState = TypeAnimation.Idle2;

    [SerializeField, ReadOnlyField] CharacterDataSettings characterDataSettings;

    public CharacterData CharacterStats { get => characterData; }

    private CharacterStatsDataNeed healthStats;
    private CharacterStatsDataNeed runStats;

    private int baseDamage = 6;
    private int currentDamage;

    private bool characterActive = true;

    public event Action<ItemBaseData> onWeaponChanged;

    public event Action onDead;

    public event Action<bool> onSleep;


    private bool isDead = false;

    private bool isFrezzed = false;

    private bool isSleeping = false;

    private Vector3 lastPosition;
    private Quaternion lastQuaternion;

    public float Speed { get => characterData.speed; }

    public int Health { get => healthStats.value; }
    public bool IsDead { get => isDead; }
    public WeaponItem CurrentWeapon { get => currentWeapon; }
    public bool IsSleeping { get => isSleeping; }




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



        baseDamage = characterData.damage;
        ReturnToBaseDamage();

    }
    void Start()
    {

#if UNITY_EDITOR
        CheckValidStats();

#endif
        if (ControlManagerObject.Manager == null)
        {
            throw new CharacterException("Control manager object not found");
        }

        if (ControlManagerObject.Manager.ControlManager == null)
        {
            throw new CharacterException("Control manager not found");
        }


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
            LookAtDirection();
        }
        else
        {
            _rb.velocity = Vector3.zero;

        }
        ActionsCharactersCheck();

        if (isSleeping)
        {
            if (Input.GetKeyDown(ControlManagerObject.Manager.ControlManager.GetKeyCodeByFragment(KEY_CODE_OFF_SLEEP_NAME)))
            {
                Awakening();
            }
        }

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

    private void LookAtDirection()
    {
        Vector3 dir = new Vector3(Input.GetAxis(HORIZONTAL_INPUT_NAME), 0.0f, Input.GetAxis(VERTICAL_INPUT_NAME)) * characterData.rotateSpeed * Time.deltaTime;

        if (dir != Vector3.zero)
        {
            _rb.MoveRotation(Quaternion.LookRotation(dir));
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

    public void IncrementDamage(int value)
    {
        ReturnToBaseDamage();
        if (value < 0)
        {
            throw new CharacterException("invalid value damage");
        }
        currentDamage += value;
    }

    public void ReturnToBaseDamage()
    {
        currentDamage = baseDamage;
    }

    public bool CharacterUseTheWeapon(WeaponItem weapon)
    {
        CheckWeaponisNull(weapon);
        if (!currentWeapon)
        {
            return false;
        }
        return currentWeapon.data.id == weapon.data.id;
    }

    private static void CheckWeaponisNull(WeaponItem weapon)
    {
        if (!weapon)
        {
            throw new CharacterException("Weapon argument is null");
        }
    }

    public bool CharacterUseWeapon()
    {
        return currentWeapon != null;
    }

    public void SetWeapon(WeaponItem weapon)
    {  
        currentWeapon = weapon;
        onWeaponChanged?.Invoke(currentWeapon == null ? null : currentWeapon.data);
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

    public void Damage()
    {

        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, 1))
        {
            if (raycastHit.collider.tag == ZOMBIE_TAG)
            {
                BaseZombie target = raycastHit.collider.GetComponent<BaseZombie>();
                target.Hit(currentDamage);
                if (CharacterUseWeapon())
                {

                    currentWeapon.dataWeapon.strength -= 1;
                //    Debug.Log(currentWeapon.dataWeapon.strength);

                    if (currentWeapon.dataWeapon.strength <= 0)
                    {
                        GameCacheManager.gameCache.inventory.Remove(currentWeapon.data);
                        SetWeapon(null);
                        ReturnToBaseDamage();
                        
                    }
                }
            }
        }


    }


    public void Hit(int hitValue, bool playHitAnim = true)
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
            if (isSleeping)
            {
                Awakening();
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
        onDead?.Invoke();
        animator.Play(DEAD_ANIM_NAME);
        _rb.isKinematic = true;
        gameObject.AddComponent<CharacterRebel>();
        enabled = false;
    }

    public bool CharacterIsStand()
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

    public void CallInvokingEveryMethod(Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    public void CallInvokingMethod(Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }

    private void SetStateFrezze(bool state)
    {
        isFrezzed = state;
    }

    public void ActivateCharacter()
    {
        SetStateFrezze(false);
    }

    public void FrezzeCharacter()
    {
        SetAnimationState(TypeAnimation.Idle2);
        SetStateFrezze(true);
    }

    #region Sleep System
    public void Sleep(Bed bedTarget)
    {
        if (bedTarget == null)
        {
            throw new CharacterException("bed target is null");
        }
        CachingLastTransform();
        SetSleepStatus(true);
        SetNewTransform(bedTarget.PointSleep, bedTarget.QuaternionSleep);
        SetAnimationState(TypeAnimation.Idle2);
    }

    private void SetNewTransform(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    private void CachingLastTransform ()
    {
        lastPosition = transform.position;
        lastQuaternion = transform.rotation;
        Debug.Log($"Player transform cached. Position: {lastPosition} Rotation: {lastQuaternion}");
    }

   public void Awakening()
    {
        SetSleepStatus(false);
        SetNewTransform(lastPosition, lastQuaternion);
    }

    private void SetSleepStatus (bool status)
    {
        isSleeping = status;
        onSleep?.Invoke(isSleeping);
        SetStateFrezze(isSleeping);
        characterActive = !status;
        WorldManager.Manager.NewTimeScale(status == true ? 4 : 1);
    }
    #endregion
}
