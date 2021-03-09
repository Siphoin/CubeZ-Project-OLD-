using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BoxCollider))]
public class Zombie : BaseZombie
    {

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
        if (!visiblePlayer)
        {
            float distance = Vector3.Distance(transform.position, targetPoint);
            if (distance > 1f)
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

            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance > 1f)
            {
                SetAnimationState(TypeAnimation.Run);
            }

            else
            {
                SetAnimationState(TypeAnimation.ZombieAttackGeneric);
            }
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
        if (collision.gameObject.tag == TAG_PLAYER)
        {
            target = collision.gameObject.GetComponent<Character>();
            SetAggresiveBehavior();
        }
    }


}