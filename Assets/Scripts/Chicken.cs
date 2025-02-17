using UnityEngine;
using UnityEngine.AI;

public class Chicken : MonoBehaviour
{
    public NavMeshAgent agent;
    public float viewRadius;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        
    }
    private void Update()
    {
        
        float dist = Vector3.Distance(agent.destination, transform.position);
        if(dist < 0.5f)
        {
            float x = Random.Range(viewRadius * -0.5f, viewRadius * 0.5f);
            float y = Random.Range(viewRadius * -0.5f, viewRadius * 0.5f);
            Vector3 randomDest = new Vector3(transform.position.x - x, 0.51f, transform.position.y - y);
            agent.SetDestination(randomDest);
            Debug.Log(randomDest);
        }
    }

}
