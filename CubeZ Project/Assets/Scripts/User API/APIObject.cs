using System.Collections;
using UnityEngine;

namespace CBZ.API
{
    public class APIObject : MonoBehaviour, IRemoveObject
    {
        private const string PATH_API_OBJECTS = "Prefabs/api/";


        public string TagObject { get; set; } = "Untagged";


        public void SetPosition (Point point)
        {
            transform.position = point.ToVector3();
        }

        public void SetLocalPosition(Point point)
        {
            transform.localPosition = point.ToVector3();
        }

        public void SetRotation (Angle angle)
        {
            transform.rotation = angle.ToQuaternion();
        }

        public void SetLocalRotation(Angle angle)
        {
            transform.localRotation = angle.ToQuaternion();
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetTag(string tag)
        {
            TagObject = tag;
        }

        public void Remove()
        {
            Destroy(gameObject);
        }

        public void SetParent (APIObject aPIObject)
        {
            transform.SetParent(aPIObject.transform);
        }

        public static APIObject CreateObjectAPI (string path)
        {
            APIObject obj = Resources.Load<APIObject>(PATH_API_OBJECTS +  path);


            if (obj == null)
            {
                throw new APIObjectException($"not found object on path {path}");
            }

            return Instantiate(obj);
        }
    }
}