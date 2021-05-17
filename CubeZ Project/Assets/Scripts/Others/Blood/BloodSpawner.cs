using System.Collections;
using UnityEngine;

    public class BloodSpawner : MonoBehaviour, IBloodCreator
    {
    [SerializeField] private int maxCountBloodDecals = 100;

    private Blood bloodPrefab;

    private const string PATH_BLOOD_PREFAB = "Prefabs/Others/blood_decal";
    private const string NAME_BLOOD_CONTAINER = "bloodContainer";
    private WorldManager worldManager;

    private GameObject parentBloods;

        // Use this for initialization
        void Start()
    {
        if (WorldManager.Manager == null)
        {
            throw new BloodSpawnerException("world manager not found");
        }

        worldManager = WorldManager.Manager;

        bloodPrefab = Resources.Load<Blood>(PATH_BLOOD_PREFAB);

        if (bloodPrefab == null)
        {
            throw new BloodSpawnerException("blood decal prefab not found");
        }
        parentBloods = new GameObject(NAME_BLOOD_CONTAINER);

        CreateBloodsTexturesOnEnviroment();

    }

    private void CreateBloodsTexturesOnEnviroment()
    {
        for (int i = 0; i < maxCountBloodDecals; i++)
        {
            CreateBlood(worldManager.GetRandomPointWithRandomPlane());
        }
    }

    public Blood CreateBlood (Vector3 center)
    {
        Blood blood = Instantiate(bloodPrefab);


        var pos = center;
        pos.y = bloodPrefab.transform.position.y;
        blood.transform.position = pos;

        blood.transform.SetParent(parentBloods.transform);

        return blood;
    }
}