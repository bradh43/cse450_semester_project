using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) {
        // Reload scene only when colliding with player
        // TODO change to GoatController
        if(other.gameObject.GetComponent<GoatMovement>()){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
