using System;
using UnityEngine;

public class ListenerCharacterUI : MonoBehaviour, IRemoveObject
    {

    private const string PATH_PREFAB_BACKGROUND_DAMAGE = "Prefabs/UI/BackgroundDamage";


    private Character myCharacter;

    private GameObject bgDamagePrefab;

    private GameObject activeBackground;


        // Use this for initialization
        void Start()
        {
        if (PlayerManager.Manager == null)
        {
            throw new ListenerCharacterUIException("player manager not found");
        }

        else if (PlayerManager.Manager.Player == null)
        {
            throw new ListenerCharacterUIException("local player not found");
        }

        bgDamagePrefab = Resources.Load<GameObject>(PATH_PREFAB_BACKGROUND_DAMAGE);

        if (bgDamagePrefab == null)
        {
            throw new ListenerCharacterUIException("prefab background damage not found");
        }

        myCharacter = PlayerManager.Manager.Player;

        myCharacter.onDamage += PlayerGetHit;
        myCharacter.onDead += Remove;
    }

    public void Remove()
    {
        myCharacter.onDamage -= PlayerGetHit;
        myCharacter.onDead -= Remove;
        Destroy(gameObject);
    }

    private void PlayerGetHit()
    {
        if (activeBackground == null)
        {
            activeBackground = Instantiate(bgDamagePrefab);
        }
    }

    }