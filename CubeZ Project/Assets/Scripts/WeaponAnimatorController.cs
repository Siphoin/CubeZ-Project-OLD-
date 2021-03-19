using UnityEngine;
[RequireComponent(typeof(Animator))]
public class WeaponAnimatorController : MonoBehaviour
{
    [SerializeField] string attackAnimName;

    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.Play(attackAnimName);
        }
    }
}