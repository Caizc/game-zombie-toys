using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    float timeBetweenAttacks = 0.5f;
    [SerializeField]
    int attackDamage = 10;
    [SerializeField]
    Animator animator;

    bool canAttack;
    bool playerInRange = false;
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
            if (playerInRange)
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
    }
}
