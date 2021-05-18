using UnityEngine;

public class ObjectRandomScale : MonoBehaviour, IRemoveObject
    {

    // Use this for initialization
    void Start()
        {
        transform.localScale = ProbabilityUtility.GenerateProbalityInt() >= 50.0f ? transform.localScale / Random.Range(1.5f, 10.0f) : transform.localScale;
        Remove();
        }

    public void Remove()
    {
        Destroy(this);
    }

}