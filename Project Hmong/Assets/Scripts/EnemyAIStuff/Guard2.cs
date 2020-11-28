using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard2 : MonoBehaviour
{

    Color original = Color.white;
    Transform player;
    Vector3 newPosition;
    public NavMeshAgent agent;

    //Patrolling
    public LayerMask viewMask, whatIsGround, whatIsPlayer;
    public Light spotLight;
    public float viewDistance;
    public float guardSpeed = 4.0f;
    public float guardPause = 2.0f;
    public float guardChaseSpeed = 5f;
    public float chaseWaitTime = 1f;
    public float turnSpeed = 90f;
    public float timeToSportPlayer = 0.5f;
    public float walkPointRange;
    public Vector3 walkPoint;

    //Attacking
    bool alreadyAttacked;
    float viewAngle;
    float playerVisibleTimer;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, PlayerInAttackRange;
    public bool walkPointSet;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {


        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!PlayerInAttackRange && !playerInSightRange) { Patrolling(); }
        if (PlayerInAttackRange && !playerInSightRange) { ChasePlayer(); }


    }

    void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX  = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround))
        {
            walkPointSet = true;
        }

    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void SuspectPlayer()
    {

    }

    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance) // Compares the distance of the transform position and palyer position to the view distance; 1st check
        {
            Debug.Log("Hey");
            Vector3 dirToPlayer = (player.position - transform.position);
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer); //returns smallest angle between player and guard
            if (angleBetweenGuardAndPlayer < viewAngle / 2f) //Checks to see if angle between guard and player is within the viewAngle; 2nd check
            {
                Debug.Log("True");
                //Checks if line of sight to player is blocked by hospital
                //We use a layermask variable
                if (!Physics.Linecast(transform.position, player.position, viewMask))//Checks to see if we havent hit anything
                {
                    return true;
                }
            }
        }
        return false;
    }

}
