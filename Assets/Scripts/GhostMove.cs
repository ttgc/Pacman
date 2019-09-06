using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostMove : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 0.3f;
    int currentWaypoint = 0;
    Vector2 initialPosition;
    public bool vulnerable = false;
    Sprite spr;
    public Sprite vulnerableSpr;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        spr = gameObject.GetComponent<SpriteRenderer>().sprite;
        //gameObject.GetComponent<Animator>().
    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate is called at fixed time interval
    void FixedUpdate()
    {
        // Waypoint not reached yet? then move closer
        if (transform.position != waypoints[currentWaypoint].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position,
                                            waypoints[currentWaypoint].position,
                                            speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        // Waypoint reached, select next one
        else currentWaypoint = (currentWaypoint + 1) % waypoints.Length;

        // Animation
        Vector2 dir = waypoints[currentWaypoint].position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);

        // Disable vulnerability after 5s
        if (vulnerable)
        {
            timer += Time.fixedDeltaTime;
            if (timer > 5.0f)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = spr;
                GetComponent<Animator>().enabled = true;
                vulnerable = false;
                resetTimer();
            }
        }
    }

    public void resetTimer()
    {
        timer = 0;
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "pacman")
        {
            if (vulnerable)
            {
                transform.position = initialPosition;
                currentWaypoint = 0;
                gameObject.GetComponent<SpriteRenderer>().sprite = spr;
                GetComponent<Animator>().enabled = true;
                vulnerable = false;
                resetTimer();
            }
            else
            {
                //Destroy(co.gameObject);
                if (PacmanMove.health <= 0)
                {
                    SceneManager.LoadScene(2);
                }
                else
                {
                    PacmanMove.health--;
                    foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ghost"))
                    {
                        obj.transform.position = obj.GetComponent<GhostMove>().initialPosition;
                        obj.GetComponent<GhostMove>().currentWaypoint = 0;
                        obj.gameObject.GetComponent<SpriteRenderer>().sprite = spr;
                        obj.gameObject.GetComponent<Animator>().enabled = true;
                        obj.GetComponent<GhostMove>().vulnerable = false;
                        obj.GetComponent<GhostMove>().resetTimer();
                    }
                    co.gameObject.GetComponent<PacmanMove>().resetPosition();
                }
            }
        }
    }
}