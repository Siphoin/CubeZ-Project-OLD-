using UnityEngine;

public class AvatarCharacter : CharacterDataSkin
    {
    private const string TAG_LOCAL_AVATAR = "MyPlayerAvatar";


    private  Character localPlayer;


    [SerializeField]
    [ReadOnlyField]
    private bool moving = false;
    private void Start()
    {
        if (tag == TAG_LOCAL_AVATAR)
        {
            if (PlayerManager.Manager == null)
            {
                throw new AvatarCharacterException("player manager not found");
            }

            if (PlayerManager.Manager.Player == null)
            {
                throw new AvatarCharacterException("local player not found");
            }

            localPlayer = PlayerManager.Manager.Player;
            moving = true;
        }
    }

    private void Update()
    {
        if (!moving)
        {
            return;
        }
        MovingToLocalPlayer();
    }

    private void MovingToLocalPlayer()
    {
        if (localPlayer == null)
        {
            Destroy(gameObject);
        }
        Vector3 posPlayer = localPlayer.transform.position;
        Vector3 pos = new Vector3(posPlayer.x, transform.position.y, posPlayer.z);
        transform.position = pos;
    }
}
    