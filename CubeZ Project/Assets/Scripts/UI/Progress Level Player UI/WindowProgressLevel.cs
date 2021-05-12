using System;
using System.Collections;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(DestroyedObject))]
    public class WindowProgressLevel : MonoBehaviour, IInvokerMono, IRemoveObject
    {

    private const string NAME_ANIM_END = "window_progress_level_end";

    [Header("Прогресс уровня")]
    [SerializeField] ProgressLevel progressLevel;

    [Header("Текст текущего уровня")]
    [SerializeField] TextMeshProUGUI textLevel;

    [Header("Задержка перед исчезновением")]
    [SerializeField] float timeOutRemove;

    private Animator animatorWindow;
    // Use this for initialization
    void Start()
        {
        if (textLevel == null)
        {
            throw new WindowProgressLevelException("text current level not seted");
        }

        if (progressLevel == null)
        {
            throw new WindowProgressLevelException("progress level not seted");
        }

        if (!TryGetComponent(out animatorWindow))
        {
            throw new WindowProgressLevelException($"{name} not have component Animator");
        }

        progressLevel.onEndProgress += ProgressLevel_onEndProgress;

        UpdateData();
        CallInvokingMethod(Remove, timeOutRemove);
    }

    private void ProgressLevel_onEndProgress()
    {
        progressLevel.SetMaxValueProgress(GameCacheManager.gameCache.levelProgressPlayer.currentXPForNextLevel);
        progressLevel.UpdateProgressLevel(0);
        UpdateTextLevel();
    }

    private void UpdateTextLevel ()
    {
        textLevel.text = $"Level {GameCacheManager.gameCache.levelProgressPlayer.currentLevel}";
    }

    public void CallInvokingEveryMethod(Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    public void CallInvokingMethod(Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }

    public void Remove()
    {
        animatorWindow.Play(NAME_ANIM_END);
    }

    public void UpdateData ()
    {
        progressLevel.SetMaxValueProgress(GameCacheManager.gameCache.levelProgressPlayer.currentXPForNextLevel);
        progressLevel.UpdateProgressLevel(GameCacheManager.gameCache.levelProgressPlayer.currentXP);
        UpdateTextLevel();
    }

}