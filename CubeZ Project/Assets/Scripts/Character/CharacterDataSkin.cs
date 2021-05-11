using System.Collections;
using UnityEngine;

    public class CharacterDataSkin : MonoBehaviour
    {
        [Header("Список глаз")]
        [SerializeField] protected SkinArrayMaterial eyes;

        [Header("Список ног")]
        [SerializeField] protected SkinArrayMaterial legs;

        [Header("Кожа")]
        [SerializeField] protected SkinMaterial skin;

        [Header("Торс")]
        [SerializeField] protected SkinMaterial torso;

        [Header("Волосы")]
       [SerializeField] protected SkinMaterial hair;
    // Use this for initialization
    void Start()
        {

        }



    public void SetColorTorso(Color torosColor)
    {
            torso.SetColorMaterial(torosColor);
    }

    public void SetColorHair(Color colorHair)
    {
            hair.SetColorMaterial(colorHair);
    }

   public void SetColorSkin(Color skinColor)
    {
            skin.SetColorMaterial(skinColor);       
    }

    public void SetColorLegs(Color colorLegs)
    {
            legs.SetColorMaterial(colorLegs);      
    }

    public void SetColorEyes(Color colorEyes)
    {
            eyes.SetColorMaterial(colorEyes);     
    }

}