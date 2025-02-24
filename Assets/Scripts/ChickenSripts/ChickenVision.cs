using UnityEngine;

public class ChickenVision : MonoBehaviour
{
    GameObject chicken;
    [SerializeField]ChickenAI chickenScript;
    public bool isTargeting;
    
    void Start()
    {
        Transform parent = transform.parent;
        if(parent != null)
        {
            chicken = parent.Find("Chicken").gameObject;
        }
    }

    private void Update()
    {
        transform.position = chicken.transform.position;
    }
    private void OnTriggerStay(Collider other)
    {
        if(chickenScript != null)
        {
            
            float r = Random.Range(1, 101);
            if (other.CompareTag("food") && r < chickenScript.chanceForFood)
            {
                Debug.Log("r");
                chickenScript.foodSpotted = true;
                chickenScript.foodTrans = other.transform;
            }
            else if (other.CompareTag("chicken") && !chickenScript.ableToBirth)
            {
                if (other.GetComponent<ChickenAI>() != null)
                {
                    ChickenAI otherAI = other.GetComponent<ChickenAI>();
                    
                    if (otherAI.ableToBirth && otherAI.birthCooldown < 1f && otherAI.births < otherAI.maxBirths)
                    {
                        chickenScript.foundMate = true;
                        chickenScript.mateTrans = other.transform;
                        Debug.Log("mateFound");
                        other.GetComponent<ChickenAI>().wait = true;
                        other.GetComponent<ChickenAI>().foundMate = true;
                    }
                    
                }
                else
                {
                    Debug.Log("other chicken script is null");
                }


            }
            else if (other.CompareTag("food"))
            {
                chickenScript.foodSpotted = true;
                chickenScript.foodTrans = other.transform;
            }

            
        }
        else
        {
            Debug.LogError("chicken-script is null");
        }
        
        
    }

    
}
