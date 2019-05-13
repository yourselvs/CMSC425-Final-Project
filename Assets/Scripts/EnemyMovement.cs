using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform goal;
    public float distanceTraveled = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent.enabled == true)
        {
            agent.destination = goal.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceTraveled += Time.deltaTime * agent.velocity.magnitude;
        if (agent.remainingDistance <= 0.2f)
        {
            Destroy(this.gameObject);
        }
    }
}
