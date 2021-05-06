using System.Collections;
using UnityEngine;
using TMPro;
namespace CBZ.API
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Text : APIObject, IAPIObject
    {
        private TextMeshProUGUI targetText;

        public string text { get => targetText.text; set => targetText.text = value; }
        public float Size { get => targetText.fontSize; set => targetText.fontSize = value; }
        // Use this for initialization
        void Awake()
        {
            Ini();
        }

        public void Ini()
        {
            targetText = GetComponent<TextMeshProUGUI>();
        }

        public void SetColor (Color color)
        {
            targetText.color = color.ToUnityColor();
        }

        public void SetColor(Color32 color)
        {
            targetText.color = color.ToUnityColor32();
        }



    }
}