using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [AddComponentMenu("Health")]
    public int enemyHP = 3;
    public int damage = 1;


    // Start is called before the first frame update


    void OnParticleCollision(GameObject other)
    {

        if (other.gameObject.tag == "Player")
        {
            enemyHP = enemyHP - damage;
            Debug.Log("I'm hit");
            if (enemyHP <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
        
    }
}
