using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [AddComponentMenu("Health")]
    public int playerHp = 3;
    public int damage = 1;
    bool isDead;

    // Start is called before the first frame update

    public bool IsDead()
    {
        return isDead;
    }
    void OnParticleCollision(GameObject other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            playerHp = playerHp - damage;
            Debug.Log("I'm hit");
            if (playerHp <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        
    }
}
