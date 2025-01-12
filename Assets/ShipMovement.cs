using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float defaultshipspeed = 8.0f;
    public float shipspeed = 8.0f;
    public float afterburnerspeed = 20.0f;
    public int shiprotationspeed = 10;
    public AudioSource bounce;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Afterburners()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            shipspeed = afterburnerspeed;
        else
            shipspeed = defaultshipspeed;
    }

    private void Update()
    {
        Afterburners();
        float translation = Input.GetAxisRaw("Vertical") * shipspeed;
        float shiprotation = Input.GetAxisRaw("Horizontal") * shiprotationspeed * Time.deltaTime;
        rb.AddRelativeForce(Vector3.up * translation);
        rb.transform.Rotate(0, 0, -shiprotation * shiprotationspeed);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, shipspeed);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CollisionTag")
            if (collision.relativeVelocity.magnitude > 3.5)
                bounce.Play();
    }

    private void FixedUpdate()
    {
    }
}