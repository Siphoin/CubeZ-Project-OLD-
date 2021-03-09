using System.Collections;
using UnityEngine;

    public class GameCamera : MonoBehaviour, IFinderPlayer
    {
    private Character target;
  [SerializeField]  private float smooth = 5.0f;
    private Vector3 offset;

    private const string TAG_PLAYER = "Player";

    // Use this for initialization
    void Start()
    {
        FindPlayer();
        offset = transform.position - target.transform.position;
    }

    public void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag(TAG_PLAYER).GetComponent<Character>();
    }

    void Update()
    {
        Vector3 newpos = target.transform.position + offset;
        transform.position = newpos;
    }
}