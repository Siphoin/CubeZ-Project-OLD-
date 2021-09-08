using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsLevelController : MonoBehaviour, IFaderImage
    {

    private const string NAME_ANIM_NEW_LEVEL_TEXT = "new_level_text";

    [Header("Текст уровня")]
    [SerializeField] private TextMeshProUGUI _textLevel;


    private  void Start()
        {
        if (!ListenerLevelProgressionLocalPlayer.Manager)
        {
            throw new UIStatsLevelControllerException("listener progression level local player not found");
        }
        if (!_textLevel)
        {
            throw new UIStatsLevelControllerException("text level not seted");
        }

        ListenerLevelProgressionLocalPlayer.Manager.onLevelUp += UpdateTextLevel;

        UpdateTextLevel();
        }

    public void FadeImage(Image image, Color color, float time)
    {
        image.color = new Color();
        image.DOColor(color, 2);
        image.DOFade(1, 2);
    }

    private void UpdateTextLevel() => _textLevel.text = GameCacheManager.gameCache.levelProgressPlayer.currentLevel.ToString();
}