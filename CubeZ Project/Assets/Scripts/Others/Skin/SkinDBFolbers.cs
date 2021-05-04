using UnityEngine;
[CreateAssetMenu(menuName = "Db/Skin Folbers", order = 0)]
public class SkinDBFolbers : ScriptableObject
    {
    [Header("Список папок просматриваемых в папке Textures/Resources")]
    [SerializeField]
    private string[] folbersList;

    public string[] FolbersList { get => folbersList; }

    public void SetListFolbers (string[] list)
    {
        folbersList = list;
    }
}