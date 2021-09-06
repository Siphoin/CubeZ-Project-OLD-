using UnityEngine;

public class BuildObjectsManager : MonoBehaviour
{
	private const string PATH_DATA_BUILD_OBJECTS = "BuildObjects";

	private static BuildObjectsManager activeManager;


	private BuildObjectData[] buildObjects;
	public static BuildObjectsManager ActiveManager => activeManager;

	private void Awake()
	{
		if (activeManager == null)
		{
            activeManager = this;

            DontDestroyOnLoad(gameObject);

			Ini();

			return;
		}
        Destroy(gameObject);
	}


	public BuildObjectData[] GetBuildObjectsData()
	{
		return buildObjects;
	}

	private void Ini() => buildObjects = Resources.LoadAll<BuildObjectData>(PATH_DATA_BUILD_OBJECTS);

}
