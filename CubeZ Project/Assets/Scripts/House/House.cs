using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class House : MonoBehaviour
{
    [Header("Двери")]
    [SerializeField] Door[] doors;

    [Header("Крыша")]
    [SerializeField] Renderer roof;

    [Header("Триггер дома")]
    [SerializeField] HouseArea houseArea;

    private HouseData houseData = new HouseData();

    private HouseSettings houseSettings;

    private bool viewed = false;

    private bool playerinHouse = false;

    private Bounds boundsAreaHouse;

    private Color defaultColotRoof;

    private const string PATH_SETTINGS_HOUSE = "Props/House/HouseSettings";
    private const string TAG_ZOMBIE_SPAWNER = "ZombieSpawner";


    ZombieSpawner zombieSpawner;
    // Use this for initialization
    void Start()
    {


#if UNITY_EDITOR
        if (doors.Length == 0)
        {
            throw new HouseException("list doors is emtry");
        }

        if (doors.Any(door => door == null))
        {
            throw new HouseException("any door is null");
        }
#endif

        if (roof == null)
        {
            throw new HouseException("roff not seted");
        }
        houseSettings = Resources.Load<HouseSettings>(PATH_SETTINGS_HOUSE);


        if (houseSettings == null)
        {
            throw new HouseException("house settings");
        }



        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].onDoorInteraction += DoorOpened;
        }
        defaultColotRoof = roof.material.color;
        houseArea.onPlayerEnteredHouse += HouseEnteredLocalPlayer;
        houseArea.onOtherPlayerEnteredHouse += HouseEnteredOtherPlayer;
        try
        {
            zombieSpawner = GameObject.FindGameObjectWithTag(TAG_ZOMBIE_SPAWNER).GetComponent<ZombieSpawner>();
        }
        catch
        {

        }

        boundsAreaHouse = houseArea.GetBounds();
        StartCoroutine(SpawnZombies());


        WorldManager.Manager.onDayChanged += NewGenerationZombie;

    }

    private void NewGenerationZombie(DayTimeType dayTime)
    {
        if (dayTime == DayTimeType.Morming)
        {
            StartCoroutine(SpawnZombies());
        }
    }

    private void HouseEnteredLocalPlayer(bool status)
    {
        viewed = status;
        AccountingCountPlayers(status);
        StartCoroutine(LerpingColorRoof());
    }

    private void HouseEnteredOtherPlayer(bool status)
    {
        AccountingCountPlayers(status);
    }

    private void DoorOpened(bool opened)
    {
    }

    private void AccountingCountPlayers (bool status)
    {
        houseData.countPlayersOnHouse += status == true ? 1 : -1;
     //   Debug.Log(houseData.countPlayersOnHouse);
    }

    private IEnumerator SpawnZombies ()
    {
        if (zombieSpawner != null)
        {
            int probability = ProbabilityUtility.GenerateProbalityInt();

            if (probability >= houseSettings.ProbabilitySpawnZombie)
            {
                if (houseData.countPlayersOnHouse < houseSettings.MaxCountZombies)
                {
                    int countZombies = Random.Range(1, houseSettings.MaxCountZombies - houseData.countZombiesOnHouse + 1);
                    for (int i = 0; i < countZombies; i++)
                    {
                        yield return new WaitForSeconds(0.5f);
                        SpawnZombie();
                    }

                }
                
            }
        }
        yield return null;
    }

    private void SpawnZombie()
    {
        Vector3 pos = NavMeshManager.GenerateRandomPath(boundsAreaHouse.center / 2);
        zombieSpawner.CreateZombie(pos, Quaternion.identity);
    }



    // Update is called once per frame
    void Update()
    {

    }

    
    private IEnumerator LerpingColorRoof ()
    {
        float aValue = 0;
        float bValue = 0;

        float lerpValue = 0;
        

        if (viewed)
        {
            aValue = 1f;
            bValue = 0f;
        }

        else
        {
            aValue = 0f;
            bValue = 1f;
        }


        var alphaColor = defaultColotRoof;
        while (true)
        {
            float fpsRate = 1.0f / 60.0F;
            yield return new WaitForSeconds(fpsRate);
            lerpValue += fpsRate;


            alphaColor.a = Mathf.Lerp(aValue, bValue, lerpValue);
            roof.material.color = alphaColor;

            if (lerpValue >= 1f)
            {
                yield break;
            }

        }

    }

}
    