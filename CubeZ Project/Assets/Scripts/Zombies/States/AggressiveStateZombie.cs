using System.Collections;
using UnityEngine;

class AggresiveStateZombie : IStateBehavior
{
    BaseZombie owner;
    public void Enter()
    {
       // Debug.Log("Zombie agressive on");
    }

    public void Exit()
    {
      //  Debug.Log("Zombie agressive off");
    }

    public void Update()
    {
      //  Debug.Log("Zombie agressive update");
    }

    public IEnumerator UpdateWaiting()
    {
        while (true)
        {

            yield return new WaitForSeconds(1.0f / 60.0f);
            if (owner.Target != null)
            {
                float distance = Vector3.Distance(owner.transform.position, owner.Target.transform.position);
                if (distance > owner.DistanceVisible)
                {
                    owner.SetWalkingBehavior();
                    yield break;

                }

                else
                {
                    var pos = owner.Target.transform.position;
                    pos.y = owner.transform.position.y;
                    owner.SetTargetPoint(pos);
                }
            }


        }
    }

    public AggresiveStateZombie(BaseZombie owner)
    {
        this.owner = owner;
    }
}