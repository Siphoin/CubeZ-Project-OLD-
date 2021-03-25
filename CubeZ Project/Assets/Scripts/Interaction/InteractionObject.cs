using UnityEngine;

public class InteractionObject : MonoBehaviour
    {
    protected InteractionMenu interactionMenuPrefab;
    protected InteractionMenu activeIntegrationMenu;

    private const string PATH_PREFAB_INTERATION_MENU = "Prefabs/UI/InterationMenu";


    protected const string TAG_PLAYER = "MyPlayer";

    protected Character enteredPlayer;


    [Header("Данные по взаимодействию с игроком")]
    [SerializeField] protected InteractionSettings interactionSettings;

    protected void Ini ()
    {
        if (interactionSettings == null)
        {
            throw new InteractionObjectException("interaction settings not seted");
        }
        interactionMenuPrefab = Resources.Load<InteractionMenu>(PATH_PREFAB_INTERATION_MENU);

        if (interactionMenuPrefab == null)
        {
            throw new InteractionObjectException("interaction menu not found");
        }
    }
    }