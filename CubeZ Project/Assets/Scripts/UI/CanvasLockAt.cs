using UnityEngine;

public class CanvasLockAt : MonoBehaviour
{
    private Camera cam;
    private void Update()
    {
        transform.forward = Camera.main.transform.forward;

    }

    private void Start()
    {
        cam = Camera.main;
    }
}