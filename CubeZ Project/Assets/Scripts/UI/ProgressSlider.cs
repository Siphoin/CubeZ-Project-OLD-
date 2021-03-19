using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressSlider : MonoBehaviour
{
    protected Slider slider;
    [SerializeField] private TextMeshProUGUI textValue;


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

        if (textValue == null)
        {
            throw new ProgressSliderException("text value component is null");
        }

    }

    protected void UpdateText(object value)
    {


        textValue.text = value.ToString();


    }

    protected void UpdateProgress(int value)
    {
        slider.value = value;
    }

    protected void UpdateProgress(float value)
    {
        slider.value = value;
    }

    protected void UpdateProgress(long value)
    {
        slider.value = value;
    }

    protected void SetMaxValueProgress(float value)
    {
        slider.maxValue = value;
    }
}