using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Light))]
    public class LightRipple : MonoBehaviour
    {
    private Light light;


    private float intensity;

    private float startintensity;
    [Header("Скорость пульсирования")]
    [SerializeField] private float speed = 0;


    private const float DIVISION_VALUE = 2f;
        // Use this for initialization
        void Start()
        {
        if (speed < 0)
        {
            throw new LightRippleException("speed not must be lower 0!");
        }
        if (!TryGetComponent(out light))
        {
            throw new LightRippleException("not found component Light");
        }


        startintensity = light.intensity;
    }

        // Update is called once per frame
        void Update()
        {
        if (light.enabled)
        {
        intensity = Mathf.PingPong(speed * Time.time, (int)startintensity / 1.5f);
        light.intensity = intensity;
        }

        }
    }