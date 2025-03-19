using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChickenAI : MonoBehaviour
{
    [SerializeField] GameObject visionObj;
    public NavMeshAgent agent;
    [HideInInspector]public bool foundMate;
    [HideInInspector] public bool wait;
    [HideInInspector] public Transform mateTrans;
    //start of block genes
    public float viewRadius;
    public float maxHealth;
    public float health;
    public float maxHunger;
    public float hunger;
    public float hungerGainOnEat;
    public float hungerConsumption;
    public float speed;
    public float birthWait;
    public int maxAge;
    //end of block genes

    int waitFailsave = 5;
    int age;
    [HideInInspector] public float birthCooldown;
    [HideInInspector] public Transform foodTrans;
    [HideInInspector] public bool foodSpotted;
    public bool ableToBirth;
    [HideInInspector] bool isFoodDistSet;
    public int births;
    public int maxBirths;
    [SerializeField] ChickenVision visionScript;
    [SerializeField] int geneMutationChance;
    [SerializeField] GameObject chickenClone;
    [SerializeField] GameObject chickenParent;
    GameManager gameManagerScript;
    float degenTimer;

    private void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        birthCooldown = 5f;
        health = maxHealth;
        hunger = maxHunger;
        if (Random.Range(1, 3) == 1)
        {
            ableToBirth = true;
        }

        //StartCoroutine(passiveDegen());

    }
    private void Update()
    {
        Movement();

        degenTimer += Time.deltaTime;
        if(degenTimer >= 1f)
        {
            degenTimer = 0f;
            hunger -= hungerConsumption;
            birthCooldown--;
            age++;
            if (hunger < 1f) { gameManagerScript.hungerDeath(); Destroy(visionObj); Destroy(transform.parent.gameObject); }
            if (age > maxAge) { Destroy(visionObj); Destroy(transform.parent.gameObject); }
            if (wait && waitFailsave < 1) { wait = false; waitFailsave = 5; }
            else if (wait) { waitFailsave--; }
        }
    }

    void Movement()
    {
        float dist = Vector3.Distance(agent.destination, transform.position);
        if (!foodSpotted && !foundMate && !wait && Random.Range(0, 100) < 20) //set random dest
        {
            if (isFoodDistSet == true) { isFoodDistSet = false; }
            visionScript.isTargeting = true;
            if (dist < 0.5f)
            {
                setRandomDest();


            }
        }
        else if (!wait && foodSpotted && foodTrans != null) //if food go there
        {
            visionScript.isTargeting = true;
            agent.SetDestination(new Vector3(foodTrans.position.x, -0.5f, foodTrans.position.z));
        }
        else if (!wait && foundMate && mateTrans != null && !ableToBirth) //if mate found and unable to birth go to mate
        {
            visionScript.isTargeting = true;
            //Debug.Log("mate found");
            agent.SetDestination(new Vector3(mateTrans.position.x, -0.5f, mateTrans.position.z));
        }
        else if (!wait && dist < 0.5f) //if all else fails return to random
        {
            visionScript.isTargeting = true;
            setRandomDest();

        }
    }
    void setRandomDest()
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
    IEnumerator passiveDegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            hunger -= hungerConsumption;
            birthCooldown--;
            age++;
            if (hunger < 1f) { gameManagerScript.hungerDeath(); Destroy(visionObj); Destroy(transform.parent.gameObject); }
            if(age > maxAge) { Destroy(visionObj); Destroy(transform.parent.gameObject); }
            if(wait && waitFailsave < 1) { wait = false; waitFailsave = 5; }
            else if(wait) { waitFailsave--; }
        }

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "food")
        {
            
            visionScript.isTargeting = false;
            //Debug.Log("chicken trigger");
            Destroy(other.gameObject);
            hunger += hungerGainOnEat;
            if (hunger > maxHunger)
            {
                hunger = maxHunger;
            }
        }
        else if (other.tag == "chicken" && foundMate == true)
        {
            ChickenAI otherScript = other.GetComponent<ChickenAI>();
            if (ableToBirth && birthCooldown < 1f && !otherScript.ableToBirth && births < maxBirths)
            {
                births++;
                wait = false;
                birth(otherScript.viewRadius, otherScript.maxHealth, otherScript.maxHunger, otherScript.hungerGainOnEat, otherScript.hungerConsumption, otherScript.speed);
            }
        }
    }
    #region birth
    void birth(float otherViewRadius, float otherMaxHealth, float otherMaxHunger, float otherHungerGainOnEat, float otherHungerConsumption, float otherSpeed) //prefix _ means temp here
    {
        float _viewRadius;
        float _maxHealth;
        float _maxHunger;
        float _hungerGainOnEat;
        float _hungerConsumption;
        float _speed;

        //get base genetics from mom/dad
        if(Random.Range(1, 3) == 1) { _viewRadius = viewRadius; }
        else { _viewRadius = otherViewRadius; }
        if (Random.Range(1, 3) == 1) { _maxHealth = maxHealth; }
        else { _maxHealth = otherMaxHealth; }
        if (Random.Range(1, 3) == 1) { _maxHunger = maxHunger; }
        else { _maxHunger = otherMaxHunger; }
        if (Random.Range(1, 3) == 1) { _hungerGainOnEat = hungerGainOnEat; }
        else { _hungerGainOnEat = otherHungerGainOnEat; }
        if (Random.Range(1, 3) == 1) { _hungerConsumption = hungerConsumption; }
        else { _hungerConsumption = otherHungerConsumption; }
        if (Random.Range(1, 3) == 1) { _speed = speed; }
        else { _speed = otherSpeed; }

        //genetic mutations
        //if (Random.Range(1, 101) < geneMutationChance) { _viewRadius += Random.Range(-5, 6); }
        //if (Random.Range(1, 101) < geneMutationChance) { _chanceForFood += Random.Range(-5, 6); }
        //if (Random.Range(1, 101) < geneMutationChance) { _maxHealth += Random.Range(-5, 6); }
        //if (Random.Range(1, 101) < geneMutationChance) { _maxHunger += Random.Range(-5, 6); }
        //if (Random.Range(1, 101) < geneMutationChance) { _hungerGainOnEat += Random.Range(-5, 6); }
        //if (Random.Range(1, 101) < geneMutationChance) { _hungerConsumption += Random.Range(-5, 6); }
        if(Random.Range(1, 101) < geneMutationChance) 
        {
            Debug.Log("geneModified");
            float r = Random.Range(-3, 4);
            _speed += r;
            //_hungerConsumption += r;

        }

        //the actual "birth"
        GameObject babyChicken = Instantiate(chickenClone, new Vector3(transform.position.x + Random.Range(-1.5f, 1.6f), transform.position.y, transform.position.z + Random.Range(-1.5f, 1.6f)), Quaternion.identity); //in chickenHolder
        ChickenAI babyScript = babyChicken.GetComponentInChildren<ChickenAI>();

        babyScript.viewRadius = _viewRadius;
        babyScript.maxHealth = _maxHealth;
        babyScript.maxHunger = _maxHunger;
        babyScript.hungerGainOnEat = _hungerGainOnEat;
        babyScript.hungerConsumption = _hungerConsumption;
        babyScript.speed = _speed;

        hunger -= 5;
        birthCooldown = birthWait;
        //Debug.Log("child born");
    }
    #endregion

}


