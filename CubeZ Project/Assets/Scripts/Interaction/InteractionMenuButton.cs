using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
    public class InteractionMenuButton : MonoBehaviour
    {

    [SerializeField] TextMeshProUGUI textButton;
    public event UnityAction onPressed;

    private Button thisButton;
        // Use this for initialization
        void Start()
        {
        if (!TryGetComponent(out thisButton))
        {
            throw new InteractionMenuButtonException("not found component UnityEnginw.UI.Button");
        }

        if (textButton == null)
        {
            throw new InteractionMenuButtonException("text button not seted");
        }
        }

        // Update is called once per frame
        void Update()
        {

        }

    public void AddListener (UnityAction action)
    {
        if (!TryGetComponent(out thisButton))
        {
            throw new InteractionMenuButtonException("not found component UnityEnginw.UI.Button");
        }
        onPressed += action;
        thisButton.onClick.AddListener(onPressed);
        Debug.Log($"New litener added. Method Name: {onPressed.Method.Name}()");
    }

    public void SetTextAction (string text)
    {
        textButton.text = text;
    }


    }