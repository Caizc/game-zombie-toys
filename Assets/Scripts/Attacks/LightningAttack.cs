using UnityEngine;

public class LightningAttack : MonoBehaviour
{
    [HeaderAttribute("Weapon Specs")]
    public float cooldown = 1f;

    [SerializeField]
    int damage = 20;
    [SerializeField]
    float range = 20.0f;
    [SerializeField]
    LayerMask strikeableMask;

    [HeaderAttribute("Weapon References")]
    [SerializeField]
    LightningBolt lightningBolt;
    [SerializeField]
    AVPlayer lightningHit;

    public void Fire()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, strikeableMask))
        {
            lightningHit.transform.position = hit.point;
            lightningHit.Play();

            lightningBolt.endPoint = hit.point;

            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

            if (null != enemyHealth)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
        else
        {
            lightningBolt.endPoint = ray.GetPoint(range);
        }

        lightningBolt.gameObject.SetActive(true);
    }
}
