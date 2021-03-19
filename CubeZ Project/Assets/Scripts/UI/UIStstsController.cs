using UnityEngine;
public class UIStstsController : MonoBehaviour
{
    [Header("Прогрессы потребностей")]


    [Header("Прогресс голода")]
    [SerializeField] ProgressStatsCharacter progressHunger;

    [Header("Прогресс сна")]
    [SerializeField] ProgressStatsCharacter progressSleep;

    [Header("Прогресс выносливости")]
    [SerializeField] ProgressStatsCharacter progressRun;

    [Header("Прогресс здоровья")]
    [SerializeField] ProgressStatsCharacter progressHealth;

    [Header("Прогресс температуры тела")]
    [SerializeField] ProgressStatsCharacter progressTemperature;


    [SerializeField, ReadOnlyField] CharacterStatsController character;

    private const string TAG_PLAYER = "MyPlayer";

    private float fpsRate = 0;
    // Use this for initialization
    void Start()
    {
        if (progressHealth == null || progressSleep == null || progressTemperature == null || progressRun == null || progressHunger == null)
        {
            throw new UIStatsControllerException("not all progress stats be seted!");
        }


        character = GameObject.FindGameObjectWithTag(TAG_PLAYER).GetComponent<CharacterStatsController>();

        SetObserversStats();
    }

    private void SetObserversStats()
    {
        character.Hunger.onValueChanged += ShowHungerProgress;
        character.Sleep.onValueChanged += ShowSleepProgress;
        character.Run.onValueChanged += ShowRunProgress;
        character.Health.onValueChanged += ShowHealthProgress;
        character.TemperatureBody.onValueChanged += ShowTemperatureProgress;
    }

    private void ShowHealthProgress(int value)
    {
        SetProgressValue(progressHealth, value);
    }

    private void ShowRunProgress(int value)
    {
        SetProgressValue(progressRun, value);
    }

    private void ShowSleepProgress(int value)
    {
        SetProgressValue(progressSleep, value);
    }

    private void ShowHungerProgress(int value)
    {
        SetProgressValue(progressHunger, value);
    }

    private void ShowTemperatureProgress(int value)
    {
        SetProgressValue(progressTemperature, value);
    }

    private void SetProgressValue(ProgressStatsCharacter progress, int value)
    {
        progress.SetValue(value);
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        fpsRate = 1.0f / Time.unscaledDeltaTime;
        GUILayout.Label("FPS: " + (int)fpsRate);
    }

    // control value stats a progresses


}
