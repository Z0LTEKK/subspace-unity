using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Netcode;
using System.Data;
using Unity.Mathematics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
public class ShipController : NetworkBehaviour
{
  public float defaultshipspeed = 8.0f;
  public float shipspeed = 8.0f;
  public float afterburnerspeed = 20.0f;
  public int shiprotationspeed = 10;
  public AudioSource bounce;
  public Rigidbody2D rb;
  public GameObject ExplosionEffect;
  public GameObject Exhaust1;
  public GameObject Exhaust2;
  public bool AllowedToPlay = true;
  public bool Debounce = false;
  void Keys()
  {

    if (Input.GetKey(KeyCode.LeftShift))
    {
      shipspeed = afterburnerspeed;
    }
    else
    {
      shipspeed = defaultshipspeed;
    }
        if (Input.GetKeyDown(KeyCode.K))
    {
      if (Debounce == false)
      {
        Explode();
      }
    }
  }
  void Explode()
  {
    if (OwnerClientId != 0)
    {
      ExplosionServerRpc();
    }
    else
    
    {
      ExplodeClient();
    }
  }
  void ExplodeClient()
  {//yoinked most of this from the bullet explosion script
    Debounce = true;
    GameObject ExplosionClone = Instantiate(ExplosionEffect, transform.position, transform.rotation);
    ExplosionClone.GetComponent<NetworkObject>().Spawn(true);
    ParticleSystem ExplosionCloneParticle = ExplosionClone.GetComponent<ParticleSystem>();
    AudioSource ExplosionSound = ExplosionClone.GetComponent<AudioSource>();
    ExplosionCloneParticle.Emit(1);
    ExplosionSound.Play();
    rb.isKinematic = true;
    rb.velocity = Vector2.zero;
    gameObject.GetComponent<SpriteRenderer>().enabled = false;
    AllowedToPlay = false;
    Respawn();

  }
  void Respawn()
  {
    if (OwnerClientId != 0)
    {
      Invoke("RespawnPlayerServerRpc", 3);
    }

    else
    {
      Invoke("RespawnPlayerClient", 3);
    }
  }
  void RespawnPlayerClient()
  {
    gameObject.transform.position = new Vector3(0, 0, 0);
    gameObject.GetComponent<SpriteRenderer>().enabled = true;
    rb.isKinematic = false;
    AllowedToPlay = true;
    Debounce = false;
  }
  void Start()
  {

  }
  void Update()
  {
    if (!IsOwner) return;
    Keys();
    float translation = Input.GetAxisRaw("Vertical") * shipspeed;
    float shiprotation = Input.GetAxisRaw("Horizontal") * shiprotationspeed * Time.deltaTime;
    rb.AddRelativeForce(Vector3.up * translation);
    rb.transform.Rotate(0, 0, -shiprotation * shiprotationspeed);
    rb.velocity = Vector2.ClampMagnitude(rb.velocity, shipspeed);

    if (AllowedToPlay == false)
    {
      Exhaust1.GetComponent<ParticleSystem>().Play(false);
      Exhaust2.GetComponent<ParticleSystem>().Play(false);
    }

  }
  public void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "CollisionTag")
    {
      if (collision.relativeVelocity.magnitude > 3.5)
      {
        bounce.Play();
      }
      if (collision.gameObject.name == "BlueBullet(Clone)")
      {
        print("Hit");
      }
    }
  }

  private void FixedUpdate()
  {

  }

  [ServerRpc]
  private void ExplosionServerRpc()
  {
    ExplodeClient();
  }
  [ServerRpc]
  private void RespawnPlayerServerRpc()
  {
    RespawnPlayerClient();
  }
}