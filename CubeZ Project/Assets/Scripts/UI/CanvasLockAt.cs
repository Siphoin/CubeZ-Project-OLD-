using UnityEngine;

public class CanvasLockAt : MonoBehaviour
{
    private Camera cam;
    [Header("Target")]
   [SerializeField] private Transform target;
    [Header("Offset")]
    [SerializeField] private float yOffset = 0;
    [SerializeField] private float zOffset = 0;
    [SerializeField] private float xOffset = 0;

    private Vector3 oldPosition;

    public bool DestroyIfParentOnDestroy { get; set; } = true;

    private bool targetSeted = false;

    private void Update()
    {
        transform.LookAt(transform.position + cam.transform.forward);

        if (target != null)
        {
            if (oldPosition != target.position)
            {
                oldPosition = target.position;
                transform.position = target.position;
                var posWithOffset = new Vector3( xOffset + transform.position.x, yOffset + transform.position.y, zOffset + transform.position.z);
                transform.position = posWithOffset;
            }
        }

        if (target == null && targetSeted && DestroyIfParentOnDestroy)
        {
            Destroy(gameObject);
        }
       

    }

    private void Start()
    {
        cam = Camera.main;

        if (target != null)
        {
            targetSeted = true;
        }
        {

        }
    }

    public void SetTarget (Transform target)
    {
        if (target == null)
        {
            throw new CanvasLockAtException("target is null");
        }

        targetSeted = true;
        this.target = target;
    }
}