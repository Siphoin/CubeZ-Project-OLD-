using System;
using UnityEngine;

public class ListenerCharacterUI : MonoBehaviour, IRemoveObject
    {

    private const string PATH_PREFAB_BACKGROUND_DAMAGE = "Prefabs/UI/BackgroundDamage";

    private const string PATH_PREFAB_BACKGROUND_ADRENALIN = "Prefabs/UI/BackgroundAdrenalin";

    private const string PATH_PREFAB_WINDOW_PROGRESS_LEVEL = "Prefabs/UI/window_progress_level";

    private const string NAME_MAIN_CANVAS = "MainCanvas";

    private Character myCharacter;

    private GameObject bgDamagePrefab;

    private GameObject bgAdrenalinPrefab;

    private GameObject activeBackground;

    private WindowProgressLevel windowProgressLevelPrefab;

    private WindowProgressLevel windowProgressLevelActive;

    private Transform mainCanvas;


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

        mainCanvas = GameObject.Find(NAME_MAIN_CANVAS).GetComponent<Transform>();

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

        windowProgressLevelPrefab = Resources.Load<WindowProgressLevel>(PATH_PREFAB_WINDOW_PROGRESS_LEVEL);

        if (windowProgressLevelPrefab == null)
        {
            throw new ListenerCharacterUIException("prefab window progress level not found");
        }


        myCharacter = PlayerManager.Manager.Player;

        myCharacter.onDamage += PlayerGetHit;
        myCharacter.onDead += Remove;
        myCharacter.onAdrenalin += OnAdrenalin;
        myCharacter.onXPAdded += ShowProgressLevel;
    }

    private void ShowProgressLevel(long value)
    {
        if (windowProgressLevelActive == null)
        {
      windowProgressLevelActive =  Instantiate(windowProgressLevelPrefab, mainCanvas);
        }

        else
        {
            windowProgressLevelActive.UpdateData();
        }

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
        myCharacter.onXPAdded -= ShowProgressLevel;
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