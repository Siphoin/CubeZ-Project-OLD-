using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using TMPro;
public class ButtonMenu : GameButtonBase
    {
    private Image _backgroundButton;

  [SerializeField]  private TextMeshProUGUI _text;
       private void Start()
    {
        Init();

        if (!_text)
        {
            throw new NullReferenceException("button menu not have refrence Text");
        }

        if (!Button.TryGetComponent(out _backgroundButton))
        {
            throw new NullReferenceException("game button noty have component UnityEngine.UI.Image");
        }
        TurnAnimation();
    }

    private void TurnAnimation()
    {
        Color originalColor = _backgroundButton.color;

        _backgroundButton.color = new Color();

        _backgroundButton.DOFade(1, 2);

        _backgroundButton.DOColor(originalColor, 2);

        _text.color = new Color();

        _text.DOColor(Color.white, 2);
    }
}