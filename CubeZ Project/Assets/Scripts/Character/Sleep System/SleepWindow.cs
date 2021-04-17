using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SleepWindow : Window
    {
    [SerializeField] private Image imageClockSquare;
    [SerializeField] private Image imageClock;
    [SerializeField] private Image background;

    private Vector3 vectorRotate = new Vector3(0, 0, -1);

    private Color defaultColorBackground;
    private Color defaultColorClockAndSquare;

    private Color alphaColorBackground;
    private Color alphaColorClockAndSquare;

    private const float SPEED_ROTATE_CLOCK = 150;
        // Use this for initialization
        void Start()
    {
        if (background == null)
        {
            throw new SleepWindowException("background is null");
        }

        if (imageClockSquare == null)
        {
            throw new SleepWindowException("image is null");
        }

        if (imageClock == null)
        {
            throw new SleepWindowException("image Clock is null");
        }

        if (PlayerManager.Manager == null)
        {
            throw new SleepWindowException("player manager not found");
        }


        if (PlayerManager.Manager.Player == null)
        {
            throw new SleepWindowException("player not found");
        }

        if (UIController.Manager == null)
        {
            throw new SleepWindowException("UI Controller not found");
        }


        PlayerManager.Manager.Player.onSleep += PlayerSleeping;
        FrezzePlayer();

        defaultColorBackground = background.color;
        defaultColorClockAndSquare = imageClock.color;
        alphaColorBackground = GetAlphaColor(defaultColorBackground);
        alphaColorClockAndSquare = GetAlphaColor(defaultColorClockAndSquare);
        SetColorClock(alphaColorClockAndSquare);
        SetColorBackground(alphaColorBackground);

        LerpingColor(true);
    }


    private void Awake()
    {
        
    }

    private void PlayerSleeping(bool isSleeping)
    {
        if (!isSleeping)
        {
            LerpingColor(isSleeping);
        }
    }

    private void SetColorClock(Color color)
    {
        imageClock.color = color;
        imageClockSquare.color = color;
    }

    private void LerpingColor (bool state)
    {
        StartCoroutine(LerpingColorWindow(state));
    }

    private void SetColorBackground (Color color)
    {
        background.color = color;
    }

    // Update is called once per frame
    void Update()
        {
        imageClockSquare.transform.Rotate(vectorRotate * SPEED_ROTATE_CLOCK * Time.deltaTime);
        }

     private IEnumerator LerpingColorWindow (bool startState)
    {
        float lerpValue = 0;
        while (true)
        {
            float fpsRate = 1.0f / 60.0f;
            yield return new WaitForSeconds(fpsRate);
            lerpValue += fpsRate;
            Color alphaWhiteLerp = new Color();
            Color alphaBlackLerp = new Color();


             alphaWhiteLerp = startState == true ? Color.Lerp(alphaColorClockAndSquare, defaultColorClockAndSquare, lerpValue) : Color.Lerp(defaultColorBackground, alphaColorClockAndSquare, lerpValue);
             alphaBlackLerp = startState == true ? Color.Lerp(alphaColorBackground, defaultColorBackground, lerpValue) : Color.Lerp(defaultColorBackground, alphaColorBackground, lerpValue);
            SetColorClock(alphaWhiteLerp);
            SetColorBackground(alphaBlackLerp);


            if (!startState)
            {
            if (imageClock.color.a  == 0 && background.color.a == 0)
            {
                    Exit();
            }
            }

            if (lerpValue >= 1f)
            {
                yield break;
            }
        }
    }

    private Color GetAlphaColor (Color originalColor)
    {
        var alphaColor = originalColor;
        alphaColor.a = 0;
        return alphaColor;
    }

    public override void Exit()
    {
        PlayerManager.Manager.Player.onSleep -= PlayerSleeping;
        base.Exit();
    }

    private void OnDestroy()
    {
        try
        {
        }
        catch
        {

        }
    }

}