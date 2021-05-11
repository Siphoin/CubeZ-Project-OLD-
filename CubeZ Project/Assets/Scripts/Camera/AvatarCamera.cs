using System.Collections;
using UnityEngine;

    public class AvatarCamera : MonoBehaviour
    {
    private const string TAG_AVATAR = "MyPlayerAvatar";

    private Vector3 startPosition;
        // Use this for initialization
        void Start()
        {

        startPosition = transform.position;
        Transform avatar = GameObject.FindGameObjectWithTag(TAG_AVATAR).GetComponent<Transform>();

        transform.SetParent(avatar);
        transform.localPosition = Vector3.zero;
        transform.localPosition = new Vector3(0f, 1f, 0.64f);

        Destroy(this);
        }

    }