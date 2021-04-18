using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class ZombieArea : MonoBehaviour, IObjectArea
    {
    public event Action<Character> onCharacterVisible;
    public event Action<Character> onCharacterInvisible;

    private HashSet<Character> charactersVisibles = new HashSet<Character>();

    private const string TAG_PLAYER = "PlayerArea";

    private SphereCollider sphereCollider;
    // Use this for initialization
    void Start()
    {
        Ini();
    }

    private void Ini()
    {
        if (!TryGetComponent(out sphereCollider))
        {
            throw new ZombieAreaException("Sphere colider not exits");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains(TAG_PLAYER))
        {
            CacheCharacter(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains(TAG_PLAYER))
        {
            RemoveCharacter(other);
        }
    }

    private void CacheCharacter(Collider other)
    {
        Character character = null;

        if (other.transform.parent.TryGetComponent(out character))
        {
            if (!charactersVisibles.Contains(character))
            {
                charactersVisibles.Add(character);
                onCharacterVisible?.Invoke(character);
            }
        }
    }

    private void RemoveCharacter(Collider other)
    {
        Character character = null;

        if (other.transform.parent.TryGetComponent(out character))
        {
            if (charactersVisibles.Contains(character))
            {
                charactersVisibles.Remove(character);
                onCharacterInvisible?.Invoke(character);
            }
        }
    }

    public void SetRadius (float raduis)
    {
        if (sphereCollider == null)
        {
            Ini();
        }

        sphereCollider.radius = raduis;
    }
}