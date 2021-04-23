using UnityEngine;

public class RandomizerObjectEulerAngles : MonoBehaviour, IRemoveObject
    {
    public GameObject[] objects;
    [Range(0, 360)]
    public float maxAngle = 180f;

    public void Remove()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        Remove();
    }
}