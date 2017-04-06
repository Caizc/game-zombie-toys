using System.Collections;
using UnityEngine;

public class StinkHit : MonoBehaviour
{
    [SerializeField]
    float explosionDuration = 4f;
    [SerializeField]
    LayerMask whatIsShootable;

    ArrayList enemies = new ArrayList();

    bool isStopExploding;

    void OnEnable()
    {
        enemies.Clear();
        isStopExploding = false;

        Invoke("StopExploding", explosionDuration);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isStopExploding)
        {
            return;
        }

        EnemyMovement enemyMovement = other.gameObject.GetComponent<EnemyMovement>();

        if (null == enemyMovement)
        {
            return;
        }

        enemies.Add(enemyMovement);
        enemyMovement.Runaway();
    }

    void StopExploding()
    {
        isStopExploding = true;

        foreach (EnemyMovement enemyMovement in enemies)
        {
            if (null != enemyMovement)
            {
                enemyMovement.ComeBack();
            }
        }

        gameObject.SetActive(false);
    }
}
