using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
public class TextShowNewDayUI : MonoBehaviour, IInvokerMono
{
    [SerializeField]   TextMeshProUGUI _thisText;

    [Header("Время исчезновения в секундах")]
    [SerializeField] private float _timeDestroy = 5;

    private Color _defaultColor;
    private Color _endColor = new Color();

    private int _currentDay = 0;

   private void Start()
        {
        if (!_thisText)
        {
            throw new TextShowNewDayUIException("Text mesh pro GUI Component not found");
        }

        _defaultColor = _thisText.color;

        CallShowTextFirst();

        _thisText.text = $"Day {_currentDay}";

        CallInvokingMethod(HideText, _timeDestroy);
    }


    public void SetDay (int dayNumber)
    {
        if (dayNumber < 0)
        {
            throw new TextShowNewDayUIException("argument day number is invalid!");
        }

        _currentDay = dayNumber;
    }

    private void Fade (Color a, Color b)
    {
        _thisText.color = a;

        _thisText.DOColor(b, 2);
    }


    private void HideText()
    {
        Fade(_defaultColor, _endColor);

        Destroy(gameObject, 2);
    }

    private void OnValidate()
    {
        if (_timeDestroy <= 0)
        {
            _timeDestroy = 5;
        }
    }

    private void CallShowTextFirst() => Fade(_defaultColor, _endColor);

    public void CallInvokingEveryMethod(Action method, float time) => InvokeRepeating(method.Method.Name, time, time);
    public void CallInvokingMethod(Action method, float time) => Invoke(method.Method.Name, time);
}