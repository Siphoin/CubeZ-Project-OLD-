using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Canvas))]
public class CanvasDisplayKeyCode : MonoBehaviour, IFaderImage
    {

    private KeyCode keyCode = KeyCode.N;
    [SerializeField] TextMeshProUGUI textKeyCode;
    [SerializeField] Image image;

    private Canvas canvas;




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

        if (!TryGetComponent(out canvas))
        {
            throw new NullReferenceException("canvas reference not seted on canvas display key code");
        }

        textKeyCode.text = keyCode.ToString();

        textKeyCode.color = new Color();
        textKeyCode.DOColor(Color.white, 1);
        textKeyCode.DOFade(1, 2);
        FadeImage(image, Color.white, 1);
        }


    public void SetKeyCode (KeyCode keyCode)
    {
        this.keyCode = keyCode;
    }

    public void FadeImage(Image image, Color color, float speed)
    {
        image.color = new Color();
        image.DOColor(color, speed);
        image.DOFade(1, speed);
    }
}