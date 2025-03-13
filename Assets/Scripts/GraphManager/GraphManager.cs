using UnityEngine;
using XCharts.Runtime;
using System.Collections;
using Unity.VisualScripting;
using System;

public class GraphManager : MonoBehaviour
{
    int[] height = new int[10];
    GameObject[] chickens;


    BarChart graph;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        graph = gameObject.GetComponent<BarChart>();
        if(graph != null)
        {
            StartCoroutine(Graphing());
        }
        else
        {
            Debug.LogError("graph is null");
        }
            //graph.UpdateData("serie0", 0, 3);
            
    }

    IEnumerator Graphing()
    {
        yield return new WaitForSeconds(1f);
        chickens = GameObject.FindGameObjectsWithTag("chicken");
        if(chickens == null) { Debug.LogError("chicke obj array is null"); }
        ChickenAI[] chickenScripts = new ChickenAI[chickens.Length];

        for (int i = 0; i < chickens.Length; i++)
        {
            chickenScripts[i] = chickens[i].GetComponent<ChickenAI>();
            if (chickenScripts[i] == null) { Debug.LogError("chickenScripts[i] is null"); }
            float chickenSpeedf = chickenScripts[i].speed;
            int chickenSpeed = Convert.ToInt32(chickenSpeedf);
            
            
            //edit bar on graph
            height[chickenSpeed]++;
            //
            

        }
        for(int i = 0; i < 10; i++)
        {
            graph.UpdateData("serie0", i - 1, height[i]);
        }
        
    }
}
