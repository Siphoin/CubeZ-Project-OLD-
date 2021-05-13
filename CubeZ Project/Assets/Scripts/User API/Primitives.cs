namespace CBZ.API
{
    public static class Primitives
    {
        private const string PATH_PREFAB_CUBE = "primitives/cube";

        private const string PATH_PREFAB_PLANE = "primitives/plane";

        public static APIObjectCube CreateCube ()
        {
            APIObjectCube cube = APIObject.CreateObjectAPI(PATH_PREFAB_CUBE).GetComponent<APIObjectCube>();
            cube.Ini();
            return cube;
        }

        public static APIObjectPlane CreatePlane ()
        {
            APIObjectPlane plane = APIObject.CreateObjectAPI(PATH_PREFAB_PLANE).GetComponent<APIObjectPlane>();
            plane.Ini();
            return plane;
        }
    }
}