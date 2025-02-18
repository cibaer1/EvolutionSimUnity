using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Chicken : MonoBehaviour
{
    [SerializeField] GameObject visionObj;
    public NavMeshAgent agent;
    public float viewRadius;
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] float maxHunger;
    [SerializeField] float hunger;
    [SerializeField] float hungerGainOnEat;

    public Transform foodTrans;
    public bool foodSpotted;
    bool isFoodDistSet;

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
        if (foodSpotted == false) //set random dest
        {
            if(isFoodDistSet == true) { isFoodDistSet = false; }
            
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
        else if(foodSpotted == true && isFoodDistSet == false && foodTrans != null) //if food got there
        {
            
            agent.SetDestination(new Vector3(foodTrans.position.x, -0.5f, foodTrans.position.z));
            isFoodDistSet = true;
        }
        else if(dist < 0.5f) //if all else fails return to random
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
            if(hunger < 1f) { Destroy(visionObj); Destroy(transform.parent.gameObject); }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "food")
        {
            Debug.Log("chicken trigger");
            Destroy(other.gameObject);
            hunger += hungerGainOnEat;
            if(hunger > maxHunger)
            {
                hunger = maxHunger;
            }
        }
    }

}
