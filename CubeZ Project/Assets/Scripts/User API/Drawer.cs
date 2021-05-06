

namespace CBZ.API
{
    public class Drawer : UserAPIComponent
    {
        private static Drawer activeDrawer;

        private static string PATH_OBJECT_TEXT = "text";
        private static string PATH_OBJECT_CANVAS = "canvas";
        // Use this for initialization
        void Awake()
        {
            if (activeDrawer == null)
            {
                Ini();
                activeDrawer = this;
            }
        }

        public static Text DrawText (Point point, string textString = "New_Text", string canvasName = "MainCanvas")
        {
            UnityEngine.GameObject canvas = UnityEngine.GameObject.Find(canvasName);

            Text text = APIObject.CreateObjectAPI(PATH_OBJECT_TEXT).GetComponent<Text>();
            text.transform.SetParent(canvas.transform);

            text.Ini();

            text.SetPosition(point);
            text.text = textString;


            return text;
        }

        public Canvas NewCanvas (int index = 0)
        {
            Canvas canvas = APIObject.CreateObjectAPI(PATH_OBJECT_TEXT).GetComponent<Canvas>();
            canvas.SetOrderIndex(index);
            return canvas;
        }

    }
}