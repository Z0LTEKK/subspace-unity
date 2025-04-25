using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class StarsParallax : NetworkBehaviour
{
    public GameObject Player;
    [SerializeField] private GameObject stars;
    public Transform PlayerTransform;
    public float ParallaxValue;
    public float ParallaxLayer;
    public void Start()
    {
        if (stars)
            if (!IsLocalPlayer)
                Destroy(stars.gameObject);
    }


    // Update is called once per frame
    public void Update()
    {
        if (stars)
            if (!IsLocalPlayer)
                Destroy(stars.gameObject);

        if (SceneManager.GetActiveScene().name == "MainMap")
        {
            //print("setting transform");
            transform.position = PlayerTransform.transform.position * ParallaxValue + new Vector3(0,0,ParallaxLayer);
            transform.rotation = Quaternion.Euler(0, 90, 270);
        }
    }
}