using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class TimeGameSessionManager : MonoBehaviour
    {
        private Character localPlayer;
        // Use this for initialization
        void Start()
        {
            if (PlayerManager.Manager == null)
            {
                throw new TimeGameSessionManagerException("player manager not found");
            }
            if (PlayerManager.Manager.Player == null)
            {
                throw new TimeGameSessionManagerException("player not found");
            }

            localPlayer = PlayerManager.Manager.Player;

            StartCoroutine(TickTimeSession());

        }

        private IEnumerator TickTimeSession ()
        {
            while (localPlayer != null && !localPlayer.IsDead)
            {
                yield return new WaitForSecondsRealtime(1);

                GameCacheManager.gameCache.timeSession = GameCacheManager.gameCache.timeSession.AddSeconds(1);
            }
        }

    }
}