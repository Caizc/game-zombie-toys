using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [HeaderAttribute("Attacks")]
    [SerializeField]
    LightningAttack lightningAttack;

    [HeaderAttribute("UI")]
    [SerializeField]
    Countdown countdown;

    float attackCooldown = 0f;
    float timeOfLastAttack = 0f;
    bool canAttack = true;

    public void Fire()
    {
        if (!ReadyToAttack() || !canAttack)
        {
            return;
        }

        ShootLightning();
    }

    public void StopFiring()
    {
        if (!ReadyToAttack() || !canAttack)
        {
            return;
        }
    }

    void ShootLightning()
    {
        if (null == lightningAttack)
        {
            return;
        }

        lightningAttack.Fire();

        attackCooldown = lightningAttack.cooldown;

        BeginCountdown();
    }

    bool ReadyToAttack()
    {
        return Time.time >= timeOfLastAttack + attackCooldown;
    }

    void BeginCountdown()
    {
        timeOfLastAttack = Time.time;
        if (null != countdown)
        {
            countdown.BeginCountdown(attackCooldown);
        }
    }

    public void Defeated()
    {
        canAttack = false;

        DisableAttacks();
    }

    void DisableAttacks()
    {
        if (null != lightningAttack)
        {
            lightningAttack.gameObject.SetActive(false);
        }
    }
}
