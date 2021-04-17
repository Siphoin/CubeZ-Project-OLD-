using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
    public class GCZombie : MonoBehaviour
    {
    [SerializeField] private GameObject parentMeshRenderer;

    private Collider collider;

    private float timeDestroy;
        // Use this for initialization
        void Start()
        {
        if (parentMeshRenderer == null)
        {
            throw new GCZombieException("parent mesh renderer not seted");
        }

        if (!parentMeshRenderer.TryGetComponent(out collider))
        {
            throw new GCZombieException("not found Colider component on parent mesh renderer");
        }

        if (WorldManager.Manager == null)
        {
            throw new GCZombieException("world manager not found");
        }


        timeDestroy = WorldManager.Manager.SettingsZombie.GetData().timeRemoveonGCZombie;
        StartCoroutine(DestroyObjectOfTime());
        }

        // Update is called once per frame
        void Update()
        {
    }

    IEnumerator DestroyObjectOfTime ()
    {
        while (true)
        {

yield return new WaitForSeconds(timeDestroy);
            
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            if (!GeometryUtility.TestPlanesAABB(planes, collider.bounds))
            {
            Destroy(parentMeshRenderer);
            }

        }
        
    }
    }