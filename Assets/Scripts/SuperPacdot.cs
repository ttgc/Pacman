using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPacdot : MonoBehaviour
{
    void Update()
    {

    }

    public GameObject score;
    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "pacman")
        {
            Destroy(gameObject);
            Score scoreScript = score.GetComponent<Score>();
            scoreScript.score += 25;
            //co.gameObject.GetComponent<PacmanMove>().superPacman = true;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ghost"))
            {
                obj.GetComponent<GhostMove>().vulnerable = true;
                obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<GhostMove>().vulnerableSpr;
                obj.GetComponent<Animator>().enabled = false;
            }
        }
    }
}
