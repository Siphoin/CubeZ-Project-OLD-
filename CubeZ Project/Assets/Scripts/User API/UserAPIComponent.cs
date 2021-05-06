using UnityEngine;

namespace CBZ.API
{
    public class UserAPIComponent : MonoBehaviour
    {
        private const string NAME_PARENT_OBJECT = "UserAPI";
        // Use this for initialization
        void Start()
        {

        }

        public virtual void Ini ()
        {
            GameObject parent = GameObject.Find(NAME_PARENT_OBJECT);
            if (parent == null)
            {
                parent = new GameObject(NAME_PARENT_OBJECT);
            }
            DontDestroyOnLoad(parent);
            transform.SetParent(parent.transform);
            DontDestroyOnLoad(gameObject);
        }
    }
}