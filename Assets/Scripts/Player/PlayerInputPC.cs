using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInputPC : MonoBehaviour
{
    [SerializeField]
    PlayerMovement playerMovement;
    [SerializeField]
    PlayerAttack playerAttack;
    [SerializeField]
    PauseMenu pauseMenu;

    void Reset()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        if (null != pauseMenu && Input.GetButtonDown("Cancel"))
        {
            pauseMenu.Pause();
        }

        if (!CanUpdate())
        {
            return;
        }

        HandleMoveInput();
        HandleAttackInput();
        HandleAllyInput();
    }

    bool CanUpdate()
    {
        if (null != pauseMenu && pauseMenu.isPaused)
        {
            return false;
        }

        if (null == GameManager.instance.player || GameManager.instance.player.transform != transform)
        {
            return false;
        }

        return true;
    }

    void HandleMoveInput()
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

    void HandleAttackInput()
    {
        if (null == playerAttack)
        {
            return;
        }

        if (Input.GetButtonDown("SwitchAttack"))
        {
            playerAttack.SwitchAttack();
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

    void HandleAllyInput()
    {
        if (Input.GetButtonDown("SummonAlly") && null != GameManager.instance)
        {
            GameManager.instance.SummonAlly();
        }
    }
}
