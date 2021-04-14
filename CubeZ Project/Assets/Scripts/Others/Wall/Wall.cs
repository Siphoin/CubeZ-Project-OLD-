using System.Collections;
using UnityEngine;

    public class Wall : MonoBehaviour, IGeterHit
    {
    [Header("Стартовое здоровье")]
    [SerializeField] private int startHealth = 0;

    private int currentHealth = 0;

    public int CurrentHealth { get => currentHealth; }
    // Use this for initialization
    void Start()
        {
        if (startHealth <= 0)
        {
            throw new WallException($"start health not valid ({name}) Value: {startHealth}");
        }


        currentHealth = startHealth;
        }

    public void Hit (int hitValue, bool playHitAnim = true)
    {
        currentHealth = Mathf.Clamp(currentHealth - hitValue, 0, startHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}