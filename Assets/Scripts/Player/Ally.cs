using UnityEngine;
using UnityEngine.AI;

public class Ally : MonoBehaviour
{
    public float duration;

    [SerializeField]
    NavMeshAgent navMeshAgent;

    void Reset()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Move(Vector3 point)
    {
        navMeshAgent.SetDestination(point);
    }
}
