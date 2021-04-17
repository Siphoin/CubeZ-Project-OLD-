using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SliderBackgroundsLoading : MonoBehaviour
    {
    private const string FOLBER_BACKGROUNDS = "loading/backgrounds";

    private const string NAME_ANIM_NEW_BACKGROUND = "bg_anim_end";

    private Sprite lastBackgroundSprite = null;

    private Sprite[] backgroundsVariants;

    [SerializeField] Animator animatorBackground;

    [SerializeField] Image background;

    [SerializeField] private float timeOutBackground = 3;
        // Use this for initialization
        void Start()
        {
        if (animatorBackground == null)
        {
            throw new SliderBackgroundsLoadingException("animator background not seted");
        }
        LoadBackgroundsVariants();

        StartCoroutine(SlidingBackground());
        }


    private void LoadBackgroundsVariants()
    {
        backgroundsVariants = Resources.LoadAll<Sprite>(FOLBER_BACKGROUNDS);

        if (backgroundsVariants.Length == 0)
        {
            throw new SliderBackgroundsLoadingException("not founds variants background");
        }
    }
private IEnumerator SlidingBackground ()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeOutBackground);


            animatorBackground.Play(NAME_ANIM_NEW_BACKGROUND);
            yield return new WaitForSeconds(1.2f);


            Sprite[] backgrounds = backgroundsVariants.Where(bg => bg != lastBackgroundSprite).ToArray();
            lastBackgroundSprite = backgrounds[Random.Range(0, backgrounds.Length)];
            background.sprite = lastBackgroundSprite;
        }
    }

}