using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Build Objects/New Build Object Data", order = 0)]
public class BuildObjectData : ScriptableObject
{
	
	[Header("Имя объекта объекта")]
	[SerializeField]
	private string nameBuildObject;

	
	[Header("Данные по расходам ресурсов")]
	[SerializeField]
	private BuildDataRequirements[] requirementsResources;

	
	[Header("Пиктограмма объекта")]
	[SerializeField]
	private Sprite icoBuildObject;

	
	[Header("Префаб Build Object`a")]
	[SerializeField]
	private BuildObject prefabBuildObject;

	
	[Header("Категория объекта")]
	[SerializeField]
	private BuildObjectType category;

	public BuildDataRequirements[] RequirementsResources => requirementsResources;

	
	public Sprite IcoBuildObject => icoBuildObject;

	
	public string NameBuildObject => nameBuildObject;

	public BuildObject PrefabBuildObject => prefabBuildObject;

	public BuildObjectType Category => category;

}
