using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Others
{
    public class ObjectWithRandomAngle : MonoBehaviour
    {
        [Header("Максиальный угол наклона")]
        [Range(0.1f, 360f)]
        [SerializeField] private float maxAngleValue = 0.1f;
        // Use this for initialization
        void Start()
        {
            var angle = transform.rotation.eulerAngles;
            angle.y = Random.Range(0, maxAngleValue + 1.0f);
            transform.rotation = Quaternion.Euler(angle);
            Destroy(this);
        }

    }
}