[System.Serializable]

    public class AudioData
    {
    public float fxVolume = 0.5f;
    public float musicVolume = 0.2f;

    public AudioData ()
    {

    }

    public AudioData (AudioData copyClass)
    {
        copyClass.CopyAll(this);
    }
}