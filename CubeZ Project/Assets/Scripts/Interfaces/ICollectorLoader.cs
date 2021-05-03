using System.Collections;

public interface ICollectorLoader
{
    void Load();
    IEnumerator LoadAsync();
}