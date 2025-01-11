using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ExhaustScript : NetworkBehaviour

{
    // Start is called before the first frame update
  public ParticleSystem exhaust;

    void Start()
    {
      
       exhaust = gameObject.GetComponent<ParticleSystem>();
    }
void ExhaustAxis()
{
        if (Input.GetAxisRaw("Vertical") ==0)
        
    {
        exhaust.Stop();
        
    }
    else
            if (!exhaust.isPlaying){
    {
exhaust.Play();
    }
    }

}
    // Update is called once per frame
    void Update()
    {
    if (!IsOwner) return;
    ExhaustAxis();
    
   
    }
}
