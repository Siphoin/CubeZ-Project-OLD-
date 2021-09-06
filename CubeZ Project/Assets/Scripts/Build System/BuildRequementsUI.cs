using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000018 RID: 24
public class BuildRequementsUI : MonoBehaviour, IRemoveObject
{
	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000085 RID: 133 RVA: 0x0000372B File Offset: 0x0000192B
	public string IdResource
	{
		get
		{
			return this.idResource;
		}
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00003733 File Offset: 0x00001933
	private void Start()
	{
		this.Ini();
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0000373B File Offset: 0x0000193B
	private void Ini()
	{
		if (this.icon == null)
		{
			throw new BuildRequementsUIException("icon not seted");
		}
		if (this.textRequements == null)
		{
			throw new BuildRequementsUIException("text requements not seted");
		}
	}

	// Token: 0x06000088 RID: 136 RVA: 0x0000376F File Offset: 0x0000196F
	public void Remove()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000089 RID: 137 RVA: 0x0000377C File Offset: 0x0000197C
	public void SetData(Sprite icon, int valueRequements, string idResource)
	{
		if (icon == null)
		{
			throw new BuildRequementsUIException("icon argument is null");
		}
		this.Ini();
		this.icon.sprite = icon;
		this.textRequements.text = valueRequements.ToString();
		this.requementValue = valueRequements;
		this.idResource = idResource;
	}

	// Token: 0x0600008A RID: 138 RVA: 0x000037CF File Offset: 0x000019CF
	private void SetColorText(Color color)
	{
		this.Ini();
		this.textRequements.color = color;
	}

	// Token: 0x0600008B RID: 139 RVA: 0x000037E3 File Offset: 0x000019E3
	public void CheckValidRequementCountResource(int value)
	{
		this.SetColorText((value < this.requementValue) ? Color.red : Color.white);
	}

	// Token: 0x04000049 RID: 73
	[Header("Иконка")]
	[SerializeField]
	private Image icon;

	// Token: 0x0400004A RID: 74
	[Header("Текст требуемых ресурсов")]
	[SerializeField]
	private TextMeshProUGUI textRequements;

	// Token: 0x0400004B RID: 75
	private int requementValue;

	// Token: 0x0400004C RID: 76
	private string idResource;
}
