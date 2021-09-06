using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200001A RID: 26
public class BuildWindow : Window
{
	// Token: 0x06000091 RID: 145 RVA: 0x00003800 File Offset: 0x00001A00
	private void Start()
	{
		this.Ini();
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00003808 File Offset: 0x00001A08
	private void Ini()
	{
		if (this.buildObjectInfoWindow == null)
		{
			throw new BuildWindowException("build object info window not seted");
		}
		if (this.gridButtonsCategories == null)
		{
			throw new BuildWindowException("grid buttons categories not seted");
		}
		if (this.gridObjects == null)
		{
			throw new BuildWindowException("grid objects not seted");
		}
		if (this.inputFind == null)
		{
			throw new BuildWindowException("input find not seted");
		}
		if (BuildObjectsManager.ActiveManager == null)
		{
			throw new BuildWindowException("active build manager not found");
		}
		this.buildObjectCellPrefab = Resources.Load<BuildObjectCell>("Prefabs/UI/buildObjectCell");
		if (this.buildObjectCellPrefab == null)
		{
			throw new BuildWindowException("build object cell prefab not found");
		}
		this.buttonCategoryBuildPrefab = Resources.Load<ButtonCategoryBuild>("Prefabs/UI/ButtonCategoryBuild");
		if (this.buttonCategoryBuildPrefab == null)
		{
			throw new BuildWindowException("button category build prefab not found");
		}
		this.SetVisibleBuildObjectInfoWindow(false);
		this.buildObjectsList = BuildObjectsManager.ActiveManager.GetBuildObjectsData();
		this.ShowBuildObjects(this.buildObjectsList);
		Array array = Enum.GetValues(typeof(BuildObjectType)).Cast<BuildObjectType>().ToArray<BuildObjectType>();
		for (int i = 0; i < array.Length; i++)
		{
			this.CreateCategoryButton((BuildObjectType)array.GetValue(i));
		}
		this.inputFind.onValueChanged.AddListener(new UnityAction<string>(this.InputNameObject));
		this.inputFind.onEndEdit.AddListener(new UnityAction<string>(this.ActivateCharacter));
		this.inputFind.onSelect.AddListener(new UnityAction<string>(this.PlayerFrezze));
		GameCacheManager.gameCache.inventory.onItemOfTypeAdded += this.NewItemAddedtoInventory;
	}

	// Token: 0x06000093 RID: 147 RVA: 0x000039B2 File Offset: 0x00001BB2
	private void ActivateCharacter(string str)
	{
		this.player.ActivateCharacter();
	}

	// Token: 0x06000094 RID: 148 RVA: 0x000039C0 File Offset: 0x00001BC0
	private void InputNameObject(string str)
	{
		str = str.ToLower().Trim();
		this.PlayerFrezze(str);
		if (string.IsNullOrEmpty(str))
		{
			this.ShowBuildObjects(this.buildObjectsList);
			return;
		}
		BuildObjectData[] listObjects = this.NewFitler((BuildObjectData item) => item.NameBuildObject.ToLower().Contains(str));
		this.ShowBuildObjects(listObjects);
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00003A30 File Offset: 0x00001C30
	private void PlayerFrezze(string str)
	{
		base.FrezzePlayer();
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00003A38 File Offset: 0x00001C38
	private void NewItemAddedtoInventory(string itemObject)
	{
		this.CheckRequementsOfBuildObject(this.selectedBuildObjectData);
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00003A48 File Offset: 0x00001C48
	private void SetVisibleBuildObjectInfoWindow(bool status)
	{
		this.buildObjectInfoWindow.gameObject.SetActive(status);
		if (status)
		{
			GameCacheManager.gameCache.inventory.onItemOfTypeAdded += this.NewItemAddedtoInventory;
			return;
		}
		GameCacheManager.gameCache.inventory.onItemOfTypeAdded -= this.NewItemAddedtoInventory;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00003AA0 File Offset: 0x00001CA0
	private void ShowBuildObjects(BuildObjectData[] listObjects)
	{
		this.RemoveOldListBuildObjects();
		foreach (BuildObjectData data in listObjects)
		{
			this.CreateBuildObjectCell(data);
		}
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00003ACC File Offset: 0x00001CCC
	private void RemoveOldListBuildObjects()
	{
		for (int i = 0; i < this.gridObjects.transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.gridObjects.transform.GetChild(i).gameObject);
		}
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00003B0F File Offset: 0x00001D0F
	private void CreateBuildObjectCell(BuildObjectData data)
	{
		BuildObjectCell buildObjectCell = UnityEngine.Object.Instantiate<BuildObjectCell>(this.buildObjectCellPrefab, this.gridObjects.transform);
		buildObjectCell.SetData(data);
		buildObjectCell.onClick += this.BuildObjectSelect;
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00003B40 File Offset: 0x00001D40
	private void BuildObjectSelect(BuildObjectData buildObject)
	{
		this.SetVisibleBuildObjectInfoWindow(true);
		this.buildObjectInfoWindow.SetData(buildObject);
		this.CheckRequementsOfBuildObject(buildObject);
		this.selectedBuildObjectData = buildObject;
		if (this.activeSelectedObject != null && this.activeSelectedObject != buildObject)
		{
			this.activeSelectedObject.Remove();
		}
		if (GameCacheManager.gameCache.inventory.NotEnoughValueTypeItem(buildObject))
		{
			return;
		}
		this.activeSelectedObject = UnityEngine.Object.Instantiate<BuildObject>(buildObject.PrefabBuildObject);
		this.activeSelectedObject.onBuild += this.BuildEvent;
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00003BD0 File Offset: 0x00001DD0
	private void BuildEvent()
	{
		this.activeSelectedObject.onBuild -= this.BuildEvent;
		this.CheckRequementsOfBuildObject(this.selectedBuildObjectData);
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00003BF8 File Offset: 0x00001DF8
	private void CheckRequementsOfBuildObject(BuildObjectData buildObject)
	{
		try
		{
			for (int i = 0; i < buildObject.RequirementsResources.Length; i++)
			{
				this.buildObjectInfoWindow.CheckRequwments(buildObject.RequirementsResources[i].typeResource.data.idItem);
			}
		}
		catch
		{
		}
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00003C50 File Offset: 0x00001E50
	private void CreateCategoryButton(BuildObjectType categoeryType)
	{
		ButtonCategoryBuild buttonCategoryBuild = UnityEngine.Object.Instantiate<ButtonCategoryBuild>(this.buttonCategoryBuildPrefab, this.gridButtonsCategories.transform);
		buttonCategoryBuild.SetCategory(categoeryType);
		buttonCategoryBuild.onSelected += this.CategorySelected;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00003C80 File Offset: 0x00001E80
	private void CategorySelected(BuildObjectType category)
	{
		BuildObjectData[] listObjects = this.NewFitler((BuildObjectData item) => item.Category == category);
		this.ShowBuildObjects(listObjects);
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00003CB4 File Offset: 0x00001EB4
	private BuildObjectData[] NewFitler(Func<BuildObjectData, bool> conditions)
	{
		return this.buildObjectsList.Where(conditions).ToArray<BuildObjectData>();
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00003CC7 File Offset: 0x00001EC7
	public override void Exit()
	{
		if (this.activeSelectedObject != null)
		{
			this.activeSelectedObject.Remove();
		}
		GameCacheManager.gameCache.inventory.onItemOfTypeAdded -= this.NewItemAddedtoInventory;
		base.Exit();
	}

	// Token: 0x0400004D RID: 77
	[Header("Окно информации объекта")]
	[SerializeField]
	private BuildObjectInfoWindow buildObjectInfoWindow;

	// Token: 0x0400004E RID: 78
	[Header("Грид объектов в Scroll View")]
	[SerializeField]
	private GameObject gridObjects;

	// Token: 0x0400004F RID: 79
	[Header("Грид категорий")]
	[SerializeField]
	private GameObject gridButtonsCategories;

	// Token: 0x04000050 RID: 80
	[Header("Поле поиска")]
	[SerializeField]
	private TMP_InputField inputFind;

	// Token: 0x04000051 RID: 81
	private BuildObjectData[] buildObjectsList;

	// Token: 0x04000052 RID: 82
	private BuildObjectData selectedBuildObjectData;

	// Token: 0x04000053 RID: 83
	private const string PATH_PREFAB_BUILD_OBJECT_CELL = "Prefabs/UI/buildObjectCell";

	// Token: 0x04000054 RID: 84
	private const string PATH_PREFAB_BUTTON_CATEGORY = "Prefabs/UI/ButtonCategoryBuild";

	// Token: 0x04000055 RID: 85
	private BuildObjectCell buildObjectCellPrefab;

	// Token: 0x04000056 RID: 86
	private ButtonCategoryBuild buttonCategoryBuildPrefab;

	// Token: 0x04000057 RID: 87
	private BuildObject activeSelectedObject;
}
