using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionMenu : MonoBehaviour
    {
    private HashSet<InteractionFragment> interactionsList = new HashSet<InteractionFragment>();

    private InteractionMenuButton prefabButtonInteraction;

    private const string PATH_PREFAB_INTERACTION_BUTTON = "Prefabs/UI/InterationMenuButton";
    [Header("Грид кнопок")]
    [SerializeField] private GridLayoutGroup gridButtons;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

    public void AddInterationFragment (InteractionFragment fragment)
    {
        LoadPrefabs();


        if (interactionsList.Contains(fragment))
        {
            throw new InteractionMenuException("fragment contains on list fragments");
        }

        interactionsList.Add(fragment);

        CreateInteractionButton(fragment);


    }

    private void LoadPrefabs ()
    {
        if (prefabButtonInteraction == null)
        {
            prefabButtonInteraction = Resources.Load<InteractionMenuButton>(PATH_PREFAB_INTERACTION_BUTTON);

            if (prefabButtonInteraction == null)
            {
                throw new InteractionMenuException("prefab interaction button not found");
            }
        }
    }

    private InteractionMenuButton CreateInteractionButton (InteractionFragment fragment)
    {
        InteractionMenuButton newButton = Instantiate(prefabButtonInteraction, gridButtons.transform);
        newButton.SetTextAction(fragment.actionName);
        newButton.AddListener(fragment.action);
        return newButton;
    }
    }