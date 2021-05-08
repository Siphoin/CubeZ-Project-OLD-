using System.Collections;
using UnityEngine;
using CBZ.API.Scripting;
using UnityEngine.UI;
using System;
using TMPro;

[RequireComponent(typeof(Button))]
    public class PatternScriptElementUI : MonoBehaviour
    {
    private ScriptPatternData scriptPatternData = null;

    private Button button;

    public event Action<ScriptPatternData> onClick;

    [Header("Текст имени шаблона")]
    [SerializeField] private TextMeshProUGUI textName;

    [Header("Изображение шаблона")]
    [SerializeField] private Image icon;
    // Use this for initialization
    void Start()
    {
        Ini();
    }

    private void Ini()
    {
        if (icon == null)
        {
            throw new PatternScriptElementUIException("icon not seted");
        }

        if (textName == null)
        {
            throw new PatternScriptElementUIException("text name not seted");
        }

        if (button == null)
        {
            if (!TryGetComponent(out button))
            {
                throw new PatternScriptElementUIException($"{name} not have component UnityEngine.UI.Button");
            }
            button.onClick.AddListener(Click);

        }
    }

    private void Click ()
    {
        onClick?.Invoke(scriptPatternData);
    }

 public void SetData (ScriptPatternData targetData)
    {
        if (targetData == null)
        {
            throw new PatternScriptElementUIException("target data is null");
        }

        scriptPatternData = targetData;
        Ini();

        textName.text = scriptPatternData.NamePattern;
        icon.sprite = scriptPatternData.Icon;
    }



    }