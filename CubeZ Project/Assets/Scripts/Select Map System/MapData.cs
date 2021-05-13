using UnityEngine;
[CreateAssetMenu(menuName = "Map/Map Data", order = 0)]
public class MapData : ScriptableObject
    {
    [Header("Название карты")]
    [SerializeField]
    private string mapName;

    [Header("Описание карты")]
    [TextArea]
    [SerializeField]
    private string decription;

    [Header("Имя сцены для загрузки карты")]
    [SerializeField]
    private string sceneName;

    [Header("Размер карты (текстом)")]
    [SerializeField]
    private string size;

    [Header("Изображение карты")]
    [SerializeField]
    private Sprite icon;

    public string MapName { get => mapName; }
    public string Decription { get => decription; }
    public string SceneName { get => sceneName; }
    public string Size { get => size; }
    public Sprite Icon { get => icon; }
}