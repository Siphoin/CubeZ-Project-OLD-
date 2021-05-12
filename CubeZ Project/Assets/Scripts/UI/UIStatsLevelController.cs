using TMPro;
using UnityEngine;

public class UIStatsLevelController : MonoBehaviour
    {

    private const string NAME_ANIM_NEW_LEVEL_TEXT = "new_level_text";

    [Header("Текст уровня")]
    [SerializeField] private TextMeshProUGUI textLevel;

    private Animator animatorText;
        // Use this for initialization
        void Start()
        {
        if (ListenerLevelProgressionLocalPlayer.Manager == null)
        {
            throw new UIStatsLevelControllerException("listener progression level local player not found");
        }
        if (textLevel == null)
        {
            throw new UIStatsLevelControllerException("text level not seted");
        }

        if (!textLevel.TryGetComponent(out animatorText))
        {
            throw new UIStatsLevelControllerException("text level not have component Animator");
        }

        ListenerLevelProgressionLocalPlayer.Manager.onLevelUp += UpdateTextLevel;
        UpdateTextLevel();
        }

    private void UpdateTextLevel()
    {
        animatorText.Play(NAME_ANIM_NEW_LEVEL_TEXT);
        textLevel.text = GameCacheManager.gameCache.levelProgressPlayer.currentLevel.ToString();
    }
}