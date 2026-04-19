using UnityEngine;

public class AnimalAnimator : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator != null)
        {
            animator.SetFloat("Vert", 1f);  // 1 = movimento (sai do idle)
            animator.SetFloat("State", 1f); // 1 = run
        }
    }
}