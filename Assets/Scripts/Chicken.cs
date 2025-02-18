using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Chicken : MonoBehaviour
{
    public NavMeshAgent agent;
    public float viewRadius;
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] float maxHunger;
    [SerializeField] float hunger;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;
        hunger = maxHunger;

        StartCoroutine(passiveDegen());
        
    }
    private void Update()
    {
        Movement();
        
    }

    void Movement()
    {
        float dist = Vector3.Distance(agent.destination, transform.position);
        if (dist < 0.5f)
        {
            float x = Random.Range(viewRadius * -0.5f, viewRadius * 0.5f);
            float y = Random.Range(viewRadius * -0.5f, viewRadius * 0.5f);
            Vector3 randomDest = new Vector3(transform.position.x - x, 0.51f, transform.position.y - y);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDest, out hit, 5.0f, NavMesh.AllAreas))
            {
                agent.SetDestination(randomDest);
            }


        }
    }
    IEnumerator passiveDegen()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            hunger -= 1f;
            if(hunger < 1f) { Destroy(this.gameObject); }
        }

    }

}
