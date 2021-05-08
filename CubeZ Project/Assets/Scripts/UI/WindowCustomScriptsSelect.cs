using System.Collections;
using UnityEngine;
using TMPro;
using CBZ.API.Scripting;
using System;
using UnityEngine.UI;
using System.IO;
using System.Diagnostics;

public class WindowCustomScriptsSelect : Window
    {
    private const string PATH_PREFAB_SCRIPT_PATTERN_ELEMENT = "Prefabs/UI/PatternScriptElement";

    private const string PATH_DATA_SCRIPT_PATTERNS = "cbz_api/ScriptsPatterns";

    private const string PATH_FOLBER_SCRIPTS = "localData/session/scripts";

    private const string SPECIFIX_STRING_TYPE = "<b>Type:</b>";

    [Header("Контейнер информации о паттерне")]
    [SerializeField] private GameObject containerInfo;

    [Header("Контейнер списка паттернов")]
    [SerializeField] private Transform containerScriptsPatterns;


    [Header("Поле для ввода имени файла")]
    [SerializeField] private TMP_InputField inputFieldFileName;

    [Header("Текст описания паттерна")]
    [SerializeField] private TextMeshProUGUI textInfoPattern;

    [Header("Текст описания паттерна")]
    [SerializeField] private TextMeshProUGUI textTypePattern;

    [Header("Кнопка создать файл паттерна")]
    [SerializeField] private Button buttonCreateFile;

    private PatternScriptElementUI patternScriptElementUIPrefab;

    private ScriptPatternData[] patterns;

    private ScriptPatternData patternSelected;
    // Use this for initialization
    void Start()
        {
        if (containerScriptsPatterns == null)
        {
            throw new WindowCustomScriptsSelectException("container scripts patterns not seted");
        }

        if (containerInfo == null)
        {
            throw new WindowCustomScriptsSelectException("container info not seted");
        }

        if (inputFieldFileName == null)
        {
            throw new WindowCustomScriptsSelectException("input field file name not seted");
        }


        if (textInfoPattern == null)
        {
            throw new WindowCustomScriptsSelectException("text info pattern not seted");
        }

        if (textTypePattern == null)
        {
            throw new WindowCustomScriptsSelectException("text type pattern not seted");
        }

        if (buttonCreateFile == null)
        {
            throw new WindowCustomScriptsSelectException("");
        }

        patternScriptElementUIPrefab = Resources.Load<PatternScriptElementUI>(PATH_PREFAB_SCRIPT_PATTERN_ELEMENT);

        if (patternScriptElementUIPrefab == null)
        {
            throw new WindowCustomScriptsSelectException("prefab pattern script element not found");
        }

        patterns = Resources.LoadAll<ScriptPatternData>(PATH_DATA_SCRIPT_PATTERNS);

        if (patterns == null || patterns.Length == 0)
        {
            throw new WindowCustomScriptsSelectException("patterns data not found");
        }

        SetStateVisibleInfoPattern(false);

        buttonCreateFile.onClick.AddListener(CreateFilePattern);

        for (int i = 0; i < patterns.Length; i++)
        {
            PatternScriptElementUI element = Instantiate(patternScriptElementUIPrefab, containerScriptsPatterns);
            element.SetData(patterns[i]);
            element.onClick += SelectPattern;
        }
    }

    private void SelectPattern(ScriptPatternData data)
    {
        SetStateVisibleInfoPattern(true);
        textInfoPattern.text = data.PatternDecription;
        textTypePattern.text = $"{SPECIFIX_STRING_TYPE} {data.GetLanguageTypeString()}";
        patternSelected = data;
    }

    private void SetStateVisibleInfoPattern (bool status)
    {
        containerInfo.SetActive(status);
        buttonCreateFile.interactable = status;
    }

    private void CreateFilePattern ()
    {
        if (string.IsNullOrEmpty(inputFieldFileName.text.Trim()))
        {
            return;
        }


        string path = CacheSystem.GetPathAssetsData() + PATH_FOLBER_SCRIPTS;


        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string resultNameFile = inputFieldFileName.text.Trim() + patternSelected.TypeFile;

        string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].Contains(resultNameFile))
            {
                return;
            }
        }
        string pathToFile = $"{path}/{resultNameFile}";
        File.WriteAllText(pathToFile, patternSelected.GetFileStrings());

        Process.Start(pathToFile);

        Exit();
    }
}