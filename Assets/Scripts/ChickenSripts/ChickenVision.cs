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
            else if (other.CompareTag("chicken"))
            {
                if (other.GetComponent<ChickenAI>() != null)
                {
                    if (other.GetComponent<ChickenAI>().ableToBirth && !chickenScript.ableToBirth)
                    {
                        chickenScript.foundMate = true;
                        chickenScript.mateTrans = other.transform;
                        Debug.Log("mateFound");
                    }
                    else if (!other.GetComponent<ChickenAI>().ableToBirth && chickenScript.ableToBirth)
                    {
                        chickenScript.foundMate = true;
                        chickenScript.mateTrans = chicken.transform;
                        chickenScript.wait = true;
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
