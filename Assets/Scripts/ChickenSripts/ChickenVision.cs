using UnityEngine;

public class ChickenVision : MonoBehaviour
{
    [SerializeField] GameObject chicken;
    Chicken chickenScript;
    public bool isTargeting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chickenScript = chicken.GetComponent<Chicken>();
    }

    private void Update()
    {
        transform.position = chicken.transform.position;
    }
    private void OnTriggerStay(Collider other)
    {
        if(!isTargeting)
        {
            int r = Random.Range(1, 100);
            if (other.tag == "food" && r < chickenScript.chanceForFood)
            {
                Debug.Log("found food");
                chickenScript.foodSpotted = true;
                chickenScript.foodTrans = other.transform;
            }
            else if(other.tag == "chicken")
            {
                if(other.GetComponent<Chicken>().ableToBirth && !chickenScript.ableToBirth)
                {
                    chickenScript.foundMate = true;
                    chickenScript.mateTrans = other.transform;
                }
                else if(!other.GetComponent<Chicken>().ableToBirth && chickenScript.ableToBirth)
                {
                    chickenScript.foundMate = true;
                    chickenScript.mateTrans = chicken.transform;
                    chickenScript.wait = true;
                }
                

            }
        }
        
    }

    
}
