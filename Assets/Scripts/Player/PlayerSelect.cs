using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSelect : MonoBehaviour
{
    [SerializeField]
    PlayerSelect otherCharacter;

    [SerializeField]
    PlayerHealth playerHealth;
    [SerializeField]
    Rigidbody rigidBody;
    [SerializeField]
    CapsuleCollider capsuleCollider;
    [SerializeField]
    Animator animator;

    void Reset()
    {
        playerHealth = GetComponent<PlayerHealth>();
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
    }

    void OnMouseUp()
    {
        if (null != EventSystem.current && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        GameManager.instance.PlayerChosen(playerHealth);

        if (null != otherCharacter)
        {
            otherCharacter.DisableSelectableCharacter();
        }

        enabled = false;
    }

    public void DisableSelectableCharacter()
    {
        capsuleCollider.enabled = false;
        animator.SetTrigger("Die");
    }

    void DeathComplete()
    {
        rigidBody.drag = 0f;
        Destroy(gameObject, 1f);
    }
}
