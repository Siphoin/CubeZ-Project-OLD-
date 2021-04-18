using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
    public class FastButtonOpenWindow : MonoBehaviour
    {
        [Header("Окно при открытии")]
        [SerializeField] Window windowTargetOpen;

    [Header("Текст подсказка клавиши")]
    [SerializeField] TextMeshProUGUI textKeyCode;

    [Header("Индентификатор кода клавиши для открытия окна")]
    [SerializeField] string nameTriggerWindow;

    private ControlManager controlManager;

    private Button button;
        // Use this for initialization
        void Start()
        {

        if (UIController.Manager == null)
        {
            throw new FastButtonOpenWindowException("UI controller not found");
        }

        if (ControlManagerObject.Manager == null)
        {
            throw new FastButtonOpenWindowException("Control manager object not found");
        }



        if (windowTargetOpen == null)
        {
            throw new FastButtonOpenWindowException("window target not seted");
        }

        if (string.IsNullOrEmpty(nameTriggerWindow))
        {
            throw new FastButtonOpenWindowException("name trigger open window is emtry");
        }

        if (!TryGetComponent(out button))
        {
            throw new FastButtonOpenWindowException($"{name} not have component Button");
        }

        button.onClick.AddListener(OpenWindow);

        controlManager = ControlManagerObject.Manager.ControlManager;

        textKeyCode.text = controlManager.GetKeyCodeByFragment(nameTriggerWindow).ToString();
        }

    private void OpenWindow()
    {
        if (UIController.Manager == null)
        {
            throw new System.Exception("ui controller not found");
        }
        if (UIController.Manager.ActiveWindow != null)
        {
            return;
        }


        UIController.Manager.OpenWindow(windowTargetOpen);
    }

    }