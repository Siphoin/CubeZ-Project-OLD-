using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Control/Control Manager", order = 0)]
public class ControlManager : ScriptableObject
    {
    [Newtonsoft.Json.JsonRequired, SerializeField]
    private List<ControlFragment> controlFragments = new List<ControlFragment>(1);

    public KeyCode GetKeyCodeByFragment (string triggerName)
    {
        try
        {
        return controlFragments.Single(item => item.nameTrigger == triggerName).keyCode;
        }

        catch
        {
            throw new ControlManagerException($"Trigger Name {triggerName} not found");
        }

    }
    }