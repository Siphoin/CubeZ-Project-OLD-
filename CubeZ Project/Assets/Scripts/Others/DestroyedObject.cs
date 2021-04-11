using UnityEngine;

public class DestroyedObject : MonoBehaviour, IRemoveObject
    {
        public void Remove()
        {
            Destroy(gameObject);
        }

    }