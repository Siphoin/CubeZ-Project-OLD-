using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]
public class BaseZombie : MonoBehaviour, IAnimatiomStateController, ICheckerStats, IGeterHit, IInvokerMono
{
    protected Character target = null;
    protected IGeterHit otherTarget = null;




    protected Vector3 targetPoint = Vector3.zero;

    [SerializeField, ReadOnlyField] protected bool visiblePlayer = false;

    protected NavMeshAgent agent;


    protected Animator animator;

    private WorldManager worldManager;


    private int[] hashesAnimationAttackZombie = new int[]
    {
        -827840423,
        -224906799,
        -617844629,
    };



    protected Rigidbody rb;

    protected TypeAnimation animationState = TypeAnimation.Idle2;

    private const string PATH_CHARACTER_SETTINGS = "Character/CharacterSettings";

    protected const string TAG_PLAYER = "Player";

    protected const string PREFIX_DEAD_PLAYER = "Dead";

    private const string TAG_DEAD_ZOMBIE = "DeadZombie";

    private const string ANIM_DEATH_NAME = "Death";

    protected const string TAG_HOUSE = "HouseArea";

    protected const string TAG_WALL = "Wall";

    private const string TAG_PLAYER_AREA = "PlayerArea";


    protected const float DISTANCE_FOR_ATTACK = 0.9f;


    [SerializeField, ReadOnlyField] private SettingsZombie settingsZombie;


    [Header("Информация о статах зомби")]
    [SerializeField] private ZombieStatsSettings zombieStatsSettings;

    [Header("Zombie Area компонент")]
    [SerializeField] private ZombieArea zombieArea;

    protected ZombieStats zombieStats;

    protected Dictionary<Type, IStateBehavior> behaviorMap = new Dictionary<Type, IStateBehavior>();

    private IStateBehavior currentStateBehavior;

    public event Action<IStateBehavior> onNewBehavior;

    private ZombieAnimatorObserver animatorObserver;

    private bool isDead = false;
    protected bool inHouse = false;

    private int startHealth = 0;

    private int countCallWalkingBehavior = 0;

    private SettingsZombieData zombieData;

    public event Action onRemove;
    public event Action onDeath;
    public event Action onAttack;

    protected HouseArea houseAreaEntered;

    private BoxCollider boxCollider;
    private CharacterDataSettings characterDataSettings;
    private CharacterData characterData;

    public float FastSpeed { get => zombieStats.speed * 2; }
    public int StartHealth { get => startHealth; }
    public IStateBehavior CurrentStateBehavior { get => currentStateBehavior; }
    public bool VisiblePlayer { get => visiblePlayer; }
    public Character Target { get => target; }

    public Bounds HouseAreaBounds { get => houseAreaEntered.GetBounds(); }
    public float DistanceVisible { get => zombieStats.distanceVisible; }
    public bool InHouse { get => inHouse; }
    public bool IsDead { get => isDead; }



    public bool IsWalking { get => agent != null && agent.velocity != Vector3.zero; }
    public int CountCallWalkingBehavior { get => countCallWalkingBehavior; set => countCallWalkingBehavior = value; }

    public int CurrentHealth { get => zombieStats.health; }
    public int[] HashesAnimationAttackZombie { get => hashesAnimationAttackZombie; }
    public ZombieStats ZombieStats { get => zombieStats; }

    protected void Ini()
    {
        if (zombieArea == null)
        {

        }


        if (WorldManager.Manager == null)
        {
            throw new ZombieException("world manager not found");
        }

        worldManager = WorldManager.Manager;

        worldManager.onDayChanged += NewDay;


        if (zombieStatsSettings == null)
        {
            throw new ZombieException("zombie stats settings is null");
        }
        zombieStats = new ZombieStats(zombieStatsSettings.GetData());

        settingsZombie = WorldManager.Manager.SettingsZombie;


        if (settingsZombie == null)
        {
            throw new ZombieException("zombie settings is null");
        }
        zombieData = new SettingsZombieData(settingsZombie.GetData());


#if UNITY_EDITOR
        CheckValidStats();

#endif
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        animator = transform.GetChild(0).GetComponent<Animator>();
        animatorObserver = transform.GetChild(0).GetComponent<ZombieAnimatorObserver>();


        SetTargetPoint(transform.position);


        RandomizeStats();

        // behaviors ini

        AddBehaviors();

        WalkingBack();

        animatorObserver.onAttackEvent += Damage;

        startHealth = zombieStats.health;

        if (!TryGetComponent(out boxCollider))
        {
            throw new ZombieException("zombie not have box colider component");
        }


        characterDataSettings = Resources.Load<CharacterDataSettings>(PATH_CHARACTER_SETTINGS);

        if (characterDataSettings == null)
        {
            throw new ZombieException("not found character data");
        }
        characterData = new CharacterData(characterDataSettings.GetData());

        if (worldManager.CurrentDayTime == DayTimeType.Night)
        {
            BuffStatsZombie();
        }


        zombieArea.SetRadius(zombieStats.distanceVisible / 2);
        zombieArea.onCharacterVisible += ZombieArea_onCharacterVisible;
        zombieArea.onCarAlarm += ZombieArea_onCarAlarm;
    }

    private void ZombieArea_onCarAlarm(Vector3 position)
    {
        if (!visiblePlayer)
        {
            SetTargetPoint(position);
        }
    }

    private void ZombieArea_onCharacterVisible(Character character)
    {
        if (target == null)
        {
            target = character;
            visiblePlayer = true;
            SetAggresiveBehavior();
        }

    }

    private void NewDay(DayTimeType dayTime)
    {
        if (dayTime == DayTimeType.Night)
        {
            BuffStatsZombie();
        }

        else if (dayTime == DayTimeType.Morming)
        {
            DeBuffStatsZombie();
        }
    }

    private void AddBehaviors()
    {
        AddBehavior<AggresiveStateZombie>(new AggresiveStateZombie(this));
        AddBehavior<WalkingStateZombie>(new WalkingStateZombie(this));
    }

    private void RandomizeStats()
    {
        zombieStats.speed = Random.Range(zombieStats.speed, zombieData.maxSpeed);
        zombieStats.distanceVisible = Random.Range(zombieStats.distanceVisible, zombieData.maxDistanceVisible);
        zombieStats.damage = Random.Range(zombieStats.damage, zombieData.maxDamage);
        zombieStats.health = Random.Range(zombieStats.health, zombieData.maxHealth);

        //   Debug.Log(zombieStats);
    }

    public void CheckValidStats()
    {
        foreach (var prop in zombieStats.GetType().GetFields())
        {
            try
            {
                ValidatorData.CheckValidFieldStats(prop, zombieStats);
            }
            catch (ValidatorDataException)
            {

                throw;
            }
        }

        foreach (var prop in zombieStats.GetType().GetProperties())
        {
            try
            {
                ValidatorData.CheckValidPropertyStats(prop, zombieStats);
            }
            catch (ValidatorDataException)
            {

                throw;
            }
        }
    }


    protected void AddBehavior<T>(IStateBehavior state)
    {
        behaviorMap[typeof(T)] = state;

    }

    protected void SetBehavior(IStateBehavior stateBehavior)
    {
        if (currentStateBehavior != null)
        {
            StopCoroutine(currentStateBehavior.UpdateWaiting());
            currentStateBehavior.Exit();
        }

        currentStateBehavior = stateBehavior;
        currentStateBehavior.Enter();
        onNewBehavior?.Invoke(currentStateBehavior);
    }

    protected void UpdateState()
    {
        if (currentStateBehavior != null)
        {
            currentStateBehavior.Update();
        }
    }



    public void Damage()
    {

        if (target != null && Vector3.Distance(transform.position, target.transform.position) < DISTANCE_FOR_ATTACK)
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, transform.forward, out raycastHit, DistanceVisible))
            {
                if (raycastHit.collider.tag.Contains(TAG_PLAYER_AREA))
                {
                    Character character = null;

                    if (!raycastHit.collider.transform.parent.TryGetComponent(out character))
                    {
                        throw new ZombieException("player area object parent not have component Character");
                    }
                    character.Hit(zombieStats.damage);


                    return;


                }
                if (raycastHit.collider.gameObject == target.gameObject)
                {
                    target.Hit(zombieStats.damage);
                    SendEventAttack();

                }

                if (target.Health <= 0)
                {
                    SetWalkingBehavior();
                }
            }


        }

        if (otherTarget != null)
        {

                otherTarget.Hit(zombieStats.damage);
                Debug.Log("ATTACK");
                CheckHealthOtherTarget();
                SendEventAttack();
            
        }
    }

    private void SendEventAttack()
    {
        onAttack?.Invoke();
    }

    public void SetTargetPoint(Vector3 point)
    {
        targetPoint = point;
        agent.SetDestination(targetPoint);
    }


    public void WalkingBack()
    {
        SetWalkingBehavior();
    }

    public virtual void WatchingEnviroment()
    {
        if (isDead || otherTarget != null)
        {
            return;
        }
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit))
        {
            if (ValidTagsPlayer(ref raycastHit))
            {
                target = raycastHit.collider.GetComponent<Character>();
                if (!visiblePlayer)
                {
                    float distance = Vector3.Distance(transform.position, target.transform.position);
                    if (distance <= zombieStats.distanceVisible)
                    {
                        visiblePlayer = true;
                        SetAggresiveBehavior();

                    }
                }
            }

        }

    }

    private bool ValidTagsPlayer(ref RaycastHit raycastHit)
    {
        return raycastHit.collider.tag.Contains(TAG_PLAYER) && !raycastHit.collider.tag.Contains(PREFIX_DEAD_PLAYER) && !raycastHit.collider.tag.Contains("Area");
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

    public void SetWalkingBehavior()
    {
        visiblePlayer = false;
        SetBehavior(behaviorMap[typeof(WalkingStateZombie)]);
        StartCoroutine(behaviorMap[typeof(WalkingStateZombie)].UpdateWaiting());
        countCallWalkingBehavior++;
    }


    public void SetAggresiveBehavior()
    {
        visiblePlayer = true;
        SetBehavior(behaviorMap[typeof(AggresiveStateZombie)]);
        StartCoroutine(behaviorMap[typeof(AggresiveStateZombie)].UpdateWaiting());
    }

    protected void LockToTarget()
    {
        float distancetoPoint = Vector3.Distance(transform.position, targetPoint);
        if (distancetoPoint > 0.2f)
        {

            Vector3 direction = targetPoint - transform.position;

            if (!direction.NotValid())
            {


                Quaternion root = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, root, characterData.rotateSpeed * Time.deltaTime);
                var rootNormal = transform.rotation;
                rootNormal.x = 0;
                rootNormal.z = 0;
                transform.rotation = rootNormal;
            }

        }

    }

    public void Hit(int hitValue, bool playHitAnim = true)
    {
        zombieStats.health = Mathf.Clamp(zombieStats.health - hitValue, 0, startHealth);
        if (playHitAnim)
        {
            if (Random.Range(0, 10) > 7)
            {

                SetAnimationState(TypeAnimation.GetHit);
            }
        }




        if (zombieStats.health <= 0)
        {
            isDead = true;
            zombieStats.health = 0;
            visiblePlayer = false;
            agent.speed = 0;
            agent.acceleration = 0;
            target = null;
            tag = TAG_DEAD_ZOMBIE;
            currentStateBehavior.Exit();
            StopCoroutine(currentStateBehavior.UpdateWaiting());
            Dead();
        }

    }

    private void Dead()
    {
        worldManager.onDayChanged -= NewDay;
        onDeath?.Invoke();
        TimerDestroy timerDestroy = gameObject.AddComponent<TimerDestroy>();
        timerDestroy.timeDestroy = zombieData.timeRemove;
        animator.Play(ANIM_DEATH_NAME);
        rb.isKinematic = true;
        boxCollider.enabled = false;
        enabled = false;
    }

    public void CallInvokingEveryMethod(Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    public void CallInvokingMethod(Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }

    private void OnDestroy()
    {
        CallRemoveEvent();
    }

    protected void CallRemoveEvent()
    {
        onRemove?.Invoke();
    }

    private void BuffStatsZombie()
    {
        zombieStats.distanceVisible *= zombieData.incrementPowerZombieOnlyNight;
        zombieStats.damage *= zombieData.incrementPowerZombieOnlyNight;
    }

    private void DeBuffStatsZombie()
    {
        zombieStats.distanceVisible /= zombieData.incrementPowerZombieOnlyNight;
        zombieStats.damage /= zombieData.incrementPowerZombieOnlyNight;
    }

    private void CheckHealthOtherTarget()
    {
        if (otherTarget is Door)
        {
            Door door = (Door)otherTarget;


            if (door.Health <= 0)
            {
                otherTarget = null;
                SetWalkingBehavior();
                return;
            }
        }
        if (otherTarget is Wall)
        {
            Wall wall = (Wall)otherTarget;

            if (wall.CurrentHealth <= 0)
            {
                otherTarget = null;
                SetWalkingBehavior();
                return;
            }
        }

        
    }

}