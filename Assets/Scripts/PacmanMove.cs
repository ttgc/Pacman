﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PacmanMove : MonoBehaviour
{
    public float speed = 0.25f;
    Vector2 dest = Vector2.zero;
    public static int health;
    public GameObject heart1, heart2, heart3;
    Vector2 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        dest = transform.position;
        initialPosition = transform.position;
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        switch (health)
        {
            case 3:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 0:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
        }
    }

    // FixedUpdate is called at fixed time interval
    void FixedUpdate()
    {
        // Moving towards destination
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        // Check for Input if not moving (destination reached)
        if ((Vector2)transform.position == dest)
        {
            if (Input.GetKey(KeyCode.UpArrow) && valid(Vector2.up))
                dest = (Vector2)transform.position + Vector2.up;
            if (Input.GetKey(KeyCode.RightArrow) && valid(Vector2.right))
                dest = (Vector2)transform.position + Vector2.right;
            if (Input.GetKey(KeyCode.DownArrow) && valid(-Vector2.up))
                dest = (Vector2)transform.position - Vector2.up;
            if (Input.GetKey(KeyCode.LeftArrow) && valid(-Vector2.right))
                dest = (Vector2)transform.position - Vector2.right;
        }

        // Updating Dir parameter for Animation
        Vector2 dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);

        // Check if level is finished
        if (GameObject.Find("pacdot") == null)
        {
            SceneManager.LoadScene(3);
        }
    }

    /*
     * Check if the desired movement is valid
     * Cast a line between pacman and target location, if there is a wall on the way (if the line encoutered a wall0, this function will return false, else (if the line did not encoutered any wall) this function will return true
     */
    bool valid(Vector2 dir)
    {
        Vector2 pos = transform.position; // Pacman position
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos); // Line between Pacman and target location
        return (hit.collider == GetComponent<Collider2D>() || hit.collider.name == "pacdot");
    }

    void OnTriggerEnter2D(Collider2D co)
    {
    }

    public void resetPosition()
    {
        transform.position = initialPosition;
        dest = transform.position;
    }
}
