using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour
{
    public GameObject thePlayer;
    public float targetDistance; //distance from NPC to the player
    public float AllowedDistance = 5;
    public GameObject theNPC;
    public float FollowSpeed;
    public RaycastHit Shot;
    public float stopDistance = 5f;
    float distanceToTarget = Mathf.Infinity;
    [SerializeField] bool isActive = false;


    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isActive = true;
            this.gameObject.tag = "Player";
        }
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            transform.LookAt(thePlayer.transform);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot))
            {
                targetDistance = Shot.distance;
                if (targetDistance >= AllowedDistance)
                {
                    FollowSpeed = 0.02f;
                    transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, FollowSpeed);
                }
                else
                {
                    FollowSpeed = 0;

                }
            }
        }
    }
}
