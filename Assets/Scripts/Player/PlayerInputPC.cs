using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInputPC : MonoBehaviour
{
    [SerializeField]
    PlayerMovement playerMovement;
    [SerializeField]
    PlayerAttack playerAttack;

    void Reset()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        if (!CanUpdate())
        {
            return;
        }

        Move();
        Attack();
    }

    bool CanUpdate()
    {
        if (null == GameManager.instance.player || GameManager.instance.player.transform != transform)
        {
            return false;
        }

        return true;
    }

    void Move()
    {
        if (null == playerMovement)
        {
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        playerMovement.moveDirection = new Vector3(horizontal, 0f, vertical);

        if (null != MouseLocation.instance && MouseLocation.instance.isValid)
        {
            Vector3 lookPoint = MouseLocation.instance.mousePosition - playerMovement.transform.position;
            playerMovement.lookDirection = lookPoint;
        }
    }

    void Attack()
    {
        if (null == playerAttack)
        {
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            playerAttack.Fire();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            playerAttack.StopFiring();
        }
    }
}
