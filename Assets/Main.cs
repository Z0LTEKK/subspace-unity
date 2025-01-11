using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
  void Start()
  {
    

  }
    // Update is called once per frame
    void Update()
    {
       if  (Input.GetKey(KeyCode.Q))
      {
       Application.Quit();
       Debug.Log("Game Quit");
      }

    }
}
