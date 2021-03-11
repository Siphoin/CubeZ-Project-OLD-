using UnityEngine;

    public class CanvasLockAt : MonoBehaviour
    {
        private Camera cam;
        private void LateUpdate()
        {
            transform.LookAt(transform.position + cam.transform.forward);
        
        }

        private void Start()
        {
            cam = Camera.main;
        }
    }