using System;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TextMeshProUGUI))]
    public class DebugMessagePythonUI : MonoBehaviour
    {
    private TextMeshProUGUI textData;
        // Use this for initialization
        void Start()
    {
        Ini();

    }

    private void Ini()
    {
        if (textData == null)
        {
            if (!TryGetComponent(out textData))
            {
                throw new DebugMessagePythonUIException($"{name} not have component TMPro GUI text");
            }
        }
    }

    public void SetText (string text)
    {
        Ini();


        DateTime timeNow = DateTime.Now;




        string dateString = $"[{timeNow.ToString("HH:MM:ss:fff")}]: {text}";
        textData.text = dateString;
    }

    public void SetMessageType (CBZ.API.Debug.LogMessageType messageType)
    {
        Ini();


        Color color = new Color();


        switch (messageType)
        {
            case CBZ.API.Debug.LogMessageType.Message:
                color = Color.white;
                break;
            case CBZ.API.Debug.LogMessageType.Error:
                color = new Color32(250, 112, 112, 255);
                break;
            case CBZ.API.Debug.LogMessageType.Warning:
                color = Color.yellow;
                break;
            default:
                throw new DebugMessagePythonUIException("invalid type message.  You must set new case variant");
        }

        textData.color = color;
    }
    }