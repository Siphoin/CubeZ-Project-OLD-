using System;
using System.Collections;
using UnityEngine;

    public class Tree : MonoBehaviour, IGeterHit
    {
    [SerializeField] ItemObject woodPrefab;
    [Header("Главный объект дерева")]
    [SerializeField] GameObject treeMain;

    [Header("Звук рубки дерева")]
    [SerializeField] AudioClip audioClipAxe;

    [SerializeField, ReadOnlyField] private int currentHealth;

    private int startHealth = 0;


    TreeData treeData = null;

    public event Action onDead;


    private const string PATH_TREE_SETTINGS = "Trees/TreeSettings";

    public int CurrentHealth { get => currentHealth; }

    // Use this for initialization
    void Start()
    {

        if (treeMain == null)
        {
            throw new TreeException("tree main not seted");
        }
        if (woodPrefab == null)
        {
            throw new TreeException("wood prefab not found");
        }

        if (woodPrefab.TypeItem != TypeItem.Resource)
        {
            throw new TreeException("prefab wood not valid. type not be Resource");
        }

        LoadTreeData();
        startHealth = treeData.startHealth;
    }

    public void Hit(int hitValue, bool playHitAnim = true)
    {
        currentHealth = Mathf.Clamp(currentHealth - hitValue, 0, startHealth);


        if (currentHealth <= 0)
        {
            onDead?.Invoke();
            CreateWoodItemObject();
            Destroy(treeMain);
        }

        if (AudioDataManager.Manager != null && audioClipAxe != null)
        {
            PlaySoundAxe();
        }
    }

    private void PlaySoundAxe()
    {
        AudioObject audioObject = AudioDataManager.Manager.CreateAudioObject(transform.position, audioClipAxe);
        audioObject.RemoveIfNotPlaying = true;
        audioObject.GetAudioSource().Play();
    }

    private void LoadTreeData()
    {
        TreeSettings treeSettings = Resources.Load<TreeSettings>(PATH_TREE_SETTINGS);

        if (treeSettings == null)
        {
            throw new TreeException("tree settings not found");
        }

        treeData = new TreeData(treeSettings.Data);
        currentHealth = treeData.startHealth;
    }

    public void SetData (TreeCacheData data)
    {
        treeData = new TreeData();
        treeData.startHealth = data.health.StartValue;
    }
    private void CreateWoodItemObject ()
    {
        Instantiate(woodPrefab).transform.position = transform.position;
    }

    }