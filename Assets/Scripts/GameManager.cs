using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public float globalSpeed = 1f;
    [SerializeField] Text TimeText;
    [SerializeField] Text DeathsToHungerTxt;
    [SerializeField] Text birthsTxt;
    int time;
    int deathsToHunger;
    int births;
    

    private void Start()
    {
        StartCoroutine(timeCountUp());
    }
    private void Update()
    {
        Time.timeScale = globalSpeed;
    }

    public void hungerDeath()
    {
        deathsToHunger++;
        DeathsToHungerTxt.text = "Chickens Starved: " + deathsToHunger;
    }
    public void chickenBirth()
    {
        births++;
        birthsTxt.text = "Births: " + births;
    }
    IEnumerator timeCountUp()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            time++;
            TimeText.text = "Time Elapsed: " + time;
        }
    }
}
