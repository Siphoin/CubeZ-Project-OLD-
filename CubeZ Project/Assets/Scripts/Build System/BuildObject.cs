using System;
using UnityEngine;


public class BuildObject : MonoBehaviour, IRemoveObject
{

	
	private const float SPEED_ROTATION = 3000f;

	
	private const string AXIS_MOUSE = "Mouse ScrollWheel";


	private const string PATH_TRANSPERENT_MATERIAL = "Materials/transperent_material";
	
	[Header("Вращать только дочерние меши")]
	[SerializeField]
	private bool rotateChildMeshs;

	
	[Header("Можно создавать внутри дома")]
	[SerializeField]
	private bool createinHouse = true;

	[Header("Может ли объект построиться")]
	[SerializeField]
	[ReadOnlyField]
	private bool enableBuild = true;

	public event Action OnBuild;
	
	[Header("Меши объекта")]
	[SerializeField]
	private Renderer[] renderersParts;

	
	[Header("Объект при постройке")]
	[SerializeField]
	private GameObject objectResult;

	
	[Header("Триггер Build Object`a")]
	[SerializeField]
	private BuildObjectTrigger buildObjectTrigger;

	[Header("Данные по затраченным ресурсам")]
	[SerializeField]
	private BuildObjectData buildObjectData;

	private Material transperentMaterial;

	private void Start()
	{
		if (buildObjectData == null)
		{
			throw new BuildObjectException($"Build object data not seted{name}");
		}

		if (objectResult == null)
		{
			throw new BuildObjectException("object result not seted");
		}

		if (buildObjectTrigger == null)
		{
			throw new BuildObjectException("build object trigger not seted");
		}
		transperentMaterial = Resources.Load<Material>(PATH_TRANSPERENT_MATERIAL);

		if (transperentMaterial == null)
		{
			throw new BuildObjectException("transperent material not found");
		}

		SetAlphaRendererMeshs(Color.green);

		if (!createinHouse)
		{
			buildObjectTrigger.NewTriggerExit += NewTriggerCalled;
			buildObjectTrigger.NewTriggerEnter += NewTriggerCalled;
		}

		else
		{
            Destroy(buildObjectTrigger.gameObject);
		}

		enableBuild = true;
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00003FAB File Offset: 0x000021AB
	private void NewTriggerCalled(bool status)
	{
		enableBuild = status;
		SetAlphaRendererMeshs(enableBuild ? Color.green : Color.red);
	}

	private void SetAlphaRendererMeshs(Color color)
	{
		for (int i = 0; i < renderersParts.Length; i++)
		{
			Renderer renderer = renderersParts[i];
			renderer.material = transperentMaterial;
			for (int j = 0; j < renderer.materials.Length; j++)
			{
				Material material = renderer.materials[j];
				renderer.materials[j] = transperentMaterial;
				Color color2 = color;
				color2.a = 0.5f;
				material.color = color2;
			}
		}
	}

	private void Update()
	{
		RaycastHit raycastHit;


		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit))
		{
            transform.position = new Vector3(raycastHit.point.x, base.transform.position.y, raycastHit.point.z);
		}

		if (!rotateChildMeshs)
		{
			RotateParentObject();
		}
		else
		{
			RotateChildMeshes();
		}

		if (Input.GetMouseButtonDown(0) && enableBuild)
		{
			Build();
		}

		if (Input.GetMouseButtonDown(1))
		{
			Remove();
		}
	}

	
	private void Build()
	{
		for (int i = 0; i < buildObjectData.RequirementsResources.Length; i++)
		{
			BuildDataRequirements buildDataRequirements = buildObjectData.RequirementsResources[i];
			if (!GameCacheManager.gameCache.inventory.TryRemoveItemsWithCountOfType(buildDataRequirements.typeResource.data.idItem, buildDataRequirements.requirementsValue))
			{
				return;
			}
		}
		GameObject gameObject = Instantiate(objectResult);
		gameObject.transform.position = transform.position;
		gameObject.transform.rotation = transform.rotation;
		OnBuild?.Invoke();

		Remove();
	}


	private void RotateParentObject()
	{
        transform.Rotate(transform.rotation.x, Input.GetAxis(AXIS_MOUSE) * SPEED_ROTATION * Time.deltaTime, transform.rotation.z);
	}

	private void RotateChildMeshes()
	{
		for (int i = 0; i < renderersParts.Length; i++)
		{
			Renderer renderer = renderersParts[i];
			renderer.transform.Rotate(renderer.transform.rotation.x, Input.GetAxis(AXIS_MOUSE) * SPEED_ROTATION * Time.deltaTime, renderer.transform.rotation.z);
		}
	}

	public void Remove() => Destroy(gameObject);

}
