using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class camerafollow : NetworkBehaviour
{
    public GameObject Player;
    [SerializeField] private Camera camera;
    public Transform PlayerTransform;

    public void Start()
    {
        if (camera)
            if (!IsLocalPlayer)
                Destroy(camera.gameObject);
    }


    // Update is called once per frame
    public void Update()
    {
        if (camera)
            if (!IsLocalPlayer)
                Destroy(camera.gameObject);

        if (SceneManager.GetActiveScene().name == "MainMap")
        {
            print("setting transform");
            transform.position = PlayerTransform.transform.position + new Vector3(0, 0, -5);
            transform.rotation = Quaternion.LookRotation(PlayerTransform.position - transform.position, Vector3.up);
        }
    }
}