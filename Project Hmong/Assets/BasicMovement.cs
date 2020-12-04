using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{ 
    private float moveSpeed = 7.5f;
    private float smoothMoveTime = .1f;
    private float rotationalSpeed = 10;

    float horizonatlInput;
    float verticalInput;

    float targetAngle;
    float playerAngle;
    float smoothMoveVelocity;
    float smoothInputMagnitude;

    void Update()
    {
        horizonatlInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizonatlInput, 0, verticalInput);
        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        playerAngle = Mathf.LerpAngle(playerAngle, targetAngle, Time.deltaTime * rotationalSpeed * inputMagnitude);
        transform.eulerAngles = Vector3.up * playerAngle;

        transform.Translate(transform.forward * moveSpeed * Time.deltaTime * smoothInputMagnitude, Space.World);
    }

    /*
    void FixedUpdate()
    {
        float targetAngle = Mathf.Atan2(horizonatlInput, verticalInput) * Mathf.Rad2Deg;
        playerAngle = Mathf.LerpAngle(playerAngle, targetAngle, Time.deltaTime * rotationalSpeed);
        transform.eulerAngles = Vector3.up * playerAngle;

        if (horizonatlInput != 0 || verticalInput != 0)
        {
            transform.position += moveSpeed * new Vector3(horizonatlInput, 0, verticalInput) * Time.deltaTime;
        }
    }
    */
    void OnMouseDown()
    {
        Debug.Log("Pew Pew");
    }
}
