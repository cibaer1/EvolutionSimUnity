using UnityEngine;

public class ChickenVision : MonoBehaviour
{
    [SerializeField] GameObject chicken;
    Chicken chickenScript;
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
        if(other.tag == "food")
        {
            Debug.Log("found food");
            chickenScript.foodSpotted = true;
            chickenScript.foodTrans = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "food")
        {
            
            chickenScript.foodSpotted = false;
        }
    }
}
