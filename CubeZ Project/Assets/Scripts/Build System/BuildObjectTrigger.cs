using System;
using UnityEngine;

public class BuildObjectTrigger : MonoBehaviour
{
	private const string TAG_HOUSE_AREA = "HouseArea";

	public event Action<bool> NewTriggerEnter;

	public event Action<bool> NewTriggerExit;


	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(TAG_HOUSE_AREA))
		{
			NewTriggerEnter?.Invoke(false);
		}
	}

	
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag(TAG_HOUSE_AREA))
		{
			NewTriggerExit?.Invoke(true);
		}
	}

}
