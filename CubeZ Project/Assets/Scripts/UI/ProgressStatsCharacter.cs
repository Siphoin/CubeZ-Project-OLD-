public class ProgressStatsCharacter : ProgressSlider
{
    private const int DEFAULT_VALUE_STATS_CHARACTER = 100;
    // Use this for initialization
    void Start()
    {
        Ini();
        SetMaxValueProgress(DEFAULT_VALUE_STATS_CHARACTER);
        UpdateProgress(DEFAULT_VALUE_STATS_CHARACTER);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetValue(int value)
    {
        UpdateText($"{value}/{DEFAULT_VALUE_STATS_CHARACTER}");
        UpdateProgress(value);
    }
}