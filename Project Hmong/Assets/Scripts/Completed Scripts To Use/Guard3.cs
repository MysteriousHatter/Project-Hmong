using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Guard3 : MonoBehaviour
{
    public Light spotLight;
    Color original = Color.white;

    /// <summary>
    /// Variables for Chasing Player
    /// </summary>
    float playerVisibleTimer;
    public float timeToSportPlayer = 0.5f;

    /// <summary>
    ///  This is for Patrolling Behavior
    /// </summary>
    //Dictates wheather the agent waits on each node
    [SerializeField] bool _patrolWaiting;

    //The total time we wait at each node
    [SerializeField] float totatlWaitTime = 3f;

    //The prob of switching direction
    [SerializeField] float switchProb = 0.2f;

    //The list of patrol nodes to visit
    [SerializeField] List<Transform> patrolPoints;

    //private varibales for base behavior
    NavMeshAgent meshAgent;
    int currentPatrolIndex;
    bool _traveling;
    bool _waiting;
    bool _patrolForward;
    float _waitTimer;
    float viewAngle;

    /// <summary>
    /// This is the chasing Behavior
    /// </summary>
    GameObject player;
    public float targetRange;
    float distanceToTarget;
    public LayerMask viewMask;
    private void Start()
    {
        original = spotLight.color;
        viewAngle = spotLight.spotAngle;
        player = GameObject.FindWithTag("Player");
        meshAgent = this.GetComponent<NavMeshAgent>();

    }



    private void Update()
    {
        //Check if we're close to the desintation
        if(_traveling && meshAgent.remainingDistance <= 1.0f)
        {
            _traveling = false;


            //If we're going to wait, then wait
            if(_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;
            }
            else
            {
                Patrolling();
                //ChangePoints();
            }
        }
        //Chceks to see if were 
        if(_waiting)
        {
            _waitTimer += Time.deltaTime;
            if(_waitTimer >= totatlWaitTime)
            {
                _waiting = false;

               // ChangePoints();
                Patrolling();
            }
        }


        //Checks distance between Player and enemy
        distanceToTarget = Vector3.Distance(transform.position, player.transform.position);

        //Checks to see if player is in enemy spot zone
        if (Chasing())
        {

            spotLight.color = Color.red;
            playerVisibleTimer += Time.deltaTime;

        }
        else
        {
            //agent.Stop();
            spotLight.color = original;
            playerVisibleTimer -= Time.deltaTime;
        }
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSportPlayer);
        spotLight.color = Color.Lerp(original, Color.red, playerVisibleTimer / timeToSportPlayer); //If visiableTimer == 0 will be original color, if visiableTimer == sportPlayer will be red

        //If player is visble to enemy
        if (playerVisibleTimer >= timeToSportPlayer)
        {

                Debug.Log("Shoot"); //invoke event for shooting player
                meshAgent.SetDestination(player.transform.position);
        }

    }

    /// <summary>
    /// Selects a new patrol point in the aviliable list, but
    /// also with a small probability allows for us to move forward or backwords
    /// </summary>

    private void Patrolling()
    {
        if(patrolPoints != null)
        {
            Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
            meshAgent.SetDestination(targetVector);
            _traveling = true;
        }
        if (UnityEngine.Random.Range(0f, 1f) <= switchProb)
        {
            _patrolForward = !_patrolForward;
        }

        if (_patrolForward)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        else
        {
            if (--currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolPoints.Count - 1;
            }
        }
    }


    private bool Chasing()
    {

        if (distanceToTarget <= targetRange) // Compares the distance of the transform position and palyer position to the view distance; 1st check
        {
            Debug.Log("Hey");
            Vector3 dirToPlayer = (player.transform.position - transform.position);
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer); //returns smallest angle between player and guard
            if (angleBetweenGuardAndPlayer < viewAngle / 2f) //Checks to see if angle between guard and player is within the viewAngle; 2nd check
            {
                Debug.Log("True");
                //Checks if line of sight to player is blocked by hospital
                //We use a layermask variable
                if (!Physics.Linecast(transform.position, player.transform.position, viewMask))//Checks to see if we havent hit anything
                {
                    return true;
                }
            }
        }

        return false;
       
    }

    //void Start()
    //{
    //    agent = GetComponent<NavMeshAgent>();

    //    // Disabling auto-braking allows for continuous movement
    //    // between points (ie, the agent doesn't slow down as it
    //    // approaches a destination point).
    //    agent.autoBraking = false;

    //    GotoNextPoint();
    //}


    //void GotoNextPoint()
    //{
    //    // Returns if no points have been set up
    //    if (points.Length == 0)
    //        return;

    //    // Set the agent to go to the currently selected destination.
    //    agent.destination = points[destPoint].position;

    //    // Choose the next point in the array as the destination,
    //    // cycling to the start if necessary.
    //    destPoint = (destPoint + 1) % points.Length;
    //}


    //void Update()
    //{
    //    // Choose the next destination point when the agent gets
    //    // close to the current one.
    //    if (!agent.pathPending && agent.remainingDistance < 0.5f)
    //        GotoNextPoint();
    //}
}

