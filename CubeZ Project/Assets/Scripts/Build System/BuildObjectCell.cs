﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildObjectCell : MonoBehaviour
{
	
	public event Action<BuildObjectData> OnClick;

	
	[Header("Иконка")]
	[SerializeField]
	private Image pictogram;

	
	[Header("Кнопка")]
	[SerializeField]
	private Button buttonPictogram;

	
	private BuildObjectData currentData;


	
	private void Ini()
	{
		if (pictogram == null)
		{
			throw new BuildObjectCellException("pictogram not seted");
		}
		if (buttonPictogram == null)
		{
			throw new BuildObjectCellException("pictogram button not seted");
		}
		buttonPictogram.onClick.AddListener(Select);
	}


	// Token: 0x06000072 RID: 114 RVA: 0x000034B3 File Offset: 0x000016B3
	public void SetData(BuildObjectData data)
	{
		if (data == null)
		{
			throw new BuildObjectCellException("data target is null");
		}
		Ini();

		currentData = data;
		pictogram.sprite = data.IcoBuildObject;
	}

	private void Start() => Ini();
	private void Select() => OnClick?.Invoke(currentData);

}
