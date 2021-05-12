using System;

public class ProgressLevel : ProgressSlider
    {
    public event Action onEndProgress;
        // Use this for initialization
        void Start()
        {
            Ini();
        }

        public void UpdateProgressLevel(long value)
        {
            Ini();
            UpdateProgress(value);

        if (Value >= MaxValue)
        {
            onEndProgress?.Invoke();
        }
        }
    }