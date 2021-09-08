using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
    public class GameButtonBase : MonoBehaviour
    {
    public event Action OnClick;

    private Button _button;

    private delegate void Click();

    private Click _click;


    protected Button Button => _button;

    public virtual void Init ()
    {
        if (!_button)
        {
        if (!TryGetComponent(out _button))
        {
            throw new NullReferenceException("button menu not have cxomponent UnityEngine.UI.Button");
        }

        _click += ClickEvent;

            _button.onClick.AddListener(() => _click());
        }    
    }

    public void AddListener(UnityAction action)
    {
        Init();

        _click += action.Invoke;
    }

    public void RemoveAllListeners()
    {
        Init();

        _click = null;
    }


    private void Start() => Init();

    private void ClickEvent() => OnClick?.Invoke();

    public void RemoveListener(UnityAction action) => _click -= action.Invoke;

    public void SetInteractable(bool interactable)
    {
        Init();

        _button.interactable = interactable;
    }
}