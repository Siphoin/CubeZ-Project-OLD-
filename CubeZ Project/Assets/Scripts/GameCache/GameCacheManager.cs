public static class GameCacheManager
{

    private static readonly string NAME_FOLBER_SESSION = "session/";

    private static readonly string NAME_FILE_SESSION = "game_session.json";


   public static GameCache gameCache = new GameCache();

    public static void StartNewGameSession ()
    {
        if (CacheSystem.FileExits(NAME_FOLBER_SESSION, NAME_FILE_SESSION))
        {
            string path = CacheSystem.GetPathAssetsData() + "localData/" + NAME_FOLBER_SESSION + NAME_FILE_SESSION;

            CacheSystem.DeleteFile(path);
        }
        LoaderGameCache.IsLoaded = false;
        gameCache = new GameCache();
    }

}