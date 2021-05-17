using System;
using System.Collections;
using UnityEngine;

public class Blood : MonoBehaviour, IRemoveObject, IInvokerMono
    {
    private Renderer renderer;

    private Color originalColor;

    private Color alphaColor;

    [SerializeField]
    [Header("Задержка перед исчезновением")]
  private  float destroyTimeOut = 5;


    [SerializeField]
    [ReadOnlyField]
    [Header("Исчезает")]
    private bool destroyed = false;


    // Use this for initialization
    void Start()
    {
        Ini();

        
    }

    private void Ini()
    {
        if (destroyTimeOut <= 0)
        {
            throw new BloodException("destroy time out blood not valuid value");
        }

        if (renderer == null)
        {
        if (!TryGetComponent(out renderer))
        {
            throw new BloodException($"{name} not have component Renderer");
        }

            originalColor = renderer.material.color;

             alphaColor = originalColor;
            alphaColor.a = 0;


        }

    }

    public void Remove()
    {
        StartCoroutine(AlphaChangeColor(originalColor, alphaColor));
    }

    public void CallInvokingEveryMethod(Action method, float time)
    {
        InvokeRepeating(method.Method.Name, time, time);
    }

    public void CallInvokingMethod(Action method, float time)
    {
        Invoke(method.Method.Name, time);
    }

    private IEnumerator AlphaChangeColor (Color originalColor, Color endColor)
    {

        float lerpValue = 0;

        while (true)
        {
            float time = 1.0F / 60.0f;
            yield return new WaitForSeconds(time);

            lerpValue += time;

            renderer.material.color = Color.Lerp(originalColor, endColor, lerpValue);

            if (lerpValue >= 1f && this.alphaColor == endColor)
            {

                Destroy(gameObject);
            }

            else if (lerpValue >= 1f)
            {
                yield break;
            }

        }
    }
    
    public void RemoveWithTime ()
    {
        Ini();
        destroyed = true;
        renderer.material.color = alphaColor;
        CallInvokingMethod(Remove, destroyTimeOut);
        StartCoroutine(AlphaChangeColor(alphaColor, originalColor));
    }
}