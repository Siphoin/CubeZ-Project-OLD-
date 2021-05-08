using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DebugPythonUI : Window, ISenderMessagePythonDebuger
    {
        private const string PATH_PREFAB_TEXT_MESSAGE = "Prefabs/UI/logText";

    private DebugMessagePythonUI messagePrefab;

    public event Action onClearConsole;

    

    [Header("Контейнер сообщений")]

    [SerializeField] private Transform contentMessages;

    [Header("Кнопка очистить консоль")]

    [SerializeField] private Button buttonClear;
    // Use this for initialization
    void Start()
    {
        Ini();

    }

    public void Ini()
    {
        if (contentMessages == null)
        {
            throw new DebugPythonUIException("content Messages not seted");
        }
        if (buttonClear == null)
        {
            throw new DebugPythonUIException("button clear not seted");
        }

        messagePrefab = Resources.Load<DebugMessagePythonUI>(PATH_PREFAB_TEXT_MESSAGE);

        if (messagePrefab == null)
        {
            throw new DebugPythonUIException("message prefab not found");
        }

        buttonClear.onClick.AddListener(Clear);
    }

    public void Clear ()
    {
        for (int i = 0; i < contentMessages.childCount; i++)
        {
            GameObject obj = contentMessages.GetChild(i).gameObject;
            Destroy(obj);
        }

        onClearConsole?.Invoke();
    }

    public void NewMessage (string text, CBZ.API.Debug.LogMessageType messageType = CBZ.API.Debug.LogMessageType.Message)
    {
        DebugMessagePythonUI message = Instantiate(messagePrefab, contentMessages);
        message.SetMessageType(messageType);
        message.SetText(text);

    }

    }