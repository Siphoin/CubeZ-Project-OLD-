using System.Collections;
using UnityEngine;

    public class ControlManagerObject : MonoBehaviour
    {
    private ControlManager controlManager;
    private const string PATH_CONNTROL_MANAGER = "System/Control/ControlManager";

    private static ControlManagerObject manager;

    public static ControlManagerObject Manager { get => manager; }
    public ControlManager ControlManager { get => controlManager; }

    // Use this for initialization
    void Awake()
        {
        if (manager == null)
        {
            manager = this;
            controlManager = Resources.Load<ControlManager>(PATH_CONNTROL_MANAGER);

            if (controlManager == null)
            {
                throw new ControlManagerObjectException("control manager not found");
            }
            DontDestroyOnLoad(gameObject);
            Debug.Log("control manager is initialized");
        }

        else
        {
            Destroy(gameObject);
        }
        }

    }