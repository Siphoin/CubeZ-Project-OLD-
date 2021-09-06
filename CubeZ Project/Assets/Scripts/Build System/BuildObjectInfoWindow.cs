using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BuildObjectInfoWindow : MonoBehaviour
{
	private const string PATH_PREFAB_OBJECT_BUILD_OBJECT_REQUEMENTS = "Prefabs/UI/requementsBuildObject";

	
	[Header("Текст имени объекта")]
	[SerializeField]
	private TextMeshProUGUI textNameObject;

	
	[Header("Иконка объекта")]
	[SerializeField]
	private Image icoBuildObject;

	
	[Header("Иконка объекта")]
	[SerializeField]
	private GridLayoutGroup gridRequements;


	private BuildRequementsUI buildRequementsUIPrefab;

	
	private List<BuildRequementsUI> requementsList = new List<BuildRequementsUI>();



	private void Ini()
	{
		if (textNameObject == null)
		{
			throw new BuildObjectInfoWindowException("text name build object not seted");
		}

		if (icoBuildObject == null)
		{
			throw new BuildObjectInfoWindowException("ico build object not seted");
		}

		if (gridRequements == null)
		{
			throw new BuildObjectInfoWindowException("grid requements object not seted");
		}

		if (buildRequementsUIPrefab == null)
		{
			buildRequementsUIPrefab = Resources.Load<BuildRequementsUI>(PATH_PREFAB_OBJECT_BUILD_OBJECT_REQUEMENTS);

			if (buildRequementsUIPrefab == null)
			{
				throw new BuildObjectInfoWindowException("build requements ui prefab not found");
			}
		}
	}


	
	public void SetData(BuildObjectData data)
	{
		if (data == null)
		{
			throw new BuildObjectInfoWindowException("target data is null");
		}
		Ini();

		textNameObject.text = data.NameBuildObject;
		icoBuildObject.sprite = data.IcoBuildObject;

		RefreshRequements(data);
	}

	private void RefreshRequements(BuildObjectData data)
	{
		ClearOldRequements();

		for (int i = 0; i < data.RequirementsResources.Length; i++)
		{
			CreateNewRequement(data.RequirementsResources[i]);
		}
	}

	
	private void CreateNewRequement(BuildDataRequirements requirement)
	{
		BuildRequementsUI buildRequementsUI = Instantiate(buildRequementsUIPrefab, gridRequements.transform);
		buildRequementsUI.SetData(requirement.typeResource.data.icon, requirement.requirementsValue, requirement.typeResource.data.idItem);
		requementsList.Add(buildRequementsUI);
	}

	
	private void ClearOldRequements()
	{
		for (int i = 0; i < gridRequements.transform.childCount; i++)
		{
            Destroy(gridRequements.transform.GetChild(i).gameObject);
		}
		requementsList.Clear();
	}

	
	public void CheckRequwments(string idItem)
	{
		BuildRequementsUI[] array = requementsList.Where(item => item.IdResource == idItem).ToArray();

		for (int i = 0; i < array.Length; i++)
		{
			int value = GameCacheManager.gameCache.inventory.CountItemOfID(idItem);
			array[i].CheckValidRequementCountResource(value);
		}
	}

	private void Start() => Ini();

}
