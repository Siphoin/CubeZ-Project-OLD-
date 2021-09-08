using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Window : MonoBehaviour
{
    private const float SPEED_SCALE_ANIMATION = 0.5f;


    public event Action<Window> OnExit;

    protected Character _player;


    [Header("Окно, которое будет проигрывать анимацию (можно оставить пустым)")]
    [SerializeField] private GameObject _windowAnim;

    void Start() => FrezzePlayer();

    protected void FrezzePlayer()
    {
        if (PlayerManager.Manager == null)
        {
            throw new GameWindowException("_player manager not found");
        }

        else
        {
            _player = PlayerManager.Manager.Player;

            if (_player == null)
            {
                throw new GameWindowException("_player not found");
            }
            _player.FrezzeCharacter();
        }
    }


    public virtual void Exit()
    {
        if (_player != null)
        {
        _player.ActivateCharacter();
        }

        OnExit?.Invoke(this);

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
        if (_windowAnim)
        {
            _windowAnim.transform.localScale = new Vector3();

            _windowAnim.transform.DOScale(1, SPEED_SCALE_ANIMATION);
        }
    }


    private void SetVisibleAnimWindow (bool state)
    {
        _windowAnim.gameObject.SetActive(state);
    }
}