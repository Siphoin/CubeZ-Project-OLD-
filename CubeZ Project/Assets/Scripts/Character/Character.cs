using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    BoxCollider boxCollider;

    private WeaponItem currentWeapon = null;

    private AudioObject walkAudioObject = null;

    private AudioObject getItemAudioObject = null;

    private AudioDataManager audioManager = null;

    private Dictionary<string, AudioClip> clipsCharacter = new Dictionary<string, AudioClip>();


    private const string VERTICAL_INPUT_NAME = "Vertical";
    private const string HORIZONTAL_INPUT_NAME = "Horizontal";
    private const string PATH_CHARACTER_SETTINGS = "Character/CharacterSettings";
    private const string ATTACK_NAME_ANIM = "AttackGeneric";
    private const string DEAD_PLAYER_TAG = "DeadPlayer";

    private const string DEAD_ANIM_NAME = "Death";
    private const string NAME_ANIM_DEATH_CHARACTER = "character_death";
    private const string KEY_CODE_OFF_SLEEP_NAME = "offSleep";


    private const string TAG_TREE = "Tree";
    private const string TAG_INTERACTION_OBJECT = "InteractionObject";
    private const string TAG_DOOR = "Door";
    private const string ZOMBIE_TAG = "Zombie";
    private const string ZOMBIE_AREA_TAG = "ZombieArea";
    private const string FIRE_TAG = "FireArea";


    private const string NAME_KEY_CODE_SIT_DOWN = "SitdownCharacter";

    private const string NAME_FOLBER_AUDIO_CHARACTER = "Audio/character";

    private const string AXE_ID_WEAPON = "axe";

    private const float DISTANCE_FOR_ATTACK = 0.7f;
    private TypeAnimation animationState = TypeAnimation.Idle2;

    [SerializeField, ReadOnlyField] CharacterDataSettings characterDataSettings;
    [Header("Тело персонажа")]
    [SerializeField] Transform skinCharacter;
    [Header("Триггер персонажа")]
    [SerializeField] CharacterTrigger characterTrigger;

  private  ControlManager controlManager;

    private UIController UIControl;

    private CharacterStatsDataNeed healthStats;
    private CharacterStatsDataNeed runStats;
    private CharacterStatsDataNeed sleepStats;
    private int baseDamage = 6;
    private int currentDamage;

    private float currentSpeed;


    public event Action<ItemBaseData> onWeaponChanged;

    public event Action onDead;

    public event Action onDamage;

    public event Action<bool> onSleep;

    public event Action<bool> onAdrenalin;


    private bool isDead = false;

    private bool isFrezzed = false;

    private bool isSleeping = false;

    private bool isFatigue = false;

    private bool isAdrenalin = false;

    private bool collisizedInteractionObject = false;

    private bool inFireArea = false;

    private bool characterActive = true;


    private Vector3 lastPosition;

    private Quaternion lastQuaternion;
    private Quaternion startQuuaterion;

    private float defaultRaduisCharacterTrigger;

    public float Speed { get => characterData.speed; }

    public int Health { get => healthStats.value; }


    public WeaponItem CurrentWeapon { get => currentWeapon; }
    public CharacterData CharacterStats { get => characterData; }


    public bool IsSleeping { get => isSleeping; }
    public bool CollisizedInteractionObject { get => collisizedInteractionObject; }
    public bool InFireArea { get => inFireArea; }
    public bool IsDead { get => isDead; }

    public bool IsAdrenalin { get => isAdrenalin; }

    public Bounds BoundsColider { get => boxCollider.bounds; }






    // Start is called before the first frame update

    private void Awake()
    {
        Ini();

    }

    private void Ini()
    {


        

        if (skinCharacter == null)
        {
            throw new CharacterException("Character skin not seted");
        }



        startQuuaterion = skinCharacter.transform.localRotation;
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


        if (!TryGetComponent(out boxCollider))
        {
            throw new CharacterException("Box Colider component not found on Character");
        }



        baseDamage = characterData.damage;
        currentSpeed = characterData.speed;

        ReturnToBaseDamage();

        characterTrigger.onEnter += CharacterEnterOnTrigger;
        characterTrigger.onExit += CharacterExitOnTrigger;
        defaultRaduisCharacterTrigger = characterTrigger.GetRadius();


        LoadAudio();



    }


    private void CharacterEnterOnTrigger(string tag)
    {
        if (tag == this.tag)
        {
            return;
        }
        if (tag == FIRE_TAG)
        {
            inFireArea  = true;
        }
        
    }

    private void CharacterExitOnTrigger(string tag)
    {
        if (tag == this.tag)
        {
            return;
        }
        if (tag == FIRE_TAG)
        {
            inFireArea = false;
        }
    }

    void Start()
    {

#if UNITY_EDITOR
        CheckValidStats();

#endif

        if (UIController.Manager == null)
        {
            throw new CharacterException("UI Controller not found");
        }
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

        if (AudioDataManager.Manager == null)
        {
            throw new CharacterException("audio manager not found");
        }

        audioManager = AudioDataManager.Manager;

        UIControl = UIController.Manager;


        CreateAudioObjectWalk();

        IniKeyCodes();

        GameCacheManager.gameCache.inventory.onItemOfTypeAdded += PlaySoundGetItem;
        GameCacheManager.gameCache.inventory.onItemNoAdded += PlaySoundNoGetItem;
    }


    private void IniKeyCodes()
    {
        if (ControlManagerObject.Manager == null)
        {
            throw new CharacterException("control manager not found");
        }

        controlManager = ControlManagerObject.Manager.ControlManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }



        ActionsCharactersCheck();

        if (isSleeping)
        {
            if (ControlManagerObject.Manager != null && ControlManagerObject.Manager.ControlManager != null)
            {
            if (Input.GetKeyDown(ControlManagerObject.Manager.ControlManager.GetKeyCodeByFragment(KEY_CODE_OFF_SLEEP_NAME)))
            {
                Awakening();
            }
            }

        }



    }

    

    private void ActionsCharactersCheck()
    {
        StandLockInput();
    }

    private void SetCharacterMoveOppositive ()
    {
        characterActive = !characterActive;
    }

    private void StandLockInput()
    {
        if (Input.anyKeyDown)
        {


        if (Input.GetKeyDown(controlManager.GetKeyCodeByFragment(NAME_KEY_CODE_SIT_DOWN)))
        {
                SetCharacterMoveOppositive();            
                SetAnimationState(characterActive == false ? TypeAnimation.SitandLook : TypeAnimation.Idle2);
        }
        }

    }

    private void LookAtDirection()
    {
        Vector3 dir = new Vector3(Input.GetAxis(HORIZONTAL_INPUT_NAME), 0.0f, Input.GetAxis(VERTICAL_INPUT_NAME));

        if (dir != Vector3.zero)
        {
            Quaternion root = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, root, characterData.rotateSpeed * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }
        if (characterActive && !isFrezzed && CanMove())
        {
            LookAtDirection();
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
                PlayWalkSound();
                speed = Walk();
                characterTrigger.SetRadius(defaultRaduisCharacterTrigger);
            }

            else
            {
                if (runStats.value > 0)
                {
                    speed = Run();
                    characterTrigger.SetRadius(defaultRaduisCharacterTrigger * 2);
                }

                else
                {
                    characterTrigger.SetRadius(defaultRaduisCharacterTrigger);
                    SetAnimationState(TypeAnimation.Idle2);
                }

            }
        }




        else
        {
            StopWalkSound();
            SetAnimationState(TypeAnimation.Idle2);

            if (Input.GetMouseButtonDown(0))
            {
                int varintsAttack = Random.Range(1, 4);

                string animName = $"{ATTACK_NAME_ANIM}{varintsAttack}";
                SetAnimationState(animName.ToEnum<TypeAnimation>());
            }
        }


        if (!CharacterAttackingEnabled())
        {
        Vector3 tempVect = transform.forward;
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        _rb.MovePosition(transform.position + tempVect);
        }

    }

    private float Walk()
    {
        float speed =  isFatigue == false ? currentSpeed : currentSpeed - 1;
        SetAnimationState(TypeAnimation.Walk);
        PlayWalkSound();
        characterTrigger.SetRadius(defaultRaduisCharacterTrigger * 2);
        return speed;
    }

    private float Run()
    {
        
        float speed = isFatigue == false ?  currentSpeed + 1 : currentSpeed - 1;
        SetAnimationState(isFatigue == false ? TypeAnimation.Run : TypeAnimation.Walk);
        PlayWalkSound();
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

    public void IncrementBaseDamage(int value)
    {
        if (value < 0)
        {
            throw new CharacterException("invalid value damage");
        }
        currentDamage += value;
    }


    public void DecrementBaseDamage (int value)
    {
        if (value < 0)
        {
            throw new CharacterException("invalid value damage");
        }
        currentDamage -= value;
    }

    public void ReturnToBaseDamage()
    {
        currentDamage = baseDamage;
    }

    public void IncrementBaseSpeed(float value)
    {
        if (value < 0)
        {
            throw new CharacterException("invalid value damage");
        }
        currentSpeed += value;
    }

    public void DecrementBaseSpeed(float value)
    {
        if (value < 0)
        {
            throw new CharacterException("invalid value damage");
        }
        currentSpeed -= value;
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
#region Attack System


    public void Damage()
    {

        RaycastHit raycastHit;

        
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, DISTANCE_FOR_ATTACK))
        {
            if (raycastHit.collider.tag == ZOMBIE_AREA_TAG)
            {
                BaseZombie targetZombie = null;

                if (!raycastHit.collider.transform.parent.TryGetComponent(out targetZombie))
                {
                    throw new CharacterException("parent zombie area component BaseZombie not found");
                }

                DamageZombie(targetZombie);
                return;
            }


            if (raycastHit.collider.tag == ZOMBIE_TAG)
            {
                DamageZombie(raycastHit);
            }


            if (raycastHit.collider.tag == TAG_TREE)
            {
                if (CanDamageTree())
                {
                DamageTree(raycastHit);
                }

                else
                {
                    PlayFXPlayer("character_get_item_limit");
                }

            }

            if (raycastHit.collider.tag == TAG_DOOR)
            {
                DamageDoor(raycastHit);
            }

        }


    }

    private void DamageZombie(RaycastHit raycastHit)
    {
        BaseZombie target = raycastHit.collider.GetComponent<BaseZombie>();
        target.Hit(currentDamage);

        if (target.CurrentHealth <= 0 && PlayerManager.Manager.Player == this)
        {
            GameCacheManager.gameCache.zombieKils++;
        }
        CheckWearWeapon();
    }

    private void DamageZombie(BaseZombie target)
    {
        target.Hit(currentDamage);


        if (target.CurrentHealth <= 0)
        {
            GameCacheManager.gameCache.zombieKils++;
        }
        CheckWearWeapon();
    }

    private void DamageTree(RaycastHit raycastHit)
    {
        Tree target = raycastHit.collider.GetComponent<Tree>();
        target.Hit(currentDamage);
        CheckWearWeapon();
    }

    private void DamageDoor(RaycastHit raycastHit)
    {
        Door target = raycastHit.collider.GetComponent<Door>();
        target.Hit(currentDamage);
        CheckWearWeapon();
    }

    private void CheckWearWeapon()
    {
        if (CharacterUseWeapon())
        {

            currentWeapon.dataWeapon.strength -= 1;

            if (currentWeapon.dataWeapon.strength <= 0)
            {
                GameCacheManager.gameCache.inventory.Remove(currentWeapon.data);
                SetWeapon(null);
                ReturnToBaseDamage();

            }
        }
    }


    private bool CanDamageTree ()
    {
        if (currentWeapon == null)
        {
            return false;
        }

        return currentWeapon.data.idItem.Contains(AXE_ID_WEAPON);
    }

    #endregion

    public void Hit(int hitValue, bool playHitAnim = true)
    {
        healthStats.value = Mathf.Clamp(healthStats.value - hitValue, 0, 100);


        if (!isAdrenalin)
        {
            PlayFXPlayer("character_damage");



            if (playHitAnim)
            {
                if (Random.Range(0, 10) > 7)
                {
                    SetAnimationState(TypeAnimation.GetHit);

                    characterActive = true;
                }
            }
        }
        if (isSleeping)
        {
            Awakening();
            skinCharacter.transform.localRotation = startQuuaterion;

        }


        if (hitValue > 0)
        {
            onDamage?.Invoke();
        }




        if (healthStats.value <= 0)
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
        if (isAdrenalin)
        {
            SetStateAdrenalin(false);
        }
        PlayFXPlayer(NAME_ANIM_DEATH_CHARACTER);
        onDead?.Invoke();
        animator.Play(DEAD_ANIM_NAME);
        _rb.isKinematic = true;
        boxCollider.enabled = false;
        gameObject.AddComponent<CharacterRebel>();
        enabled = false;
    }

    private bool CanMove ()
    {
        TypeAnimation[] typesAnimations = new TypeAnimation[]
        {
            TypeAnimation.AttackGeneric1,
            TypeAnimation.AttackGeneric2,
            TypeAnimation.AttackGeneric3,
            TypeAnimation.SitandLook,
            TypeAnimation.Gethit,
        };

        return !typesAnimations.Any(t => t.ToString() == animator.GetCurrentClipPlayed().name);
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
        _rb.isKinematic = true;
        CachingLastTransform();
        SetSleepStatus(true);
       SetNewTransform(bedTarget.PointSleep, bedTarget.QuaternionSleep);
        SetAnimationState(TypeAnimation.Idle2);

        UIControl.On = false;
        UIControl.CloseAllWindows();

       

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
        _rb.isKinematic = false;
        SetSleepStatus(false);
        SetNewTransform(lastPosition, startQuuaterion);
        UIControl.On = true;
    }

    private void SetSleepStatus (bool status)
    {

        isSleeping = status;

        onSleep?.Invoke(isSleeping);
        SetStateFrezze(isSleeping);
        characterActive = !status;
        WorldManager.Manager.NewTimeScale(status == true ? 4 : 1);
    }

    public void SetFatigue (bool status)
    {
        isFatigue = status;
    }


    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TAG_INTERACTION_OBJECT))
        {
            SetStateCollisionInteractionObject(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(TAG_INTERACTION_OBJECT))
        {
            SetStateCollisionInteractionObject(false);
        }


    }

    private void SetStateCollisionInteractionObject (bool status)
    {
        collisizedInteractionObject = status;
    }

    private bool CharacterAttackingEnabled ()
    {
        int countAnimationAttack = 3;


        for (int i = 0; i < countAnimationAttack; i++)
        {
            if (animator.GetBool(ATTACK_NAME_ANIM + (i + 1)))
            {
                return true;
            }
        }

        return false;
    }

    #region Audio

    private void LoadAudio ()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>(NAME_FOLBER_AUDIO_CHARACTER);

        for (int i = 0; i < clips.Length; i++)
        {
            clipsCharacter.Add(clips[i].name, clips[i]);
        }
    }

    private void CreateAudioObjectWalk ()
    {
        walkAudioObject = audioManager.CreateAudioObject(transform.position, clipsCharacter["character_walking"]);
        walkAudioObject.transform.SetParent(transform);
        walkAudioObject.GetAudioSource().loop = true;
        walkAudioObject.name = "AudioCharacter";
    }

    private void StopWalkSound ()
    {
            AudioSource audioSource = walkAudioObject.GetAudioSource();
            audioSource.Stop();
        
    }

    private void PlayWalkSound()
    {
            AudioSource audioSource = walkAudioObject.GetAudioSource();
            audioSource.Play();
        
    }

    private AudioObject PlayFXPlayer(string audioName)
    {
        AudioObject audioObject = audioManager.CreateAudioObject(transform.position, clipsCharacter[audioName]);
        audioObject.GetAudioSource().Play();
        audioObject.RemoveIfNotPlaying = true;
        return audioObject;
    }

    private void PlaySoundGetItem(string idItem)
    {
        if (getItemAudioObject == null)
        {
            getItemAudioObject = PlayFXPlayer("character_get_item");
        }
    }

    private void PlaySoundNoGetItem()
    {
        Debug.Log(323);
        if (getItemAudioObject == null)
        {
            getItemAudioObject = PlayFXPlayer("character_get_item_limit");
        }
    }


    #endregion

    #region Adrenalin System
    public void BuffAdrenalin (SyringeAdrenalinParams syringeParams)
    {
        IncrementBaseDamage(syringeParams.bonusDamage);
        IncrementBaseSpeed(syringeParams.bonusSpeed);
        SetStateAdrenalin(true);
        StartCoroutine(DurationAdrenalin(syringeParams));
        StartCoroutine(HitAdrenalin());

    }

    private IEnumerator HitAdrenalin()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (isAdrenalin)
            {
        Hit(1, false);
            }

            else
            {
                yield break;
            }
        }

    }

    private void SetStateAdrenalin (bool status)
    {
        onAdrenalin?.Invoke(status);
        isAdrenalin = status;
    }

    private IEnumerator DurationAdrenalin (SyringeAdrenalinParams syringeParams)
    {
        yield return new WaitForSeconds(syringeParams.duration);

        DecrementBaseDamage(syringeParams.bonusDamage);
        DecrementBaseSpeed(syringeParams.bonusSpeed);


        SetStateAdrenalin(false);


        yield return null;

    }
    #endregion

}
