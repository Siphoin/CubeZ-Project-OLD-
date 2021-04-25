using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
    public class Weather : MonoBehaviour
    {

    private ParticleSystem particles;

    
    // Use this for initialization
    void Start()
        {
        if (!TryGetComponent(out particles))
        {
            throw new WeatherException("not found component Particle System");
        }
        BoxCollider boxCollider = null;
        if (!GameObject.FindGameObjectWithTag("WeatherBox").TryGetComponent(out boxCollider))
        {
            throw new WeatherException("weather box not found");
        }

        Bounds bounds = boxCollider.bounds;
        Vector3 size = bounds.size;
        var sh = particles.shape;
        sh.scale = new Vector3(size.x, size.z, 1);
        Destroy(this);
        }

    }