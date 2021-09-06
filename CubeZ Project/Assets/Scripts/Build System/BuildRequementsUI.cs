using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildRequementsUI : MonoBehaviour, IRemoveObject
{
	private int requementValue;

	private string idResource;

	[Header("Иконка")]
	[SerializeField]
	private Image icon;

	[Header("Текст требуемых ресурсов")]
	[SerializeField]
	private TextMeshProUGUI textRequements;

	public string IdResource => idResource;
	
	private void Ini()
	{
		if (icon == null)
		{
			throw new BuildRequementsUIException("icon not seted");
		}
		if (textRequements == null)
		{
			throw new BuildRequementsUIException("text requements not seted");
		}
	}


	public void SetData(Sprite icon, int valueRequements, string idResource)
	{
		if (icon == null)
		{
			throw new BuildRequementsUIException("icon argument is null");
		}
		Ini();
		icon.sprite = icon;
		textRequements.text = valueRequements.ToString();
		requementValue = valueRequements;
		idResource = idResource;
	}

	
	private void SetColorText(Color color)
	{
		Ini();

		textRequements.color = color;
	}

	private void Start() => Ini();

	public void Remove() => Destroy(gameObject);

	public void CheckValidRequementCountResource(int value) => SetColorText((value < requementValue) ? Color.red : Color.white);

	
}
