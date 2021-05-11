using UnityEngine;

public class ConverterCharacterMaterialsOnAvatar : CharacterDataSkin, IRemoveObject
    {
        private const string TAG_AVATAR = "MyPlayerAvatar";
        
        // Use this for initialization
        void Start()
        {
        ConvertForAvatar();
        Remove();
        }

        private void ConvertForAvatar ()
        {
            AvatarCharacter avatar = GameObject.FindGameObjectWithTag(TAG_AVATAR).GetComponent<AvatarCharacter>();

        if (eyes != null)
        {
            Debug.Log(eyes.GetColor());
            avatar.SetColorEyes(eyes.GetColor());
        }

        if (legs != null)
        {
            avatar.SetColorLegs(legs.GetColor());
        }

        if (skin != null)
        {
            avatar.SetColorSkin(skin.GetColor());
        }

        if (torso != null)
        {
            avatar.SetColorTorso(torso.GetColor());
        }


        if (hair != null)
        {

            avatar.SetColorHair(hair.GetColor());
        }



    }

    public void Remove()
    {
        Destroy(this);
    }
}