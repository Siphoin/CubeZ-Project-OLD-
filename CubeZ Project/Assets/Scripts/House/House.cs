using System;
using System.Collections;
using UnityEngine;
using System.Linq;
public class House : MonoBehaviour
{
    [Header("Двери")]
    [SerializeField] Door[] doors;

    [Header("Крыша")]
    [SerializeField] Renderer roof;

    [Header("Триггер дома")]
    [SerializeField] HouseArea houseArea;

    private bool viewed = false;

    private Color defaultColotRoof;
    // Use this for initialization
    void Start()
    {
        if (doors.Length == 0)
        {
            throw new HouseException("list doors is emtry");
        }

        if (roof == null)
        {
            throw new HouseException("roff not seted");
        }

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].onDoorInteraction += DoorOpened;
        }
        defaultColotRoof = roof.material.color;
        houseArea.onPlayerEnteredHouse += HouseEnteredPlayer;
    }

    private void HouseEnteredPlayer(bool obj)
    {
        viewed = obj;
        StartCoroutine(LerpingColorRoof());
    }

    private void DoorOpened(bool opened)
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    private IEnumerator LerpingColorRoof ()
    {
        float aValue = 0;
        float bValue = 0;

        float lerpValue = 0;
        

        if (viewed)
        {
            aValue = 1f;
            bValue = 0f;
        }

        else
        {
            aValue = 0f;
            bValue = 1f;
        }


        var alphaColor = defaultColotRoof;
        while (true)
        {
            float fpsRate = 1.0f / 60.0F;
            yield return new WaitForSeconds(fpsRate);
            lerpValue += fpsRate;


            alphaColor.a = Mathf.Lerp(aValue, bValue, lerpValue);
            roof.material.color = alphaColor;

            if (lerpValue >= 1f)
            {
                yield break;
            }

        }

    }

}
    