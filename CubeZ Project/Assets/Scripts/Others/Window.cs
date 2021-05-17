using System;
using System.Collections;
using UnityEngine;

public class Window : MonoBehaviour
{
    
    public event Action<Window> onExit;

    protected Character player;

    [Header("Анимация отурытия")]
    [SerializeField] private bool useAnimOpen;

    [Header("Окно, которое будет проигрывать анимацию (можно оставить пустым)")]
    [SerializeField] private GameObject windowAnim;
    // Use this for initialization
    void Start()
    {

        FrezzePlayer();
    }

    protected void FrezzePlayer()
    {
        if (PlayerManager.Manager == null)
        {
            throw new GameWindowException("Player manager not found");
        }

        else
        {
            player = PlayerManager.Manager.Player;

            if (player == null)
            {
                throw new GameWindowException("Player not found");
            }
            player.FrezzeCharacter();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Exit()
    {
        if (player != null)
        {
        player.ActivateCharacter();
        }

        onExit?.Invoke(this);

        try
        {
            Destroy(gameObject);
        }
        catch
        {

        }
    }

    protected void Ini ()
    {
        if (useAnimOpen)
        {
            windowAnim = windowAnim == null ? gameObject : windowAnim;
            StartCoroutine(AnimationOpen());
        }
    }

    private IEnumerator AnimationOpen ()
    {
        float lerpValue = 0;
        float time = 1.0f / 60.0f * 4;


        Vector3 scaleWindow = windowAnim.transform.localScale;

        SetVisibleAnimWindow(false);


        while (true)
        {
            yield return new WaitForSeconds(time * Time.deltaTime);


            if (!windowAnim.activeSelf)
            {
                SetVisibleAnimWindow(true);
            }
            lerpValue += time;

            windowAnim.transform.localScale = Vector3.Lerp(Vector3.zero, scaleWindow, lerpValue);

            if (lerpValue >= 1.0f)
            {
                yield break;
            }

        }
    }

    private void SetVisibleAnimWindow (bool state)
    {
        windowAnim.gameObject.SetActive(state);
    }
}