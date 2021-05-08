using UnityEngine;

namespace CBZ.API.Assembly
{
    [CreateAssetMenu(menuName = "CBZ API/List Assembly", order = 0)]
    public class AssemblyUserAPIList : ScriptableObject
    {
        [Header("Список библиотек, которые может поддерживать IronPython в Realtime")]
        [SerializeField]
        private string[] assemblyList = new string[]
        {
            "UnityEngine",
            "CBZ.API",
            "CBZ.API.Random",
            "CBZ.API.Math",
        };

        public string[] AssemblyList { get => assemblyList; }
    }
}