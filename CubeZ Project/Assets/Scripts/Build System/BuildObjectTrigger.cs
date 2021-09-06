using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
public class BuildObjectTrigger : MonoBehaviour
{
	// Token: 0x14000006 RID: 6
	// (add) Token: 0x060000CA RID: 202 RVA: 0x00004284 File Offset: 0x00002484
	// (remove) Token: 0x060000CB RID: 203 RVA: 0x000042BC File Offset: 0x000024BC
	public event Action<bool> newTriggerEnter;

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x060000CC RID: 204 RVA: 0x000042F4 File Offset: 0x000024F4
	// (remove) Token: 0x060000CD RID: 205 RVA: 0x0000432C File Offset: 0x0000252C
	public event Action<bool> newTriggerExit;

	// Token: 0x060000CE RID: 206 RVA: 0x0000357F File Offset: 0x0000177F
	private void Start()
	{
	}

	// Token: 0x060000CF RID: 207 RVA: 0x0000357F File Offset: 0x0000177F
	private void Update()
	{
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00004361 File Offset: 0x00002561
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("HouseArea"))
		{
			Action<bool> action = this.newTriggerEnter;
			if (action == null)
			{
				return;
			}
			action(false);
		}
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00004381 File Offset: 0x00002581
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("HouseArea"))
		{
			Action<bool> action = this.newTriggerExit;
			if (action == null)
			{
				return;
			}
			action(true);
		}
	}

	// Token: 0x04000071 RID: 113
	private const string TAG_HOUSE_AREA = "HouseArea";
}
