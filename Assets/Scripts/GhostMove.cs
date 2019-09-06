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

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }
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
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "pacman")
        {
            if (vulnerable)
            {
                transform.position = initialPosition;
                currentWaypoint = 0;
                vulnerable = false;
            }
            else
            {
                Destroy(co.gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}