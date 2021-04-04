using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class BaseZombie : MonoBehaviour, IAnimatiomStateController, ICheckerStats, IGeterHit, IInvokerMono
{
    protected Character target = null;
    protected IGeterHit otherTarget = null;




    protected Vector3 targetPoint = Vector3.zero;

    [SerializeField, ReadOnlyField] protected bool visiblePlayer = false;

    protected NavMeshAgent agent;


    private Animator animator;



    protected Rigidbody rb;

    protected TypeAnimation animationState = TypeAnimation.Idle2;

    protected const string TAG_PLAYER = "Player";

    protected const string PREFIX_DEAD_PLAYER = "Dead";

    private const string TAG_DEAD_ZOMBIE = "DeadZombie";

    private const string ANIM_DEATH_NAME = "Death";

    protected const string TAG_HOUSE = "HouseArea";

    [SerializeField, ReadOnlyField] private SettingsZombie settingsZombie;

    [SerializeField] private ZombieStatsSettings zombieStatsSettings;

    protected ZombieStats zombieStats;

    protected Dictionary<Type, IStateBehavior> behaviorMap = new Dictionary<Type, IStateBehavior>();

    private IStateBehavior currentStateBehavior;

    private ZombieAnimatorObserver animatorObserver;

    private bool isDead = false;
    protected bool inHouse = false;

    private SettingsZombieData zombieData;

    public event Action onRemove;

  protected  HouseArea houseAreaEntered;

    public float FastSpeed { get => zombieStats.speed * 2; }
    public IStateBehavior CurrentStateBehavior { get => currentStateBehavior; }
    public bool VisiblePlayer { get => visiblePlayer; }
    public Character Target { get => target; }

    public Bounds HouseAreaBounds { get => houseAreaEntered.GetBounds(); }
    public float DistanceVisible { get => zombieStats.distanceVisible; }
    public bool InHouse { get => inHouse; }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    protected void Ini()
    {

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

        if (target != null && Vector3.Distance(transform.position, target.transform.position) < 1f)
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, transform.forward, out raycastHit, DistanceVisible))
            {
                if (raycastHit.collider.gameObject == target.gameObject)
                {
                    target.Hit(zombieStats.damage);

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
        }
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
        if (isDead)
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

    private  bool ValidTagsPlayer(ref RaycastHit raycastHit)
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
    //    Debug.Log(distancetoPoint);
        if (distancetoPoint > 0.2f)
        {

 Vector3 direction = targetPoint - transform.position;

            if (!direction.NotValid()) {
Quaternion root = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, root, 2 * Time.deltaTime);
        var rootNormal = transform.rotation;
        rootNormal.x = 0;
        rootNormal.z = 0;
        transform.rotation = rootNormal;
            }
        
        }
       
    }

    public void Hit(int hitValue, bool playHitAnim = true)
    {
        if (zombieStats.health - hitValue > 0)
        {
            zombieStats.health -= hitValue;
            if (playHitAnim)
            {
                if (Random.Range(0, 10) > 7)
                {

                    SetAnimationState(TypeAnimation.GetHit);
                }
            }


        }

        else
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
        TimerDestroy timerDestroy = gameObject.AddComponent<TimerDestroy>();
        timerDestroy.timeDestroy = zombieData.timeRemove;
        animator.Play(ANIM_DEATH_NAME);
        rb.isKinematic = true;
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

    protected void CallRemoveEvent ()
    {
        onRemove?.Invoke();
    }

}

