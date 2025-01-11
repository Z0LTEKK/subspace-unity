using UnityEngine;
using Unity.Netcode;

public class BulletScript : NetworkBehaviour
{
    public float BounceCount = 3.0f;
    private float CurrentBounces;
    public Rigidbody2D Bullet;
    public GameObject ExplosionEffect;

    public ParticleSystem ExplosionParticle;

    // Start is called before the first frame update
    private void Start()
    {
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CollisionTag")
        {
            print("Collision!");
            CurrentBounces += 1;
            print(CurrentBounces);
            if (BounceCount <= CurrentBounces)
            {
                if (OwnerClientId != 0)
                {
                    ExplosionServerRpc();
                }
                else
                {
                    GameObject ExplosionClone = Instantiate(ExplosionEffect, transform.position, transform.rotation);
                    ExplosionClone.GetComponent<NetworkObject>().Spawn(true);
                    ParticleSystem ExplosionCloneParticle = ExplosionClone.GetComponent<ParticleSystem>();
                    AudioSource ExplosionSound = ExplosionClone.GetComponent<AudioSource>();
                    ExplosionCloneParticle.Emit(1);
                    ExplosionSound.Play();
                    Destroy(gameObject);
                }
            }
        }
    }

    private void ExplodeClient()
    {
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