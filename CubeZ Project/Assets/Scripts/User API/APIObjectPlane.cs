using CBZ.API;
using CBZ.API.Debug;
using System.Collections;
using UnityEngine;

namespace CBZ.API
{
    public class APIObjectPlane : MonoBehaviour, IAPIObjectMesh, IAPIObject
    {
        private Renderer renderer;
        public void Ini()
        {

            if (!TryGetComponent(out renderer))
            {
                throw new APIObjectException($"{name} not have component Renderer");
            }
        }

        public void SetColorMaterial(Color32 color, int index = 0)
        {
            Ini();


            if (renderer.materials.Length < index)
            {
                Debug.Debug.Print($"index material greater then count materials on the object {name}", LogMessageType.Warning);
                return;
            }

            renderer.materials[index].color = color.ToUnityColor32();
        }

        public void SetColorMaterial(Color color, int index = 0)
        {
            Ini();


            if (renderer.materials.Length < index)
            {
                Debug.Debug.Print($"index material greater then count materials on the object {name}", LogMessageType.Warning);
                return;
            }

            renderer.materials[index].color = color.ToUnityColor();
        }
    }
}