using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] public KeyType keyType;

    public enum KeyType
    {
        Red,
        Green,
        Blue
    }

    public static bool redKey = false;
    public static bool greenKey = false;
    public static bool blueKey = false;

    public void OnCollisionEnter(Collision collision)
    {
        switch(keyType)
        {
            case KeyType.Red:
                Destroy(gameObject);
                redKey = true;
                break;
            case KeyType.Green:
                Destroy(gameObject);
                greenKey = true;
                break;
            case KeyType.Blue:
                Destroy(gameObject);
                blueKey = true;
                break;
        }
    }
}
