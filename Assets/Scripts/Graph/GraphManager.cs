using System.Collections;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    float[] height;
    float[] storedValues;
    GraphHandler graph;
    ChickenAI[] chickenScripts;
    GameObject[] chickens;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        graph = GameObject.Find("GraphHandler").GetComponent<GraphHandler>();

        StartCoroutine(Graphing());
    }

    IEnumerator Graphing()
    {
        
        int valueAmount = 0;
        chickens = GameObject.FindGameObjectsWithTag("chicken");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("chicken").Length; i++)
        {
            bool foundStoredValue = false;
            float chickenSpeed = chickenScripts[i].speed;
            chickenScripts[i] = chickens[i].GetComponent<ChickenAI>();
            for(int b = 0; b < storedValues.Length; b++)
            {
                if (storedValues[b] == chickenSpeed)
                {
                    height[i]++;
                    foundStoredValue = true;
                }
            }
            graph.CreatePoint(new Vector2(chickenSpeed, height[i]));
            if(!foundStoredValue)
            {
                storedValues[valueAmount] = chickenSpeed;
                valueAmount++;
            }
            
        }
        graph.UpdateGraph();
        yield return new WaitForSeconds(1f);
    }
}
