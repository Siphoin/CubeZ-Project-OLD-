using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BoxCollider))]
public class Zombie : BaseZombie
{
    private const string TAG_DOOR = "Door";
    private const string TAG_WALL_DYNAMIC = "WallDynamic";
    // Use this for initialization
    void Start()
    {
        Ini();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateState();
        agent.speed = visiblePlayer == true ? FastSpeed : zombieStats.speed;


        agent.isStopped = animator.GetCurrentClipPlayed().name == TypeAnimation.ZombieAttackGeneric.ToString();

        if (animationState == TypeAnimation.ZombieAttackGeneric)
        {
            agent.velocity = Vector3.zero;
        }
        if (!visiblePlayer)
        {
            if (agent.velocity != Vector3.zero)
            {
                SetAnimationState(TypeAnimation.ZombieWalk);
            }

            else
            {
                SetAnimationState(TypeAnimation.Idle2);
            }
        }

        else
        {
            if (target != null && otherTarget == null)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);


                if (distance > DISTANCE_FOR_ATTACK)
                {
                    SetAnimationState(TypeAnimation.Run);
                }

                else
                {
                    SetAnimationState(TypeAnimation.ZombieAttackGeneric);
                }
            }
        }

        if (otherTarget != null)
        {
            SetAnimationState(TypeAnimation.ZombieAttackGeneric);
        }



        try
        {
            if (targetPoint != Vector3.zero)
            {
                LockToTarget();
            }

        }

        catch
        {

        }

        if (target != null && target.Health <= 0)
        {
            SetWalkingBehavior();
            target = null;
        }

    }

    private void FixedUpdate()
    {
        WatchingEnviroment();
        rb.velocity = Vector3.zero;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains(TAG_PLAYER))
        {
            target = collision.gameObject.GetComponent<Character>();
            if (!target.IsSleeping)
            {
                SetAggresiveBehavior();
            }

        }

        if (collision.gameObject.CompareTag(TAG_DOOR))
        {
            ColissionDoor(collision);
        }

        if (collision.gameObject.CompareTag(TAG_WALL_DYNAMIC))
        {
            ColissionWall(collision);
        }

        if (collision.gameObject.CompareTag(TAG_HOUSE))
        {

            if (!collision.gameObject.TryGetComponent(out houseAreaEntered))
            {
                throw new ZombieException("colized area house not have component HouseArea");
            }
            inHouse = true;

        }
        if (collision.gameObject.CompareTag(TAG_WALL))
        {
            if (!visiblePlayer)
            {
                SetTargetPoint(NavMeshManager.GenerateRandomPath(transform.position));
            }


        }

        if (collision.gameObject.CompareTag(tag))
        {
            if (!visiblePlayer)
            {
                SetTargetPoint(NavMeshManager.GenerateRandomPath(transform.position));
            }


        }
    }

    private void ColissionDoor(Collision collision)
    {
        Door door = null;

        if (!collision.gameObject.TryGetComponent(out door))
        {
            throw new ZombieException("colized door tot have component Door");
        }
        if (!door.IsOpened)
        {
            otherTarget = door;
        }

        Debug.Log(otherTarget);


        target = null;
    }

    private void ColissionWall(Collision collision)
    {
        Wall wall = null;


        if (!collision.gameObject.TryGetComponent(out wall))
        {
            throw new ZombieException("colized door tot have component Wall");
        }
        otherTarget = wall;


        target = null;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(TAG_HOUSE))
        {
            inHouse = false;
            houseAreaEntered = null;
        }
        otherTarget = null;
    }

    private void OnDestroy()
    {
        CallRemoveEvent();
    }

}