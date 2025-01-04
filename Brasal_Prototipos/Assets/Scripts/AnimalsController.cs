using UnityEngine;
using UnityEngine.AI;

public class AnimalsController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] Transform targetPos;

    private bool scared = true;

    void Update()
    {
        if (scared)
        {
            ScaredBehaviour();
        }
    }

    void ScaredBehaviour()
    {
        Vector3 nextPos;

        if (HasAgentStopped())
        {
            float nextPos_x = Random.Range((transform.position.x - 5), (transform.position.x + 5) + 1);
            float nextPos_z = Random.Range((transform.position.z - 5), (transform.position.z + 5) + 1);

            nextPos = new Vector3(nextPos_x, transform.position.y, nextPos_z);

            agent.SetDestination(nextPos);
        }
    }

    public void Interacting()
    {
        scared = false;
        agent.SetDestination(transform.position);
    }

    public void RunAway()
    {
        gameObject.layer = 9;
        agent.SetDestination(targetPos.position);
    }

    bool HasAgentStopped()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }
}
