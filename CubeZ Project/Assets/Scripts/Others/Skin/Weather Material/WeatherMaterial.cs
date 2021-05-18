using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
public class WeatherMaterial : MonoBehaviour
{
    [Header("Мвтериал снега")]
    [SerializeField] Color materialSnow;


    private Color originalMaterial;

    private Renderer rendererMesh;
    // Use this for initialization
    void Start()
    {
        if (materialSnow == null)
        {
            throw new WeatherMaterialException("material snow not seted");
        }


        if (WorldManager.Manager == null)
        {
            throw new WeatherMaterialException("world manager not found");
        }

        if (!TryGetComponent(out rendererMesh))
        {
            throw new WeatherMaterialException($"object {name} not have component Renderer");
        }

        originalMaterial = rendererMesh.material.color;




        WorldManager.Manager.onWeatherChanged += Manager_onWeatherChanged;
        Manager_onWeatherChanged(WorldManager.Manager.CurrentWeather);
    }

    private void Manager_onWeatherChanged(WeatherType weather)
    {
        StartCoroutine(LerpingMaterial(weather == WeatherType.Snow ? materialSnow : originalMaterial));
    }

    private IEnumerator LerpingMaterial(Color colorTarget)
    {
        float lerpValue = 0;
        while (true)
        {
            if (colorTarget == rendererMesh.material.color)
            {
                yield break;
            }
            float rate = 1.0f / 60.0f;
            yield return new WaitForSeconds(rate);

            lerpValue += rate;
            rendererMesh.material.color = Color.Lerp(rendererMesh.material.color, colorTarget, lerpValue);

            if (lerpValue >= 1f)
            {
                yield break;
            }
        }
    }

    private void OnDestroy()
    {
        try
        {
            WorldManager.Manager.onWeatherChanged -= Manager_onWeatherChanged;
        }
        catch
        {

        }

    }

}