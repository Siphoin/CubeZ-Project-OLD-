using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200001C RID: 28
[RequireComponent(typeof(Button))]
public class ButtonCategoryBuild : MonoBehaviour
{
	// Token: 0x14000004 RID: 4
	// (add) Token: 0x060000A7 RID: 167 RVA: 0x00003D0C File Offset: 0x00001F0C
	// (remove) Token: 0x060000A8 RID: 168 RVA: 0x00003D44 File Offset: 0x00001F44
	public event Action<BuildObjectType> onSelected;

	// Token: 0x060000A9 RID: 169 RVA: 0x00003D79 File Offset: 0x00001F79
	private void Start()
	{
		this.Ini();
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00003D84 File Offset: 0x00001F84
	private void Ini()
	{
		if (this.button == null && !base.TryGetComponent<Button>(out this.button))
		{
			throw new ButtonCategoryBuildException("not found component Button");
		}
		if (this.textButton == null)
		{
			throw new ButtonCategoryBuildException("Text button not seted");
		}
		this.button.onClick.AddListener(new UnityAction(this.Select));
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00003DED File Offset: 0x00001FED
	private void Select()
	{
		Action<BuildObjectType> action = this.onSelected;
		if (action == null)
		{
			return;
		}
		action(this.currentCategory);
	}

	// Token: 0x060000AC RID: 172 RVA: 0x00003E05 File Offset: 0x00002005
	private void SetTextButton()
	{
		this.textButton.text = this.currentCategory.ToString();
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00003E23 File Offset: 0x00002023
	public void SetCategory(BuildObjectType category)
	{
		this.Ini();
		this.currentCategory = category;
		this.SetTextButton();
	}

	// Token: 0x04000058 RID: 88
	private BuildObjectType currentCategory;

	// Token: 0x04000059 RID: 89
	[Header("Текст кнопки")]
	[SerializeField]
	private TextMeshProUGUI textButton;

	// Token: 0x0400005A RID: 90
	private Button button;
}
