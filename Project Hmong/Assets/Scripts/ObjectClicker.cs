using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            Shoot();

        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.transform != null)
            {
                PrintTable(hit.transform.gameObject);
            }

        }
    }

    void PrintTable(GameObject obj)
    {

        if(obj.gameObject.tag == "Enemy")
        {
            Debug.Log("Kill Enemy");

        }
        else
        {
            Debug.Log("Not Enemy");
        }
        
    }
}
