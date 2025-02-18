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
        if(chickenScript != null)
        {
            if (!isTargeting)
            {
                float r = Random.Range(1, 101);
                if (other.CompareTag("food") && r < chickenScript.chanceForFood)
                {
                    chickenScript.foodSpotted = true;
                    chickenScript.foodTrans = other.transform;
                }
                else if (other.CompareTag("chicken"))
                {
                    if (other.GetComponent<Chicken>() != null)
                    {
                        if (other.GetComponent<Chicken>().ableToBirth && !chickenScript.ableToBirth)
                        {
                            chickenScript.foundMate = true;
                            chickenScript.mateTrans = other.transform;
                        }
                        else if (!other.GetComponent<Chicken>().ableToBirth && chickenScript.ableToBirth)
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
            }
        }
        else
        {
            Debug.LogError("chicken-script is null");
        }
        
        
    }

    
}
