using UnityEngine;
using System.Linq;
public class SirenLightObject : MonoBehaviour
    {
    [SerializeField] Light[] sirensObjects;
        // Use this for initialization
        void Start()
        {
        if (sirensObjects.Length == 0)
        {
            throw new SirenLightObjectException("siren object list is emtry");
        }

        if (sirensObjects.Any(i => i == null))
        {
            throw new SirenLightObjectException("siren object list any null elements");
        }

        if (ProbabilityUtility.GenerateProbalityInt() < 50)
        {
            SetActiveLights(false);
        }
        }

        // Update is called once per frame
        void Update()
        {

        }

    public void SetActiveLights (bool status)
    {
        for (int i = 0; i < sirensObjects.Length; i++)
        {
            sirensObjects[i].enabled = status;
        }
    }
    }