using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Netcode;
public class ShipController : MonoBehaviour
{
   public float defaultshipspeed = 8.0f;
 public float shipspeed = 8.0f;
  public float afterburnerspeed = 20.0f;
 public int shiprotationspeed = 10;
 public AudioSource bounce;
public Rigidbody2D rb;
public GameObject ExplosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
       void Afterburners()
      {
      
       if  (Input.GetKey(KeyCode.LeftShift))
      {
        shipspeed = afterburnerspeed;
      }
      else
      {
       shipspeed = defaultshipspeed; 
      }
    }
    void Update()
    {
    Afterburners();
   float translation = Input.GetAxisRaw("Vertical") * shipspeed;
    float shiprotation = Input.GetAxisRaw("Horizontal") * shiprotationspeed * Time.deltaTime;
   rb.AddRelativeForce(Vector3.up * translation);
     rb.transform.Rotate(0,0,-shiprotation * shiprotationspeed);
    rb.velocity = Vector2.ClampMagnitude(rb.velocity, shipspeed);

    }
public void OnCollisionEnter2D(Collision2D collision)
{
if(collision.gameObject.tag == "CollisionTag") 
{
if(collision.relativeVelocity.magnitude > 3.5){
bounce.Play();
}



}
}

private void FixedUpdate() 
{
    
 }
     void ExplodeClient()
    {//yoinked most of this from the bullet explosion script
        GameObject ExplosionClone = Instantiate(ExplosionEffect, transform.position, transform.rotation);
        ExplosionClone.GetComponent<NetworkObject>().Spawn(true);
        ParticleSystem ExplosionCloneParticle = ExplosionClone.GetComponent<ParticleSystem>();
        AudioSource ExplosionSound = ExplosionClone.GetComponent<AudioSource>();
        ExplosionCloneParticle.Emit(1);
        ExplosionSound.Play();
        Destroy(gameObject);
    }
    [ServerRpc]
    private void ExplosionServerRpc()
    {
        ExplodeClient();
    }
}