using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CinematicTrigger : MonoBehaviour
{

    bool alreadyTriggered = false;
    private float timeWhenDisappear;
    [SerializeField] DialougeSystem StartingSpeech;
    [SerializeField] Text mainTextComp;
    public float timeToAppear = 2f;
    Player player;


    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    DialougeSystem system;
    private void OnTriggerEnter(Collider other)
    {
        if(!alreadyTriggered && other.gameObject.tag == "Player")
        {
            player.FreezeForCutsceene();
            alreadyTriggered = true;
            GetComponent<PlayableDirector>().Play();
            system = StartingSpeech;
            mainTextComp.text = system.GetStateStory();
            mainTextComp.gameObject.SetActive(true);
            mainTextComp.enabled = true;
            timeWhenDisappear = Time.time + timeToAppear;

        }
        
    }

    private void Update()
    {

        if (mainTextComp.enabled && (Time.time >= timeWhenDisappear))
        {
            mainTextComp.enabled = false;
            mainTextComp.gameObject.SetActive(false);
        }

    }
}
