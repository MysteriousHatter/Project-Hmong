using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float moveSpeed = 8;
    private Rigidbody myRigidBody;

    float horizonatlInput;
    float verticalInput;

    Vector3 moveInput;
    Vector3 moveVelocity;

    private Camera mainCamera;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        horizonatlInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(horizonatlInput, 0f, verticalInput);
        moveVelocity = moveInput * moveSpeed;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            //REMOVE BELOW LINE BREFORE SHIPPING
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    private void FixedUpdate()
    {
        myRigidBody.velocity = moveVelocity;
    }

    void OnMouseDown()
    {
        Debug.Log("Pew Pew");
    }
}
