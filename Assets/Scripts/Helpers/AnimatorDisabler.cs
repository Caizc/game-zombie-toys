using UnityEngine;

public class AnimatorDisabler : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    void Reset()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.enabled = false;
    }
}
