using System.Collections;
using TMPro;
using UnityEngine;

    public class ShowAutoSaveUI : MonoBehaviour
    {
    [SerializeField] TextMeshProUGUI thisText;
    [SerializeField] float timeRemove;

    private Color defaultColor;
    private Color endColor = new Color();
    // Use this for initialization
    void Start()
        {
        if (thisText == null)
        {
            throw new ShowAutoSaveUIException("Text mesh pro GUI Component not found");
        }

        if (timeRemove <= 0)
        {

        }

        defaultColor = thisText.color;

        StartCoroutine(RemoveText());
    }

    private IEnumerator RemoveText ()
    {
        yield return new WaitForSeconds(timeRemove);
        float lerpValue = 0;
        while (true)
        {
            float rate = 1.0f / 60.0f;
            yield return new WaitForSeconds(rate);
            lerpValue += rate;

            thisText.color = Color.Lerp(defaultColor, endColor, lerpValue);

            if (lerpValue >= 1f)
            {
                Destroy(gameObject);
            }
        }
    }

    }