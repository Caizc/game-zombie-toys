using System.Collections;
using UnityEngine;

public class SlimeDebuff : MonoBehaviour
{
    [SerializeField]
    float effectDuration = 3f;
    [SerializeField]
    int attacksPerSecond = 2;
    [SerializeField]
    int damagePerAttack = 10;

    EnemyAttack targetAttack;
    EnemyHealth targetHealth;
    WaitForSeconds attackDelay;

    void Awake()
    {
        attackDelay = new WaitForSeconds(1f / attacksPerSecond);
    }

    public void AttachToEnemy(EnemyAttack enemy)
    {
        targetAttack = enemy;
        targetHealth = enemy.GetComponent<EnemyHealth>();

        targetAttack.slimeDebuff = this;

        transform.parent = targetAttack.transform;
        transform.localPosition = new Vector3(0f, 1f, 0f);

        StartCoroutine(AttackEnemy());
    }

    IEnumerator AttackEnemy()
    {
        int totalAttacks = Mathf.RoundToInt(effectDuration * attacksPerSecond);

        for (int i = 0; i < totalAttacks; i++)
        {
            targetHealth.TakeDamage(damagePerAttack);

            yield return attackDelay;
        }

        ReleaseEnemy();
    }

    public void ReleaseEnemy()
    {
        targetAttack = null;
        targetHealth = null;
        transform.parent = null;

        gameObject.SetActive(false);
    }
}
