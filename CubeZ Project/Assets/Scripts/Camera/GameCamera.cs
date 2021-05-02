using UnityEngine;

public class GameCamera : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float smooth = 5.0f;
    private Vector3 offset;

   public static GameCamera Main {  get; private set; }

    private void Awake()
    {
        Main = this;
    }
    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {
        if (target != null)
        {
        Vector3 newpos = target.transform.position + offset;
        transform.position = newpos;
        }

    }

    public void SetTarget (Transform target)
    {
        if (target == null)
        {
            throw new GameCameraException("target is null");
        }

        this.target = target;
        offset = transform.position - target.transform.position;
    }

    public void CentringToTarget (Transform target)
    {
        if (target == null)
        {
            return;
        }

        Vector3 pos = transform.position;
        pos.x = target.transform.position.x;
        pos.z = target.transform.position.z -  1f;
        transform.position = pos;
    }
}