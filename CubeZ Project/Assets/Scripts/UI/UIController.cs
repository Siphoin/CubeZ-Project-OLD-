using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] UIWindowFragment[] windows;
    private HashSet<string> windowsCached = new HashSet<string>();

    private const string PREFIX_PREFAB = "(Clone)";

    private static UIController manager;

    public bool On { get; set; } = true;

    public static UIController Manager { get => manager; }

    // Use this for initialization
    void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && On)
        {
            for (int i = 0; i < windows.Length; i++)
            {
                UIWindowFragment windowFragment = windows[i];
                if (Input.GetKeyDown(ControlManagerObject.Manager.ControlManager.GetKeyCodeByFragment(windowFragment.buttonPressTrigger)) && !windowsCached.Contains(windowFragment.window.name + PREFIX_PREFAB))
                {
                    OpenWindow(windowFragment.window);
                }
            }
        }
    }

    private void WindowExit(Window window)
    {
        windowsCached.Remove(window.name);
    }


    public Window OpenWindow(string nameWindow)
    {
        UIWindowFragment targetFragment = windows.Single(windowSelected => windowSelected.window.name == nameWindow);
        Window window = Instantiate(targetFragment.window);
        window.onExit += WindowExit;
        windowsCached.Add(window.name);
        return window;

    }

    public Window OpenWindow(Window windowTarget)
    {
        UIWindowFragment targetFragment = windows.Single(windowSelected => windowSelected.window.name == windowTarget.name);
        Window newWindow = Instantiate(targetFragment.window);
        newWindow.onExit += WindowExit;
        windowsCached.Add(newWindow.name);
        return newWindow;

    }

    public bool ContainsWindow(string windowName)
    {
        return windowsCached.Contains(windowName + PREFIX_PREFAB);
    }

}