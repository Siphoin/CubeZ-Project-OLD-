using UnityEngine;
[CreateAssetMenu(menuName = "Character/Character Settings", order = 0)]
public class CharacterDataSettings : ScriptableObject
{
    [SerializeField] CharacterData data = new CharacterData();

    public CharacterData GetData()
    {
        return data;
    }

    public CharacterDataSettings()
    {

    }
}