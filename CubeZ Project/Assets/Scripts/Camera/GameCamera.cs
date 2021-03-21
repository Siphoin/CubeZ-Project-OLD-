using UnityEngine;

public class GameCamera : MonoBehaviour, IFinderPlayer
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
        FindPlayer();
        offset = transform.position - target.transform.position;
    }

    public void FindPlayer()
    {
        if (PlayerManager.Manager == null)
        {
            throw new GameCameraException("Player manager not found");
        }
        else if (PlayerManager.Manager.Player == null)
        {
            throw new GameCameraException("Player not found");
        }

        SetTarget(PlayerManager.Manager.Player.transform);
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
}