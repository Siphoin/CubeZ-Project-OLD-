using System;
using UnityEngine;

public class InfectedCorpse : MonoBehaviour, IInvokerMono, IRemoveObject
    {
    [Header("Тип зомби при оживлении")]
    [SerializeField] private BaseZombie zombieResult;

    private WorldManager worldManager;


    // Use this for initialization
    void Start()
        {
        if (WorldManager.Manager == null)
        {
            throw new InfectedCorpseException("world manager not founs");
        }


        if (zombieResult == null)
        {
            throw new InfectedCorpseException("zombie result not seted");
        }

        worldManager = WorldManager.Manager;

        CallInvokingMethod(Remove, worldManager.SettingsZombie.GetData().rebelInfectedCorpseTimeOut);

        }

    public void CallInvokingEveryMethod(Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    public void CallInvokingMethod(Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }

    public void Remove()
    {
        BaseZombie zombie = Instantiate(zombieResult);
        zombie.transform.position = transform.position;

        Vector3 angle = new Vector3(0, transform.eulerAngles.y, 0);

        zombie.transform.rotation = Quaternion.Euler(angle);


        Destroy(gameObject);
    }

}

