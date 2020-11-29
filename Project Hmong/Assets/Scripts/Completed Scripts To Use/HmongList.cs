using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow2 : MonoBehaviour
{
    [SerializeField] public Stack<Transform> followers = new Stack<Transform>();
    [SerializeField] public Transform[] followersList;
    public Transform Player;
    public float targetDistance;
    public static int followerCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        followers.Push(Player);

        followersList[followerCount] = Player;
    }

    //Check to see if tag matches player or
    private void OnTriggerEnter(Collider other)
    {
         if(other.gameObject.tag == "Player")
        {
            followerCount++;
            followers.Push(other.gameObject.transform);
            followersList[followerCount] = other.gameObject.transform;

        }
    }
    //If that's the case Follow the player or NPC with that index
    // Add new script for follwoing player 

    // Update is called once per frame
    void Update()
    {
 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            followers.Pop();

            int push = followers.Count;

            Destroy(followersList[push].gameObject);
            followersList[push] = null;

            followerCount--;

 
        }
    }
}
