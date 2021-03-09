using UnityEditor;
using UnityEngine;

    [CreateAssetMenu(menuName = "Zombie/Zombie Settings", order = 0)]
    public  class SettingsZombie : ScriptableObject
    {
    [SerializeField] SettingsZombieData data = new SettingsZombieData();

    public SettingsZombieData GetData ()
    {
        return data;
    }

    public SettingsZombie ()
    {

    }
    }