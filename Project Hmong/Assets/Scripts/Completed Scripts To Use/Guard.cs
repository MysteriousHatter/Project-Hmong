using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public static event System.Action OnGuardHasSpotedPlayer; 

    public Transform pathHolder;
    public float guardSpeed = 4.0f;
    public float guardPause = 2.0f;
    public float guardChaseSpeed = 5f;
    public float chaseWaitTime = 1f;
    public float turnSpeed = 90f;
    public float timeToSportPlayer = 0.5f;
    [SerializeField] float waypointToTolerance = 1f;
    [SerializeField] ParticleSystem muzzleFlash;

    public LayerMask viewMask;
    public Light spotLight;
    public float viewDistance;


    float viewAngle;
    float playerVisibleTimer;


    Color original = Color.white;
    Transform player;
    Vector3 newPosition;

    private void Start()
    {


        //PatrolBehavior();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotLight.spotAngle;
        original = spotLight.color;
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }
        StartCoroutine(FollowPath(waypoints));


    }

    // Update is called once per frame
    void Update()
    {



        if (CanSeePlayer())
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


        if (playerVisibleTimer >= timeToSportPlayer)
        {
            muzzleFlash.Play();
            if (OnGuardHasSpotedPlayer != null)
            {

                //invoke event for shooting player
                
            }
        }
    }

    bool CanSeePlayer()
    {
        if(Vector3.Distance(transform.position, player.position) < viewDistance) // Compares the distance of the transform position and palyer position to the view distance; 1st check
        {
            Debug.Log("Hey");
            Vector3 dirToPlayer = (player.position - transform.position);
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer); //returns smallest angle between player and guard
            if(angleBetweenGuardAndPlayer < viewAngle /2f) //Checks to see if angle between guard and player is within the viewAngle; 2nd check
            {
                Debug.Log("True");
                //Checks if line of sight to player is blocked by hospital
                //We use a layermask variable
                if(!Physics.Linecast(transform.position, player.position, viewMask))//Checks to see if we havent hit anything
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator FollowPath(Vector3[] path)
    {
        transform.position = path[0];

        int targetPathIndex = 1;
        Vector3 targetWaypoints = path[targetPathIndex];
        transform.LookAt(targetWaypoints);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoints, guardSpeed * Time.deltaTime);
            if (transform.position == targetWaypoints)
            {
                targetPathIndex = (targetPathIndex + 1) % path.Length; //If both these values equal they will return to 0
                targetWaypoints = path[targetPathIndex];
               
                yield return new WaitForSeconds(guardPause);
                yield return StartCoroutine(TurnToFace(targetWaypoints));
            }
            yield return null; //Yield for one frame
        }


    }

    float CheckDistanceFromTarget()
    {
        float distance = Vector3.Distance(player.transform.position, this.transform.position);
        return distance;
    }



    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05) //Checks to see if Guard is seeing the look target
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        //Gizmos.DrawLine(previousPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }


}
