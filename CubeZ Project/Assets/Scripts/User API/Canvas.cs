
using UnityEngine;
namespace CBZ.API
{
    [RequireComponent(typeof(UnityEngine.Canvas))]
    public class Canvas : APIObject, IAPIObject
    {
        private UnityEngine.Canvas canvas;

        public void Ini()
        {
            canvas = GetComponent<UnityEngine.Canvas> ();
        }



      public void SetOrderIndex(int index)
        {
            canvas.sortingOrder = index;
        }
    }
}