﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel3 : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(2);
    }
}