using UnityEngine;

public class PauseWindow : Window
    {
    private const string NAME_SCENE_MAIN_MENU = "main_menu";
    private float oldTimeScale;
        // Use this for initialization
        void Start()
        {
        oldTimeScale = Time.timeScale;
        SetValueScaleTime(0);
        }

      public void OpenSettings ()
    {

    }

    public void ContinueSession ()
    {
        Exit();
    }

    public void QuitToWindows()
    {
        Application.Quit();
    }

    public void BackToMenu ()
    {
        SetValueScaleTime(1);
        Loading.LoadScene(NAME_SCENE_MAIN_MENU);
    }

    private void SetValueScaleTime (float value)
    {
        Time.timeScale = value;
    }

    public override void Exit()
    {
        SetValueScaleTime(oldTimeScale);
        base.Exit();
    }
}