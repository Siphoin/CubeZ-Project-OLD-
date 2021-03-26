using System;
using System.Collections;
using TMPro;
using UnityEngine;
    public class TextShowNewDayUI : MonoBehaviour, IInvokerMono
{
 [SerializeField]   TextMeshProUGUI thisText;

    [Header("Время исчезновения в секундах")]
    [SerializeField] private float timeDestroy = 5;

    private Color defaultColor;
    private Color endColor = new Color();

    private int currentDay = 0;

    // Use this for initialization
    void Start()
        {
        if (thisText == null)
        {
            throw new TextShowNewDayUIException("Text mesh pro GUI Component not found");
        }

        if (timeDestroy < 0)
        {
            throw new TextShowNewDayUIException("time destroy text is invalid!");
        }

        defaultColor = thisText.color;
        var startColor = defaultColor;
        startColor.a = 0;
        thisText.color = startColor;
        CallShowTextFirst();
        CallInvokingMethod(HideText, timeDestroy);
        thisText.text = $"Day {currentDay}";
    }

        // Update is called once per frame
        void Update()
        {
        }


    public void SetDay (int dayNumber)
    {
        if (dayNumber < 0)
        {
            throw new TextShowNewDayUIException("argument day number is invalid!");
        }
        currentDay = dayNumber;
    }

    private IEnumerator LerpingColor (Color startColor, Color endColor)
    {
        float lerpValue = 0;
        while (true)
        {
            float fpsRate = 1.0f / 60.0f;
            yield return new WaitForSeconds(fpsRate);
            lerpValue += fpsRate;
            thisText.color = Color.Lerp(startColor, endColor, lerpValue);

            if (lerpValue >= 1)
            {

                if (thisText.color.a == 0 && thisText.color == this.endColor)
                {
                    Destroy(gameObject);
                }
                yield break;
            }
        }
    }

    public void CallInvokingEveryMethod(Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    public void CallInvokingMethod(Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }

    private void CallShowTextFirst ()
    {
        var endColor = defaultColor;
        endColor.a = 0;
        StartCoroutine(LerpingColor(endColor, defaultColor));
    }

    private void HideText()
    {
        StartCoroutine(LerpingColor(defaultColor, endColor));
    }
}