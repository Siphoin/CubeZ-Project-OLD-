using System;
using System.Collections;
using UnityEngine;

    public class ZombieEyeController : MonoBehaviour
    {
    [SerializeField] Light firstEye;
    [SerializeField] Light secondEye;

    private const float INTENSITY_DEFAULT = 3.0F;
        // Use this for initialization
        void Start()
        {
        if (firstEye == null)
        {
            throw new ZombieEyeControllerException("first light eye not seted");
        }
        if (secondEye == null)
        {
            throw new ZombieEyeControllerException("second light eye not seted");
        }
        OffLightEye();

        WorldManager.Manager.onDayChanged += NewDayListener;

        }

    private void NewDayListener(DayTimeType day)
    {
        StartCoroutine(LerpingEyesLight(day));
    }

    // Update is called once per frame
    void Update()
        {

        }

    private void OffLightEye ()
    {
        SetIntensityEyes(0);
    }

    private IEnumerator LerpingEyesLight (DayTimeType day)
    {
        float lerpValue = 0;
        float intensityValue = 0;
        float AValue = day == DayTimeType.Night ? 0 : INTENSITY_DEFAULT;
        float BValue = day == DayTimeType.Night ? INTENSITY_DEFAULT : 0;
        while (true)
        {
            float fpsRate = 1.0f / 60.0f;
            yield return new WaitForSeconds(fpsRate);
            lerpValue += fpsRate;
            intensityValue = Mathf.Lerp(AValue, BValue, lerpValue);
            SetIntensityEyes(intensityValue);
            if (lerpValue >= 1)
            {
                yield break;
            }

        }
    }

    private void SetIntensityEyes (float value)
    {
        secondEye.intensity = value;
        firstEye.intensity = value;
    }


    }