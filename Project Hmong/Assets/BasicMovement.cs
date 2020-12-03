using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{ 
    private float moveSpeed = 5f;

    float horizonatlInput;
    float verticalInput;

    bool disabled;

    void Update()
    {
        horizonatlInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        if (horizonatlInput != 0 || verticalInput != 0 || !disabled)
        {
            transform.position += moveSpeed * new Vector3(horizonatlInput, 0, verticalInput) * Time.deltaTime;
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Pew Pew");
        Debug.Log("oof ouch, my bones");
    }

    private void OnDestroy() //Will happen when scene get destroyed
    {
        Guard.OnGuardHasSpotedPlayer -= Disable;
    }

    private void Disable()
    {
        disabled = true;
    }
}
