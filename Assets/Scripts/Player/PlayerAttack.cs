using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [HeaderAttribute("Attacks")]
    [SerializeField]
    LightningAttack lightningAttack;
    [SerializeField]
    FrostAttack frostAttack;
    [SerializeField]
    StinkAttack stinkAttack;
    [SerializeField]
    SlimeAttack slimeAttack;
    [SerializeField]
    int numberOfAttacks;

    [HeaderAttribute("UI")]
    [SerializeField]
    Countdown countdown;

    int attackIndex = 0;
    float attackCooldown = 0f;
    float timeOfLastAttack = 0f;
    bool canAttack = true;

    public void SwitchAttack()
    {
        if (!canAttack)
        {
            return;
        }

        attackIndex++;

        if (attackIndex >= numberOfAttacks)
        {
            attackIndex = 0;
        }

        DisableAttacks();

        switch (attackIndex)
        {
            case 0:
                if (null != lightningAttack)
                {
                    lightningAttack.gameObject.SetActive(true);
                }
                break;

            case 1:
                if (null != frostAttack)
                {
                    frostAttack.gameObject.SetActive(true);
                }
                break;

            case 2:
                if (null != stinkAttack)
                {
                    stinkAttack.gameObject.SetActive(true);
                }
                break;

            case 3:
                if (null != slimeAttack)
                {
                    slimeAttack.gameObject.SetActive(true);
                }
                break;
        }
    }

    public void Fire()
    {
        if (!ReadyToAttack() || !canAttack)
        {
            return;
        }

        switch (attackIndex)
        {
            case 0:
                ShootLightning();
                break;

            case 1:
                ToggleFrost(true);
                break;
        }
    }

    public void StopFiring()
    {
        if (!ReadyToAttack() || !canAttack)
        {
            return;
        }

        switch (attackIndex)
        {
            case 1:
                ToggleFrost(false);
                break;

            case 2:
                ShootStink();
                break;

            case 3:
                ShootSlime();
                break;
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

    void ToggleFrost(bool isAttacking)
    {
        if (null == frostAttack)
        {
            return;
        }

        if (isAttacking)
        {
            frostAttack.Fire();
        }
        else
        {
            frostAttack.StopFiring();
        }
    }

    void ShootStink()
    {
        if (null == stinkAttack)
        {
            return;
        }

        if (stinkAttack.Fire())
        {
            attackCooldown = stinkAttack.cooldown;

            BeginCountdown();
        }
    }

    void ShootSlime()
    {
        if (null == slimeAttack)
        {
            return;
        }

        if (slimeAttack.Fire())
        {
            attackCooldown = slimeAttack.cooldown;

            BeginCountdown();
        }
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

        if (null != frostAttack)
        {
            frostAttack.gameObject.SetActive(false);
        }

        if (null != stinkAttack)
        {
            stinkAttack.gameObject.SetActive(false);
        }

        if (null != slimeAttack)
        {
            slimeAttack.gameObject.SetActive(false);
        }
    }
}
