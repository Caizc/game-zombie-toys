using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector]
    public FrostDebuff frostDebuff;

    [HeaderAttribute("Components")]
    [SerializeField]
    NavMeshAgent navMeshAgent;
    [SerializeField]
    Animator animator;

    [HeaderAttribute("Stink Hit Properties")]
    [SerializeField]
    float runAwayDistance = 10f;

    float originalSpeed;
    bool isRunningAway;
    Vector3 runAwayPosition;

    static WaitForSeconds updateDelay = new WaitForSeconds(0.5f);

    void Reset()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        navMeshAgent.enabled = true;
        isRunningAway = false;

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

            if (isRunningAway)
            {
                navMeshAgent.SetDestination(runAwayPosition);
            }
            else if (null != target)
            {
                navMeshAgent.SetDestination(target.position);
            }

            yield return updateDelay;
        }
    }

    public void Freeze()
    {
        animator.enabled = false;
        originalSpeed = navMeshAgent.speed;
        navMeshAgent.speed = 0f;
    }

    public void UnFreeze()
    {
        animator.enabled = true;
        navMeshAgent.speed = originalSpeed;
    }

    public void Defeated()
    {
        navMeshAgent.enabled = false;

        if (null != frostDebuff)
        {
            frostDebuff.gameObject.SetActive(false);
        }
    }

    public void Runaway()
    {
        isRunningAway = true;
        Vector3 runVector = transform.position - GameManager.instance.enemyTarget.position;
        runAwayPosition = runVector.normalized * runAwayDistance;
    }

    public void ComeBack()
    {
        isRunningAway = false;
    }
}
