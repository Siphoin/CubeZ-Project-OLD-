using System.Collections;
using UnityEngine;

class WalkingStateZombie : IStateBehavior
{
    private BaseZombie owner;
    public void Enter()
    {

    }

    public void Exit()
    {
        //     Debug.Log("Zombie walking off");
    }


    public void Update()
    {
        //    Debug.Log("Zombie walking...");
    }

    public IEnumerator UpdateWaiting()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(12, 14));


            if (owner.CountCallWalkingBehavior > 1)
            {
                owner.CountCallWalkingBehavior--;
                yield break;
            }
            if (!owner.VisiblePlayer)
            {
                Vector3 targetPosWalking = owner.InHouse == false ? owner.transform.position : owner.HouseAreaBounds.center / 2;
                var newPos = NavMeshManager.GenerateRandomPath(targetPosWalking);
                newPos.y = owner.transform.position.y;
                owner.SetTargetPoint(newPos);
            }

            else
            {
                owner.SetAggresiveBehavior();
                yield break;

            }
        }
    }


    public WalkingStateZombie(BaseZombie owner)
    {
        this.owner = owner;
    }
}