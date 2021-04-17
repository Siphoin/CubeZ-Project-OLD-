using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
    public class FastButtonOpenWindow : MonoBehaviour
    {
        [Header("Окно при открытии")]
        [SerializeField] Window windowTargetOpen;

    private Button button;
        // Use this for initialization
        void Start()
        {

        if (UIController.Manager == null)
        {
            throw new FastButtonOpenWindowException("UI controller not found");
        }


        if (windowTargetOpen == null)
        {
            throw new FastButtonOpenWindowException("window target not seted");
        }


        if (!TryGetComponent(out button))
        {
            throw new FastButtonOpenWindowException($"{name} not have component Button");
        }

        button.onClick.AddListener(OpenWindow);
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