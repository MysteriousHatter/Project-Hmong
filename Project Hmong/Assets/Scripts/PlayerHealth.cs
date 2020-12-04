using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [AddComponentMenu("Health")]
    public int playerHp = 3;
    public int damage = 1;
    bool isDead;
    LoadScene scene;

    // Start is called before the first frame update

    void OnParticleCollision(GameObject other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            playerHp = playerHp - damage;
            Debug.Log("I'm hit");
            if (playerHp <= 0)
            {
                Invoke("Restart", 1f) ;
            }
        }
    }

    public void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Debug.Log(currentSceneIndex);
    }
}


