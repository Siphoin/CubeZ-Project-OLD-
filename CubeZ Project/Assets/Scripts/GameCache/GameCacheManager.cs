public static class GameCacheManager
{
    public static GameCache gameCache = new GameCache();

    public static void StartNewGameSession ()
    {
        gameCache = new GameCache();
    }

}