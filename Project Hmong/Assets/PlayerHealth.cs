using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [AddComponentMenu("Health")]
    public int playerHp = 3;

    // Start is called before the first frame update
    void Start()
    {
        // I doubt anything is going to be put here
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHp <= 0) { } //I dunno what to put here yet, I guess this'll get figured out
    }
}
