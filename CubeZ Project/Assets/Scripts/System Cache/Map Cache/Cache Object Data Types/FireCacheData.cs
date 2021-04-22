[System.Serializable]
    public class FireCacheData : CacheObjectData
    {
    public int countParticles;
    public float intensity;

    public FireCacheData ()
    {

    }

    public FireCacheData (string idFire, int countParticles, float intensity)
    {
        idObject = idFire;
        this.countParticles = countParticles;
        this.intensity = intensity;
    }
    }