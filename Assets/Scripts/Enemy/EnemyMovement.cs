using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [HeaderAttribute("Components")]
    [SerializeField]
    NavMeshAgent navMeshAgent;
    [SerializeField]
    Animator animator;

    float originalSpeed;

    static WaitForSeconds updateDelay = new WaitForSeconds(0.5f);

    void Reset()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        navMeshAgent.enabled = true;

        StartCoroutine(ChasePlayer());
    }

    IEnumerator ChasePlayer()
    {
        yield return null;

        if (null == GameManager.instance)
        {
            yield break;
        }

        while (navMeshAgent.enabled)
        {
            Transform target = GameManager.instance.enemyTarget;

            if (null != target)
            {
                navMeshAgent.SetDestination(target.position);
            }
            
            yield return updateDelay;
        }
    }


    public void Defeated()
    {
        navMeshAgent.enabled = false;
    }
}
