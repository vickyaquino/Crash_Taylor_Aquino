using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 *  Author: [Aquino, Vicky and Maddi Taylor]
 *  Date: [10/26/2023]
 *  [movement of player, collection of fruits    ]
 */

public class PlayerController : MonoBehaviour
{
    //the total amt of fruitss the player has collected
    public int fruitsCollected = 0;

    //amount of lives main player has in the beginning of the game.
    public int Lives = 3;

    //side to side movement speed
    public float speed = 10f;

    //jump force added when the player press SPACE on keyboard
    public float jumpForce = 1f;

    private Rigidbody rigidBody;

    public bool IsGrounded, jump;

    //

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //left and right player movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //Debug.Log("Move the player left");
            //translate the player by speed using Time.deltaTime
            transform.position += -transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        //Debugs out and shows what the raycast looks like in the editor
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.5f, Color.red);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += -transform.forward * speed * Time.deltaTime;
        }

        if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }

        if (Input.GetKey(KeyCode.Space) && IsGrounded)
        {
            jump = true;
        }
    }


    private void FixedUpdate()
    {
        if (jump)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            jump = false;
        }
    }



}

