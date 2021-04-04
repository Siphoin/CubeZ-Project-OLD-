﻿using System;

[Serializable]
    public struct HealthData
    {
 [UnityEngine.SerializeField]   private int value;
    private int startValue;

    public event Action onHealthChanged;

    public int Value { get => value; }
    public int StartValue { get => startValue; }

    public HealthData (int value)
    {
        this.value = value;
        startValue = value;
        onHealthChanged = null;
    }

    public void Hill (int value)
    {
        this.value += value;
        CheckHealthRange();

    }

    public void Damage (int value)
    {
        this.value -= value;
        CheckHealthRange();
        CallEventChangedHealth();
    }

    private void CheckHealthRange ()
    {
        if (value >= startValue)
        {
            value = startValue;
        }

        if (value < 0)
        {
            value = 0;
        }


    }

    private void CallEventChangedHealth ()
    {
        onHealthChanged?.Invoke();
    }

    }