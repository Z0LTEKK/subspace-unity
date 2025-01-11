using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class Gun : NetworkBehaviour
{
    public Transform FirePoint;
    public GameObject Bullet;
    public Rigidbody2D Ship;
    public  AudioSource BulletSound;
    public float BulletSpeed = 5.0f;
    void ShootBullet()
{
         if (OwnerClientId != 0) {
 BulletServerRpc();
         }

    else
    {
    GameObject BulletClone = Instantiate(Bullet, FirePoint.position, new Quaternion(0,0,0,0));
   
    BulletClone.GetComponent<NetworkObject>().Spawn(true);
    BulletClone.GetComponent<Rigidbody2D>().velocity = (Ship.velocity + (new Vector2(transform.up.x, transform.up.y) * BulletSpeed));
    BulletSound.Play();
    }

}
   void ShootBulletClient() 
   {
        GameObject BulletClone = Instantiate(Bullet, FirePoint.position, new Quaternion(0,0,0,0));
   
    BulletClone.GetComponent<NetworkObject>().Spawn(true);
    BulletClone.GetComponent<Rigidbody2D>().velocity = (Ship.velocity + (new Vector2(transform.up.x, transform.up.y) * BulletSpeed));
    BulletSound.Play();

   }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
  private  void Update()
       
    {
         if (!IsOwner) return;
     if (Input.GetButtonDown("Fire1")) 
     {
        ShootBullet();

     }  
    }
    [ServerRpc]
private void BulletServerRpc()
{

    ShootBulletClient();
}
}
