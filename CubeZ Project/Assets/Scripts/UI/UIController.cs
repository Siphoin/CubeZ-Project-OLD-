using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class UIController : MonoBehaviour
    {
    [SerializeField] UIWindowFragment[] windows;
    private HashSet<string> windowsCached = new HashSet<string>();

    private const string PREFIX_PREFAB = "(Clone)";

    private static UIController manager;

    public static UIController Manager { get => manager; }

    // Use this for initialization
    void Start()
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
        for (int i = 0; i < windows.Length; i++)
        {
            UIWindowFragment windowFragment = windows[i];
            if (Input.GetKeyDown(windowFragment.buttonPress) && !windowsCached.Contains(windowFragment.window.name + PREFIX_PREFAB))
            {
                Window window = Instantiate(windowFragment.window);
                window.onExit += WindowExit;
                windowsCached.Add(window.name);
            }
        }
        }

    private void WindowExit(Window window)
    {
        windowsCached.Remove(window.name);
    }


}