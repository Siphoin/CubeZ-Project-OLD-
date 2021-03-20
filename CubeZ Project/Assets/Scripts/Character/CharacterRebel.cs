using System;
using UnityEngine;

    public class CharacterRebel : MonoBehaviour, IInvokerMono
    {
        [SerializeField, ReadOnlyField] CharacterDataSettings characterDataSettings;
        private const string PATH_CHARACTER_SETTINGS = "Character/CharacterSettings";
    private const string TAG_ZOMBIE_SPAWNER = "ZombieSpawner";
    private ZombieSpawner zombieSpawner;
        // Use this for initialization
        void Start()
        {
            characterDataSettings = Resources.Load<CharacterDataSettings>(PATH_CHARACTER_SETTINGS);

            if (characterDataSettings == null)
            {
                throw new CharacterRebelException("character data settings not found");
            }

        zombieSpawner = GameObject.FindGameObjectWithTag(TAG_ZOMBIE_SPAWNER).GetComponent<ZombieSpawner>();

        CallInvokingMethod(Rebel, characterDataSettings.GetData().rebelTime);
        }

    private void Rebel()
    {
        zombieSpawner.CreateZombie(transform.position, transform.rotation);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
        {

        }

        public void CallInvokingEveryMethod(Action method, float time)
        {
            InvokeRepeating(method.Method.Name, time, time);
        }

        public void CallInvokingMethod(Action method, float time)
        {
            Invoke(method.Method.Name, time);
        }
    }