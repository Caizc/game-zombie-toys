using UnityEngine;

public class SlimeProjectile : MonoBehaviour
{
    [HeaderAttribute("Projectile Properties")]
    [SerializeField]
    float speed = 20f;
    [SerializeField]
    float projectileRadius = 1f;

    [HeaderAttribute("Projectile References")]
    [SerializeField]
    SlimeDebuff slimeDebuff;
    [SerializeField]
    AVPlayer slimeHit;

    Transform attackTarget;
    bool isFlying;

    void OnEnable()
    {
        isFlying = false;
    }

    public void StartPath(Transform target)
    {
        attackTarget = target;
        isFlying = true;
    }

    void Update()
    {
        if (!isFlying)
        {
            return;
        }

        if (null == attackTarget)
        {
            gameObject.SetActive(false);
        }

        transform.LookAt(attackTarget);
        transform.Translate(0f, 0f, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, attackTarget.position) <= projectileRadius)
        {
            Explode();
        }
    }

    void Explode()
    {
        isFlying = false;

        if (null != slimeHit)
        {
            slimeHit.transform.position = attackTarget.position;
            slimeHit.Play();
        }

        EnemyAttack enemy = attackTarget.GetComponent<EnemyAttack>();

        if (null != enemy)
        {
            slimeDebuff.gameObject.SetActive(true);
            slimeDebuff.AttachToEnemy(enemy);
        }

        gameObject.SetActive(false);
    }
}
