using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class BuildWindow : Window
{
	
	private const string PATH_PREFAB_BUILD_OBJECT_CELL = "Prefabs/UI/buildObjectCell";

	
	private const string PATH_PREFAB_BUTTON_CATEGORY = "Prefabs/UI/ButtonCategoryBuild";

	
	[Header("Окно информации объекта")]
	[SerializeField]
	private BuildObjectInfoWindow buildObjectInfoWindow;

	
	[Header("Грид объектов в Scroll View")]
	[SerializeField]
	private GameObject gridObjects;

	
	[Header("Грид категорий")]
	[SerializeField]
	private GameObject gridButtonsCategories;

	
	[Header("Поле поиска")]
	[SerializeField]
	private TMP_InputField inputFind;

	
	private BuildObjectData[] buildObjectsList;

	
	private BuildObjectData selectedBuildObjectData;


	
	private BuildObjectCell buildObjectCellPrefab;

	
	private ButtonCategoryBuild buttonCategoryBuildPrefab;

	
	private BuildObject activeSelectedObject;


	
	private void Ini()
	{
		if (buildObjectInfoWindow == null)
		{
			throw new BuildWindowException("build object info window not seted");
		}

		if (gridButtonsCategories == null)
		{
			throw new BuildWindowException("grid buttons categories not seted");
		}

		if (gridObjects == null)
		{
			throw new BuildWindowException("grid objects not seted");
		}

		if (inputFind == null)
		{
			throw new BuildWindowException("input find not seted");
		}

		if (BuildObjectsManager.ActiveManager == null)
		{
			throw new BuildWindowException("active build manager not found");
		}

		buildObjectCellPrefab = Resources.Load<BuildObjectCell>(PATH_PREFAB_BUILD_OBJECT_CELL);

		if (buildObjectCellPrefab == null)
		{
			throw new BuildWindowException("build object cell prefab not found");
		}

		buttonCategoryBuildPrefab = Resources.Load<ButtonCategoryBuild>(PATH_PREFAB_BUTTON_CATEGORY);

		if (buttonCategoryBuildPrefab == null)
		{
			throw new BuildWindowException("button category build prefab not found");
		}
		SetVisibleBuildObjectInfoWindow(false);

		buildObjectsList = BuildObjectsManager.ActiveManager.GetBuildObjectsData();

		ShowBuildObjects(buildObjectsList);

		Array array = Enum.GetValues(typeof(BuildObjectType)).Cast<BuildObjectType>().ToArray();

		for (int i = 0; i < array.Length; i++)
		{
			CreateCategoryButton((BuildObjectType)array.GetValue(i));
		}

		inputFind.onValueChanged.AddListener(InputNameObject);
		inputFind.onEndEdit.AddListener(ActivateCharacter);
		inputFind.onSelect.AddListener(PlayerFrezze);

		GameCacheManager.gameCache.inventory.onItemOfTypeAdded += NewItemAddedtoInventory;
	}

	
	private void ActivateCharacter(string str) => player.ActivateCharacter();

	
	private void InputNameObject(string str)
	{
		str = str.ToLower().Trim();

		PlayerFrezze(str);

		if (string.IsNullOrEmpty(str))
		{
			ShowBuildObjects(buildObjectsList);
			return;
		}
		BuildObjectData[] listObjects = NewFitler((BuildObjectData item) => item.NameBuildObject.ToLower().Contains(str));

		ShowBuildObjects(listObjects);
	}

	


	private void SetVisibleBuildObjectInfoWindow(bool status)
	{
		buildObjectInfoWindow.gameObject.SetActive(status);
		if (status)
		{
			GameCacheManager.gameCache.inventory.onItemOfTypeAdded += NewItemAddedtoInventory;
			return;
		}
		GameCacheManager.gameCache.inventory.onItemOfTypeAdded -= NewItemAddedtoInventory;
	}

	
	private void ShowBuildObjects(BuildObjectData[] listObjects)
	{
		RemoveOldListBuildObjects();

		foreach (BuildObjectData data in listObjects)
		{
			CreateBuildObjectCell(data);
		}
	}

	
	private void RemoveOldListBuildObjects()
	{
		for (int i = 0; i < gridObjects.transform.childCount; i++)
		{
            Destroy(gridObjects.transform.GetChild(i).gameObject);
		}
	}

	
	private void CreateBuildObjectCell(BuildObjectData data)
	{
		BuildObjectCell buildObjectCell = Instantiate(buildObjectCellPrefab, gridObjects.transform);
		buildObjectCell.SetData(data);

		buildObjectCell.OnClick += BuildObjectSelect;
	}

	
	private void BuildObjectSelect(BuildObjectData buildObject)
	{
		SetVisibleBuildObjectInfoWindow(true);

		buildObjectInfoWindow.SetData(buildObject);

		CheckRequementsOfBuildObject(buildObject);

		selectedBuildObjectData = buildObject;

		if (activeSelectedObject != null && activeSelectedObject != buildObject)
		{
			activeSelectedObject.Remove();
		}

		if (GameCacheManager.gameCache.inventory.NotEnoughValueTypeItem(buildObject))
		{
			return;
		}
		activeSelectedObject = Instantiate(buildObject.PrefabBuildObject);
		activeSelectedObject.OnBuild += BuildEvent;
	}

	
	private void BuildEvent()
	{
		activeSelectedObject.OnBuild -= BuildEvent;
		CheckRequementsOfBuildObject(selectedBuildObjectData);
	}

	
	private void CheckRequementsOfBuildObject(BuildObjectData buildObject)
	{
		try
		{
			for (int i = 0; i < buildObject.RequirementsResources.Length; i++)
			{
				buildObjectInfoWindow.CheckRequwments(buildObject.RequirementsResources[i].typeResource.data.idItem);
			}
		}
		catch
		{
		}
	}

	
	private void CreateCategoryButton(BuildObjectType categoeryType)
	{
		ButtonCategoryBuild buttonCategoryBuild = Instantiate(buttonCategoryBuildPrefab, gridButtonsCategories.transform);
		buttonCategoryBuild.SetCategory(categoeryType);
		buttonCategoryBuild.OnSelected += CategorySelected;
	}

	
	private void CategorySelected(BuildObjectType category)
	{
		BuildObjectData[] listObjects = NewFitler((BuildObjectData item) => item.Category == category);
		ShowBuildObjects(listObjects);
	}

	
	private BuildObjectData[] NewFitler(Func<BuildObjectData, bool> conditions)
	{
		return buildObjectsList.Where(conditions).ToArray();
	}

	
	public override void Exit()
	{
		if (activeSelectedObject != null)
		{
			activeSelectedObject.Remove();
		}
		GameCacheManager.gameCache.inventory.onItemOfTypeAdded -= NewItemAddedtoInventory;

		base.Exit();
	}
	private void Start() => Ini();

	private void PlayerFrezze(string str) => FrezzePlayer();

	private void NewItemAddedtoInventory(string itemObject) => CheckRequementsOfBuildObject(selectedBuildObjectData);

}
