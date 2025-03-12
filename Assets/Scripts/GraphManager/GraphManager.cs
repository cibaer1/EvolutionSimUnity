using UnityEngine;
using XCharts.Runtime;
using System.Collections;
using Unity.VisualScripting;
using System;

public class GraphManager : MonoBehaviour
{
    int[] height;
    ChickenAI[] chickenScripts;
    GameObject[] chickens;

    BarChart graph;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        graph = gameObject.GetComponent<BarChart>();
        StartCoroutine(Graphing());
        //graph.UpdateData("serie0", 0, 3);
    }

    IEnumerator Graphing()
    {

        chickens = GameObject.FindGameObjectsWithTag("chicken");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("chicken").Length; i++)
        {
            float chickenSpeedf = chickenScripts[i].speed;
            int chickenSpeed = Convert.ToInt32(chickenSpeedf);
            chickenScripts[i] = chickens[i].GetComponent<ChickenAI>();
            
            //edit bar on graph
            height[chickenSpeed]++;
            //
            

        }
        for(int i = 0; i < 10; i++)
        {
            graph.UpdateData("serie0", i, height[i]);
        }
        yield return new WaitForSeconds(1f);
    }
}
