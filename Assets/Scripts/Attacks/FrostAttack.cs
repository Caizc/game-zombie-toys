using UnityEngine;

public class FrostAttack : MonoBehaviour
{
    [HeaderAttribute("Weapon Specs")]
    [SerializeField]
    int maxFreezableEnemies = 20;

    [SerializeField]
    GameObject frostDebuffPrefab;
    [SerializeField]
    GameObject frostCone;
    [SerializeField]
    MeshCollider frostArc;

    FrostDebuff[] debuffs;
    bool isFiring = false;

    void Reset()
    {
        frostCone = transform.GetChild(0).gameObject;
        frostArc = GetComponentInChildren<MeshCollider>();
    }

    void Awake()
    {
        debuffs = new FrostDebuff[maxFreezableEnemies];

        for (int i = 0; i < maxFreezableEnemies; i++)
        {
            GameObject obj = (GameObject)Instantiate(frostDebuffPrefab);
            obj.SetActive(false);
            debuffs[i] = obj.GetComponent<FrostDebuff>();
        }
    }

    void OnDisable()
    {
        StopFiring();
    }

    public void Fire()
    {
        if (!isFiring)
        {
            frostCone.SetActive(true);
            frostArc.enabled = true;

            isFiring = true;
        }
    }

    public void StopFiring()
    {
        if (!isFiring)
        {
            return;
        }

        frostCone.SetActive(false);
        frostArc.enabled = false;

        isFiring = false;

        for (int i = 0; i < debuffs.Length; i++)
        {
            if (debuffs[i].gameObject.activeInHierarchy)
            {
                debuffs[i].ReleaseEnemy();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyMovement enemy = other.GetComponent<EnemyMovement>();

        if (null == enemy)
        {
            return;
        }

        if (null != enemy.frostDebuff)
        {
            enemy.frostDebuff.AttachToEnemy(enemy);
        }
        else
        {
            AttachDebuffToEnemy(enemy);
        }
    }

    void OnTriggerExit(Collider other)
    {
        EnemyMovement enemy = other.GetComponent<EnemyMovement>();

        if (null == enemy)
        {
            return;
        }

        if (null != enemy.frostDebuff)
        {
            enemy.frostDebuff.ReleaseEnemy();
        }
    }

    void AttachDebuffToEnemy(EnemyMovement enemy)
    {
        for (int i = 0; i < debuffs.Length; i++)
        {
            if (!debuffs[i].gameObject.activeInHierarchy)
            {
                debuffs[i].gameObject.SetActive(true);
                debuffs[i].AttachToEnemy(enemy);

                return;
            }
        }
    }
}
