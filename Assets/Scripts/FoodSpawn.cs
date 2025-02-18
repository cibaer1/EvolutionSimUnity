using System.Collections;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    [SerializeField] int numberOfFood;
    [SerializeField] float mapSize;

    [SerializeField] GameObject _bush;
    [SerializeField] GameObject _bushHolder;
    [SerializeField] float foodSpawnDelay;
    void Start()
    {
        StartCoroutine(spawnFood());
    }

    IEnumerator spawnFood()
    {
        while(true)
        {
            for(int i = 0; i < numberOfFood; i++)
            {
                float x = Random.Range(mapSize/2 * -1f, mapSize/2);
                float y = Random.Range(mapSize/2 * -1f, mapSize/2);

                Vector3 randomPoint = new Vector3(x, -0.4f, y);
                Instantiate(_bush, randomPoint, Quaternion.identity, _bushHolder.transform);
            }
            yield return new WaitForSeconds(foodSpawnDelay);
        }
    }
}
