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


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void Ini()
    {
        slider = GetComponent<Slider>();

        if (slider == null)
        {
            throw new ProgressSliderException("slider component is null");
        }


    }

    protected void UpdateText(object value)
    {
        textValue.text = value.ToString();

    }

    protected void UpdateProgress(int value)
    {
        SetValueProgress(value);
    }

    protected void UpdateProgress(float value)
    {
        SetValueProgress(value);
    }


    protected void UpdateProgress(long value)
    {
        SetValueProgress(value);
    }

    protected void SetMaxValueProgress(float value)
    {
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