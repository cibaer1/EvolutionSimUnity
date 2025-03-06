using UnityEngine;

public class GraphManager : MonoBehaviour
{
    GraphHandler graph;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        graph = GameObject.Find("GraphHandler").GetComponent<GraphHandler>();

        //graph.SetCornerValues(new Vector2(-400, 200), new Vector2(-200, 350));
        //graph.UpdateGraph();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
