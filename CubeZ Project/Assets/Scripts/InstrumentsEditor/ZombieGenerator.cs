using System.Collections;
using UnityEngine;

    public class ZombieGenerator : MonoBehaviour, IRemoveObject
    {
       public BaseZombie[] zombiesVariants;

     public GameObject[] planes;
    [Header("Количество planes в контейнере")]
    public int countPlanes;

    [Header("Контейнер для складывания новых зомби на карте")]
    public Transform containerZombies;


    [Header("Максимальное кол-во зомби")]
    public int countZombies = 1;
    private const string PATH_ZOMBIES_PREFABS_FOLBER = "Prefabs/Zombies";

    private const string TAG_PLANE = "Plane";


    // Use this for initialization
    void Awake()
        {
        Remove();
        }

       public void LoadZombieVariants ()
        {
        zombiesVariants = Resources.LoadAll<BaseZombie>(PATH_ZOMBIES_PREFABS_FOLBER);

        if (zombiesVariants == null || zombiesVariants.Length == 0)
        {
            throw new ZombieGeneratorException("zombies variants not found");
        }

        


    }

    public void LoadPlanes ()
    {
        planes = GameObject.FindGameObjectsWithTag(TAG_PLANE);
        countPlanes = planes.Length;
    }

    public void GenerateZombies ()
    {
        if (countZombies < 0)
        {
            return;
        }


        if (planes == null || planes.Length == 0)
        {
            throw new ZombieGeneratorException("planes must be loaded");
        }

        if (zombiesVariants == null || zombiesVariants.Length == 0)
        {
            throw new ZombieGeneratorException("zombies variants must be loaded");
        }

        for (int i = 0; i < countZombies; i++)
        {
            BaseZombie selectedZombieVariant = zombiesVariants.Length == 1 ? zombiesVariants[0] : zombiesVariants[Random.Range(0, zombiesVariants.Length)];
            BaseZombie newZombie = Instantiate(selectedZombieVariant);

            float angleY = Random.Range(0, 181f);

            Vector3 angle = newZombie.transform.rotation.eulerAngles;

            angle.y = angleY;

            newZombie.transform.rotation = Quaternion.Euler(angle);

            newZombie.transform.position = NavMeshManager.GenerateRandomPath(planes[Random.Range(0, planes.Length)].transform.position);

            newZombie.name = newZombie.name.Replace("(Clone)", string.Empty);

            if (containerZombies != null)
            {
                newZombie.transform.SetParent(containerZombies);
            }
        }
    }

    public void Remove()
    {
        Destroy(gameObject);


    }
}