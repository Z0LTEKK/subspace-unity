using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
            Debug.Log("Game Quit");
        }
    }
}