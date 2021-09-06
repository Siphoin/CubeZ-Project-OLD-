using System;
using UnityEngine;


[Serializable]
public class BuildDataRequirements
{
	[Header("Тип ресурса")]
	public ResourceItem typeResource;

	[Header("Скольно нужно этого типа ресурса")]
	public int requirementsValue;

	public BuildDataRequirements()
	{
	}

	public BuildDataRequirements(BuildDataRequirements copyClass)
	{
		copyClass.CopyAll(this);
	}

}
