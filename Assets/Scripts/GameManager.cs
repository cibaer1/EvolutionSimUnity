using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float globalSpeed = 1f;

    private void Update()
    {
        Time.timeScale = globalSpeed;
    }
}
