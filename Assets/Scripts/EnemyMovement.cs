using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject goal;
    public float distanceTraveled = 0;
    public GameObject spawner;

    private Transform destination;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent.enabled == true)
        {
            destination = GameObject.FindWithTag("Destination").transform;
            Debug.Log(destination);
            agent.destination = destination.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceTraveled += Time.deltaTime * agent.velocity.magnitude;
        if (agent.remainingDistance <= 0.2f)
        {
            //Here we can add the logic for what happens when the player 'leaks' enemies
            Destroy(this.gameObject);
        }
    }
}
