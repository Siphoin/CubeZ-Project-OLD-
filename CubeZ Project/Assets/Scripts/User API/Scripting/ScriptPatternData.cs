using UnityEngine;

namespace CBZ.API.Scripting
{
    [CreateAssetMenu(menuName = "CBZ API/Script Pattern", order = 0)]
    public class ScriptPatternData : ScriptableObject
    {

        [Header("Название шаблона шаблона")]
        [SerializeField] string namePattern;


        [Header("Расширение файла")]
        [SerializeField] string typeFile = ".py";

        [Header("Описание паттерна")]
        [TextArea]
     [SerializeField]   private string patternDecription;

        [Header("Язык")]
        public LanguageType languageType = LanguageType.Python;


        [Header("Иконка шаблона")]
        [SerializeField] Sprite icon;

        [Header("Текстовая информация")]
        [SerializeField] TextAsset textAsset;

        public string NamePattern { get => namePattern; }
        public Sprite Icon { get => icon; }
        public string PatternDecription { get => patternDecription; }
        public string TypeFile { get => typeFile; }

        public string GetFileStrings ()
        {
            return textAsset == null ? null : textAsset.text;
        }

        public string GetLanguageTypeString ()
        {
            return languageType != LanguageType.Ciharp ? languageType.ToString() : "C#";
        }


    }
}