using UnityEngine;

public class GraphLocationScript : MonoBehaviour
{
    [SerializeField] GameObject graphTarget;
    [SerializeField] bool graphMove;

    private void Update()
    {
        if(graphMove)
        {
            transform.position = graphTarget.transform.position;
        }
    }

}
