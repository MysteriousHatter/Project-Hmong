using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float smoothMoveTime = .1f;
    public float turnSpeed = 4f;

    float angle;
    float smoothInputMagnitude;
    float smoothMoveVelocity;
    Vector3 velocity;
    CinematicTrigger trigger;

    new Rigidbody rigidbody;
    [SerializeField] bool disabled;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Guard.OnGuardHasSpotedPlayer += Disable;
        trigger = FindObjectOfType<CinematicTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        MovinMovin();
    }

    private void MovinMovin()
    {
        Vector3 inputDirection = Vector3.zero;
        if (!disabled)
        {
            inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        }
        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);

        velocity = transform.forward * moveSpeed * smoothInputMagnitude;
    }

    private void Disable()
    {
        disabled = true;
    }

    public void FreezeForCutsceene()
    {
        StartCoroutine(CutsceeneCoroutine());
    }

    IEnumerator CutsceeneCoroutine()
    {
        disabled = true;

        yield return new WaitForSeconds(trigger.timeToAppear);

        disabled = false;
    }

    private void FixedUpdate()
    {
        rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);
    }


    private void OnDestroy() //Will happen when scene get destroyed
    {
        Guard.OnGuardHasSpotedPlayer -= Disable;
    }
}
