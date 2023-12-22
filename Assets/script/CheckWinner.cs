using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinner : MonoBehaviour
{
    public static CheckWinner instance;

    public Camera defaultCamera;
    public Camera winnerCamera;
    public bool isWinner = false;

    public Transform target;
    public Transform playerRotation;
    public float smoothSpeed = 1.0f;

    private void Awake() 
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultCamera.enabled = true;
        winnerCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWinner) 
        {
            defaultCamera.enabled = false;
            winnerCamera.enabled = true;
        } 
    }

    private void lateUpdate() 
    {
        playerRotation.LookAt(new Vector3(playerRotation.position.x, playerRotation.position.y, winnerCamera.transform.position.z));

        if (target != null && isWinner) 
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, target.position.z + 2.2f);
            Vector3 smoothedPosition = Vector3.Lerp(winnerCamera.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            winnerCamera.transform.position = smoothedPosition;
        }
    }

    private void Ontriggerstay(Collider other) 
    {
        if (other.CompareTag("player") && Player_movement.instance.groundPlayer) 
        {
            isWinner = true;
        }
    }

}
