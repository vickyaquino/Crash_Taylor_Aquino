using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    public int currentLevel, totalLevels;
    public GameObject[] spawnPoints = new GameObject[4];

    //side to side movement speed
    public float speed = 10f;

    //jump force added when the player press SPACE on keyboard
    public float jumpForce = 1f;

    private Rigidbody rigidBody;

    public bool IsGrounded, jump;

    public GameObject SpawnPoint;

    public Transform SpawnPos;

    public Material Gold, Red;

    public TextMeshProUGUI FruitsCollected, LivesRemaining;

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
        LivesRemaining.text = "Lives Remaining: " + Lives;
        FruitsCollected.text = "Fruits Collected: " + fruitsCollected;
    }

    /// <summary>
    /// Vicky Come back to this is giving error..
    /// </summary>
    /// <param name="other"></param>
    //set the fruits ui to fruitsCollected
    //FruitCounter.text = "fruits Collected: " + fruitsCollected;
    //private void OnTriggerEnter(Collider other)
    //{
        //if we collide w a fruit trigger, delete it and add to score
        //if (other.gameObject.tag == "fruit")
        //{
            //fruitsCollected++;
            //other.gameObject.SetActive(false);
        //}
    //}

        private void FixedUpdate()
    {
        if (jump)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            jump = false;
        }
        SpawnPos = SpawnPoint.transform;
        if (Lives <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If you hit anything tagged enemy or spike (enemy to be changed to depend on attack state), including the endless pit.
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Spike")
        {
            LoseALife();
            if (Lives > 0)
            {
                Respawn();
            }
        }
        // Portal, teleports player to next level
        if (other.gameObject.tag == "Portal")
        {

        }
        // Fruit Grabber
        if (other.gameObject.tag == "Fruit")
        {
            other.gameObject.SetActive(false);
            fruitsCollected++;
        }
    }


    public void LoseALife()
    {
        if (Lives >= 0)
        {
            Lives--;
        }
    }

    public void Respawn()
    {
        transform.position = SpawnPos.transform.position;
    }
    
    //teleport the player to the spawn point to the level that they should be in
    //public void spawn()
    //{
       // transform.position = spawnPoints[currentLevel];
   // }


}

