using System.Collections;
using TMPro;
using UnityEngine;

    public class UIStatsWorldController : MonoBehaviour
    {
    [SerializeField] TextMeshProUGUI textTemperature;
        // Use this for initialization
        void Start()
        {
        if (textTemperature == null)
        {
            throw new UIStatsWorldControllerException("text temperature is null");
        }
        textTemperature.text = string.Empty;
        WorldManager.Manager.onTemperatureChanged += Manager_onTemperatureChanged;
        }

    private void Manager_onTemperatureChanged()
    {
        textTemperature.text = WorldManager.Manager.TemperatureString;
    }

    // Update is called once per frame
    void Update()
        {

        }
    }