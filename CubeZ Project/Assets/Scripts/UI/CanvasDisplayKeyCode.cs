using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDisplayKeyCode : MonoBehaviour
    {

    private KeyCode keyCode = KeyCode.N;
    [SerializeField] TextMeshProUGUI textKeyCode;
    [SerializeField] Image image;

    private Color transperentColor;
    private Color defaultColor;


    // Use this for initialization
    void Start()
        {
        if (textKeyCode == null)
        {
            throw new CanvasDisplayKeyCodeException("text Key Code is null");
        }

        if (image == null)
        {
            throw new CanvasDisplayKeyCodeException("image is null");
        }
        var alphaColor = image.color;
        alphaColor.a = 0;
        transperentColor = alphaColor;
        defaultColor = image.color;
        SetColorDisplay(transperentColor);
        textKeyCode.text = keyCode.ToString();
        StartCoroutine(LerpingColor());
        }

        // Update is called once per frame
        void Update()
        {

        }

    private IEnumerator LerpingColor ()
    {
        float lerpValue = 0;
        while (true)
        {
            float fpsRate = (2.0f / 60.0f) * 2;
            yield return new WaitForSeconds(fpsRate);
            lerpValue += fpsRate;
            Color lerpColor = Color.Lerp(transperentColor, defaultColor, lerpValue);
            SetColorDisplay(lerpColor);

            if (lerpValue >= 1)
            {
                yield break;
            }
        }
    }

    private void SetColorDisplay(Color color)
    {
        image.color = color;
        textKeyCode.color = color;
    }

    public void SetKeyCode (KeyCode keyCode)
    {
        this.keyCode = keyCode;
    }


    }