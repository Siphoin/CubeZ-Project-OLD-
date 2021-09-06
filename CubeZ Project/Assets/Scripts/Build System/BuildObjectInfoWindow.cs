using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000016 RID: 22
public class BuildObjectInfoWindow : MonoBehaviour
{
	// Token: 0x06000078 RID: 120 RVA: 0x000034E7 File Offset: 0x000016E7
	private void Start()
	{
		this.Ini();
	}

	// Token: 0x06000079 RID: 121 RVA: 0x000034F0 File Offset: 0x000016F0
	private void Ini()
	{
		if (this.textNameObject == null)
		{
			throw new BuildObjectInfoWindowException("text name build object not seted");
		}
		if (this.icoBuildObject == null)
		{
			throw new BuildObjectInfoWindowException("ico build object not seted");
		}
		if (this.gridRequements == null)
		{
			throw new BuildObjectInfoWindowException("grid requements object not seted");
		}
		if (this.buildRequementsUIPrefab == null)
		{
			this.buildRequementsUIPrefab = Resources.Load<BuildRequementsUI>("Prefabs/UI/requementsBuildObject");
			if (this.buildRequementsUIPrefab == null)
			{
				throw new BuildObjectInfoWindowException("build requements ui prefab not found");
			}
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x0000357F File Offset: 0x0000177F
	private void Update()
	{
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00003584 File Offset: 0x00001784
	public void SetData(BuildObjectData data)
	{
		if (data == null)
		{
			throw new BuildObjectInfoWindowException("target data is null");
		}
		this.Ini();
		this.textNameObject.text = data.NameBuildObject;
		this.icoBuildObject.sprite = data.IcoBuildObject;
		this.RefreshRequements(data);
	}

	// Token: 0x0600007C RID: 124 RVA: 0x000035D4 File Offset: 0x000017D4
	private void RefreshRequements(BuildObjectData data)
	{
		this.ClearOldRequements();
		for (int i = 0; i < data.RequirementsResources.Length; i++)
		{
			this.CreateNewRequement(data.RequirementsResources[i]);
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00003608 File Offset: 0x00001808
	private void CreateNewRequement(BuildDataRequirements requirement)
	{
		BuildRequementsUI buildRequementsUI = UnityEngine.Object.Instantiate<BuildRequementsUI>(this.buildRequementsUIPrefab, this.gridRequements.transform);
		buildRequementsUI.SetData(requirement.typeResource.data.icon, requirement.requirementsValue, requirement.typeResource.data.idItem);
		this.requementsList.Add(buildRequementsUI);
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00003664 File Offset: 0x00001864
	private void ClearOldRequements()
	{
		for (int i = 0; i < this.gridRequements.transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.gridRequements.transform.GetChild(i).gameObject);
		}
		this.requementsList.Clear();
	}

	// Token: 0x0600007F RID: 127 RVA: 0x000036B4 File Offset: 0x000018B4
	public void CheckRequwments(string idItem)
	{
		BuildRequementsUI[] array = (from item in this.requementsList
		where item.IdResource == idItem
		select item).ToArray<BuildRequementsUI>();
		for (int i = 0; i < array.Length; i++)
		{
			int value = GameCacheManager.gameCache.inventory.CountItemOfID(idItem);
			array[i].CheckValidRequementCountResource(value);
		}
	}

	// Token: 0x04000043 RID: 67
	[Header("Текст имени объекта")]
	[SerializeField]
	private TextMeshProUGUI textNameObject;

	// Token: 0x04000044 RID: 68
	[Header("Иконка объекта")]
	[SerializeField]
	private Image icoBuildObject;

	// Token: 0x04000045 RID: 69
	[Header("Иконка объекта")]
	[SerializeField]
	private GridLayoutGroup gridRequements;

	// Token: 0x04000046 RID: 70
	private const string PATH_PREFAB_OBJECT_BUILD_OBJECT_REQUEMENTS = "Prefabs/UI/requementsBuildObject";

	// Token: 0x04000047 RID: 71
	private BuildRequementsUI buildRequementsUIPrefab;

	// Token: 0x04000048 RID: 72
	private List<BuildRequementsUI> requementsList = new List<BuildRequementsUI>();
}
