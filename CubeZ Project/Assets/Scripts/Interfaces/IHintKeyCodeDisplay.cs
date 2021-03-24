using UnityEngine;

public interface IHintKeyCodeDisplay
{
    CanvasDisplayKeyCode ShowHintKeyCode(KeyCode keyCode, Transform targetTransform);
    void DestroyHintKeyCode();

}