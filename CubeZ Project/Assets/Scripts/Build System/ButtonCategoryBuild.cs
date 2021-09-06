using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class ButtonCategoryBuild : MonoBehaviour
{
	
	private BuildObjectType currentCategory;

	public event Action<BuildObjectType> OnSelected;

	
	[Header("Текст кнопки")]
	[SerializeField]
	private TextMeshProUGUI textButton;
	
	private Button button;
	
	private void Ini()
	{
		if (button == null && !TryGetComponent(out button))
		{
			throw new ButtonCategoryBuildException("not found component Button");
		}

		if (textButton == null)
		{
			throw new ButtonCategoryBuildException("Text button not seted");
		}

		button.onClick.AddListener(Select);
	}

	public void SetCategory(BuildObjectType category)
	{
		Ini();

		currentCategory = category;

		SetTextButton();
	}

	private void Start() => Ini();

	private void Select() => OnSelected?.Invoke(currentCategory);

	private void SetTextButton() => textButton.text = currentCategory.ToString();

}
