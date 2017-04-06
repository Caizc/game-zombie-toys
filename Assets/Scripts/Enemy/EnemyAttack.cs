using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [HideInInspector]
    public SlimeDebuff slimeDebuff;

    [SerializeField]
    float timeBetweenAttacks = 0.5f;
    [SerializeField]
    int attackDamage = 10;
    [SerializeField]
    Animator animator;

    bool canAttack;
    bool playerInRange;
    WaitForSeconds attackDelay;

    void Reset()
    {
        animator = GetComponent<Animator>();
    }

    void Awake()
    {
        attackDelay = new WaitForSeconds(timeBetweenAttacks);
    }

    void OnEnable()
    {
        slimeDebuff = null;
        canAttack = true;
        StartCoroutine(AttackPlayer());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            playerInRange = false;
        }
    }

    IEnumerator AttackPlayer()
    {
        yield return null;

        if (null == GameManager.instance)
        {
            yield break;
        }

        while (canAttack && CheckPlayerStatus())
        {
            if (playerInRange && null == slimeDebuff)
            {
                GameManager.instance.player.TakeDamage(attackDamage);
            }

            yield return attackDelay;
        }
    }

    bool CheckPlayerStatus()
    {
        if (GameManager.instance.player.IsAlive())
        {
            return true;
        }
        else
        {
            animator.SetTrigger("PlayerDead");

            Defeated();

            return false;
        }
    }

    public void Defeated()
    {
        canAttack = false;

        if (null != slimeDebuff)
        {
            slimeDebuff.ReleaseEnemy();
        }
    }
}
