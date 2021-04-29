using System;
using UnityEngine;

public class ListenerCharacterUI : MonoBehaviour, IRemoveObject
    {

    private const string PATH_PREFAB_BACKGROUND_DAMAGE = "Prefabs/UI/BackgroundDamage";

    private const string PATH_PREFAB_BACKGROUND_ADRENALIN = "Prefabs/UI/BackgroundAdrenalin";

    private Character myCharacter;

    private GameObject bgDamagePrefab;

    private GameObject bgAdrenalinPrefab;

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

        bgAdrenalinPrefab = Resources.Load<GameObject>(PATH_PREFAB_BACKGROUND_ADRENALIN);

        if (bgAdrenalinPrefab == null)
        {
            throw new ListenerCharacterUIException("prefab background adrenalin not found");
        }

        myCharacter = PlayerManager.Manager.Player;

        myCharacter.onDamage += PlayerGetHit;
        myCharacter.onDead += Remove;
        myCharacter.onAdrenalin += OnAdrenalin;
    }

    private void OnAdrenalin(bool adrenalin)
    {
        if (activeBackground != null)
        {
            Destroy(activeBackground);
        }
        if (adrenalin)
        {
        activeBackground = Instantiate(bgAdrenalinPrefab);
        }

    }

    public void Remove()
    {
        myCharacter.onDamage -= PlayerGetHit;
        myCharacter.onDead -= Remove;
        myCharacter.onAdrenalin -= OnAdrenalin;
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