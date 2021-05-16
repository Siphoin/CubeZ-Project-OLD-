using TMPro;
using UnityEngine;

public class UIStatsWorldController : MonoBehaviour
{
    [Header("Текст температуры")]
    [SerializeField] TextMeshProUGUI textTemperature;

    [Header("Текст проццента заражения местности")]
    [SerializeField] TextMeshProUGUI textinfectProcent;

    private ZombieSpawner zombieSpawner;
    // Use this for initialization
    void Start()
    {
        if (textTemperature == null)
        {
            throw new UIStatsWorldControllerException("text temperature is null");
        }

        if (textinfectProcent == null)
        {
            throw new UIStatsWorldControllerException("text infected procent is null");
        }

        zombieSpawner = FindObjectOfType<ZombieSpawner>();
        
        WorldManager.Manager.onTemperatureChanged += Manager_onTemperatureChanged;


        Manager_onTemperatureChanged();

        ZombieSpawner_newInfectProgress();
    }

    private void Manager_onTemperatureChanged()
    {
        textTemperature.text = WorldManager.Manager.TemperatureString;
    }

    private void ZombieSpawner_newInfectProgress()
    {
        textinfectProcent.text = $"Infected: {zombieSpawner.GetProcentInfect()}%";
    }

    private void OnDestroy()
    {
        try
        {
            WorldManager.Manager.onTemperatureChanged -= Manager_onTemperatureChanged;

            if (zombieSpawner != null)
            {
                zombieSpawner.newInfectProgress -= ZombieSpawner_newInfectProgress;
            }
        }
        catch
        {
            
        }
    }

}