using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour
{
    void Update()
    {

    }

    public GameObject score;
    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "pacman")
        {
            Score scoreScript = score.GetComponent<Score>();
            scoreScript.score += 5;
            Destroy(gameObject);
        }
    }
}
