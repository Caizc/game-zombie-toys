using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;
    [HideInInspector]
    public Vector3 lookDirection = Vector3.forward;

    [SerializeField]
    float speed = 6f;
    [SerializeField]
    Rigidbody rigidBody;
    [SerializeField]
    Animator animator;

    bool canMove = true;

    void Reset()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        moveDirection.Set(moveDirection.x, 0f, moveDirection.z);
        rigidBody.MovePosition(transform.position + moveDirection.normalized * speed * Time.deltaTime);

        lookDirection.Set(lookDirection.x, 0f, lookDirection.z);
        rigidBody.MoveRotation(Quaternion.LookRotation(lookDirection));

        animator.SetBool("IsWalking", moveDirection.sqrMagnitude > 0);
    }

    public void Defeated()
    {
        canMove = false;
    }
}
