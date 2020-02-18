﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Striker : MonoBehaviour
{
    Rigidbody2D rb2d;

    Vector2 startPos;
    Vector2 dir;

    Vector3 worldToMousePos;

    public GameObject ArrowDirection;

    Transform arrowTransform;
    Transform selfTransform;

    public int strikeSpeed = 1000;

    bool isStruck = false;
    bool posIsSet = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Declare Rigidbody2D
        arrowTransform = ArrowDirection.transform; // Declare arrowTransformation
        selfTransform = transform;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStruck && !posIsSet)
        {
            selfTransform.position = new Vector2(Mathf.Clamp(worldToMousePos.x, -3, 3), startPos.y);
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(!posIsSet)
            {
                posIsSet = true;
            }
        }

        // ---- FIXED UPDATE ---- //

        worldToMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Finds the mouse position on the world

        dir = (Vector2)((worldToMousePos - transform.position)); // sets direction by calculating the difference between the location of mouse to position of striker
        dir.Normalize(); // vector keeps the same direction but its length is 1.0.

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (rb2d.velocity.magnitude < 0.2f && rb2d.velocity.magnitude != 0) // Set arrow when magnitude is less than 0.2 in value.
        {
            rb2d.velocity = Vector2.zero; // Sets the arrow
            isStruck = false;
            posIsSet = false;
        }
        else
        {
             // Disables the arrow
        }
    }

    // Shoot is called when an input is selected
    public void Shoot()
    {
        rb2d.AddForce(dir * strikeSpeed); // Direction * Speed of Striker = Movement; Just to see if moving the striker works.
        isStruck = true;
    }

}

// Notes: For talcum powder I can set the linear drag and increase it.