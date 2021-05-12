using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressSlider : MonoBehaviour
{
    protected Slider slider;
    [SerializeField] private TextMeshProUGUI textValue;

    [Header("Плавное изменение полоски")]
    [SerializeField] protected bool lerpingValue = false;

    public float Value { get => slider.value; }

    public float MaxValue { get => slider.maxValue; }


    // Use this for initialization
    void Start()
    {

    }


    protected void Ini()
    {
        if (slider == null)
        {
        slider = GetComponent<Slider>();

        if (slider == null)
        {
            throw new ProgressSliderException("slider component is null");
        }
        }



    }

    protected void UpdateText(object value)
    {
        textValue.text = value.ToString();

    }

    public virtual void UpdateProgress(int value)
    {
        SetValueProgress(value);
    }

    public virtual void UpdateProgress(float value)
    {
        SetValueProgress(value);
    }


    public virtual void UpdateProgress(long value)
    {
        SetValueProgress(value);
    }

    public virtual void SetMaxValueProgress(float value)
    {
        Ini();
        slider.maxValue = value;
    }

    private void SetValueProgress(float value)
    {
        if (!lerpingValue)
        {
            slider.value = value;
        }

        else
        {
            StopAllCoroutines();
            StartCoroutine(LerpProgress(value));
        }
    }

    private IEnumerator LerpProgress (float value)
    {
        float lerp = 0;
        float startValue = slider.value;

        while (true)
        {
            float rate = 1.0f / 60.0f;

            yield return new WaitForSeconds(rate);
            lerp += rate;

            slider.value = Mathf.Lerp(startValue, value, lerp);

            if (lerp >= 1f)
            {
                yield break;
            }
        }
    }
}