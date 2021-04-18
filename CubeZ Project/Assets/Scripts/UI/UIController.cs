using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] UIWindowFragment[] windows;
    private HashSet<string> windowsCached = new HashSet<string>();

    private const string PREFIX_PREFAB = "(Clone)";

    private static UIController manager;

    private Window activeWindow = null;

    private List<Window> openedWindows = new List<Window>(0);
    [Header("Включен")]
[SerializeField, ReadOnlyField]    private bool on = true;

    public static UIController Manager { get => manager; }
    public bool On { get => on; set => on = value; }
    public Window ActiveWindow { get => activeWindow; }

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
                    if (activeWindow == null)
                    {
                   activeWindow = OpenWindow(windowFragment.window);
                    }

                }
            }
        }
    }

    private void WindowExit(Window window)
    {
        openedWindows.Remove(window);
        windowsCached.Remove(window.name);
    }


    public Window OpenWindow(string nameWindow)
    {
        UIWindowFragment targetFragment = windows.Single(windowSelected => windowSelected.window.name == nameWindow);
        Window window = Instantiate(targetFragment.window);
        window.onExit += WindowExit;
        windowsCached.Add(window.name);
        CacheWindow(window);
        activeWindow = window;
        return window;

    }

    public Window OpenWindow(Window windowTarget)
    {
        Window newWindow = Instantiate(windowTarget);
        newWindow.onExit += WindowExit;
        windowsCached.Add(newWindow.name);
        CacheWindow(windowTarget);
        activeWindow = newWindow;
        return newWindow;

    }

    public bool ContainsWindow(string windowName)
    {
        return windowsCached.Contains(windowName + PREFIX_PREFAB);
    }

    public void CloseAllWindows ()
    {
        if (activeWindow != null)
        {
            activeWindow.Exit();
        }
    }

    private void CacheWindow (Window window)
    {
        openedWindows.Add(window);
    }

}