using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontroller : MonoBehaviour
{
    [SerializeField] private float mouseSensitity = 3.0f;

    private float rotationX;
    private float rotationY;

    [SerializeField] private Transform target;
    [SerializeField] private float distanceFromTarget = 3.0f;

    private Vector3 currentRotaion;
    private Vector3 smoothVelocity = Vector3.zero;

    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private Vector2 rotationXMinMax = new Vector2 (-20, 40);

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitity;

        rotationY += mouseX;
        rotationX += mouseY;

        rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);
        Vector3 nextRotation = new Vector3(rotationX, rotationY);

        currentRotaion = Vector3.SmoothDamp(currentRotaion, nextRotation, ref smoothVelocity, smoothTime);

        transform.localEulerAngles = currentRotaion;
        transform.position = target.position - transform.forward * distanceFromTarget;
    }
}
