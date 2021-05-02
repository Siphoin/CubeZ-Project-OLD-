using System;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingMapUI : MonoBehaviour
    {
    [Header("Прогресс бар загрузки")]
    [SerializeField] private Slider progressBar;

    [Header("Текст прогресса загрузки")]
    [SerializeField] private TextMeshProUGUI textProgress;

    [Header("Текст этапа загрузки")]
    [SerializeField] private TextMeshProUGUI textProgressStep;

    private LoaderMap loaderMap;
    // Use this for initialization
    void Awake()
        {
        if (textProgress == null)
        {
            throw new LoadingMapUIException("text progress not seted");
        }

        if (textProgressStep == null)
        {
            throw new LoadingMapUIException("text progress step not seted");
        }

        if (progressBar == null)
        {
            throw new LoadingMapUIException("progress bar not seted");
        }

        if (transform.parent == null)
        {
            throw new LoadingMapUIException("parent is null");
        }

        if (!transform.parent.TryGetComponent(out loaderMap))
        {
            throw new LoadingMapUIException("parent not have component LoaderMap");
        }

        loaderMap.onProgressLoading += ShowProgressLoading;
        loaderMap.onNewStepLoading += ShowTextStepLoading;


    }

    private void ShowTextStepLoading(string text)
    {
        textProgressStep.text = text;
    }

    private void ShowProgressLoading (float value)
    {
        textProgress.text = $"{value}%";
        progressBar.value = value / 100;
    }


    private void OnDestroy()
    {
        try
        {
            loaderMap.onNewStepLoading -= ShowTextStepLoading;
            loaderMap.onProgressLoading -= ShowProgressLoading;
        }
        catch 
        {
        }
    }
}