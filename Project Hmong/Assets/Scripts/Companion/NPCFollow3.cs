using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow3 : MonoBehaviour
{
    public Transform thisRabbit;
    public Transform player;
    bool IsActive = false;
    float speed = 1.0f;
    const float minDistance = 2f;
    Vector3 DistancefromTarget;
    Animator m_Animator;
    public Transform target;
    [SerializeField] public  Transform[] followers;
    public static int followerCount = 0;
    void Start()
    {
        followers[0] = player;
        m_Animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            IsActive = true;
            target = followers[followerCount];
            followerCount++;
            followers[followerCount] = thisRabbit;
        }
    }

    void FixedUpdate()
    {
        if (IsActive)
        {
            Follow();
        }

        DistancefromTarget = transform.position - player.position;
    }

    void Follow()
    {
        transform.LookAt(player);
        if (DistancefromTarget.magnitude > minDistance)
        {
            transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        }
    }
}
