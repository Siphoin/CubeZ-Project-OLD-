using CBZ.API.Debug;
using System;
using UnityEngine;

public class DebugerPython : MonoBehaviour, ISenderMessagePythonDebuger, IInvokerMono
    {
    private const string NAME_KEY_CODE_OPEN_CONSOLE = "developConsole";

    private const string PATH_PREFAB_DEBUG_WINDOW = "Prefabs/UI/DebugPythonUI";

    private ControlManager controlManager;

    private DebugPythonUI debugWindowPrefab;

    private DebugPythonUI debugWindowActive;

    private static DebugerPython activeDebuger = null;

    public static DebugerPython ActiveDebuger { get => activeDebuger; }

    // Use this for initialization
    void Start()
        {
        if (activeDebuger == null)
        {
            if (ControlManagerObject.Manager == null)
            {
                throw new DebugerPythonException("control manager not found");
            }

            controlManager = ControlManagerObject.Manager.ControlManager;

            debugWindowPrefab = Resources.Load<DebugPythonUI>(PATH_PREFAB_DEBUG_WINDOW);

            if (debugWindowPrefab == null)
            {
                throw new DebugerPythonException("debug window prefab not found");
            }

            debugWindowActive = Instantiate(debugWindowPrefab, transform);
            debugWindowActive.onClearConsole += HideConsole;
            DontDestroyOnLoad(gameObject);
            activeDebuger = this;

            CallInvokingMethod(HideConsole, 0.02f);
#if UNITY_EDITOR
            UnityEngine.Debug.Log("debuger python is working...");
#endif
        }

        else
        {
            Destroy(gameObject);
        }
        }

    private void HideConsole()
    {
        SetStatusVisibleConsole(false);
    }

    private void SetStatusVisibleConsole (bool status)
    {
        debugWindowActive.gameObject.SetActive(status);
    }

    // Update is called once per frame
    void Update()
        {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(controlManager.GetKeyCodeByFragment(NAME_KEY_CODE_OPEN_CONSOLE)))
            {
                SetStatusVisibleConsole(!debugWindowActive.gameObject.activeSelf);
            }
        }


        }

    public void NewMessage(string text, LogMessageType messageType = LogMessageType.Message)
    {
        try
        {
            debugWindowActive.NewMessage(text, messageType);

            if (debugWindowActive.gameObject.activeSelf == false)
            {
                SetStatusVisibleConsole(true);
            }
        }

        catch
        {

        }
    }

    public void Clear()
    {
        debugWindowActive.Clear();
    }

    public void CallInvokingEveryMethod(Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    public void CallInvokingMethod(Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }
}