using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "Interaction/New Interaction Settings", order = 0)]
public class InteractionSettings : ScriptableObject
    {
    [Header("Список имен действий")]
    [SerializeField] InteractionData interactionData = new InteractionData();

    public string[] GetActionNames ()
    {
        if (interactionData.actionsNames.Any(str =>  str == "" || str == null || str.Trim().Length == 0))
        {
            throw new InteractionException($"found null or emtry string action name. Check list names actions. Scriptable Object {name}");
        }

        return interactionData.actionsNames;
    }

    }