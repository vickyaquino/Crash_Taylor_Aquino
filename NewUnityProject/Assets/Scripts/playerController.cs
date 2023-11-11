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

    public Transform SecretSpawnPos;

    //side to side movement speed
    public float speed = 10f;

    //jump force added when the player press SPACE on keyboard
    public float jumpForce = 1f;

    private Rigidbody rigidBody;

    public bool IsGrounded, jump;

    public Transform SpawnPos;

    public Material Gold, Red;

    public TextMeshProUGUI FruitsCollected, LivesRemaining;

    bool canAttack, isAttacking, SeenSecret;

    //

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        currentLevel = 0;
        totalLevels = spawnPoints.Length;
        canAttack = true;
        isAttacking = false;
        SeenSecret = false;
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
        if (Input.GetKey(KeyCode.E) && canAttack)
        {
            StartCoroutine(AttackMode());
        }
        if (fruitsCollected >= 100)
        {
            gainALife();
        }
        LivesRemaining.text = "Lives Remaining: " + Lives;
        FruitsCollected.text = "Fruits Collected: " + fruitsCollected;
        SpawnPos = spawnPoints[currentLevel].transform;
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
        if (Lives <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If you hit anything tagged enemy or spike (enemy to be changed to depend on attack state), including the endless pit.
        if (other.gameObject.tag == "Spike")
        {
            LoseALife();
            if (Lives > 0)
            {
                Respawn();
            }
        }
        if (other.gameObject.tag == "Enemy" && !isAttacking)
        {
            LoseALife();
            if (Lives > 0)
            {
                Respawn();
            }
        }
        else
        {
            if (other.gameObject.tag == "Enemy" && isAttacking)
            {
                other.gameObject.SetActive(false);
            }
        }
        // Portal, teleports player to next level
        if (other.gameObject.tag == "Portal")
        {
            if (currentLevel < totalLevels)
            {
                currentLevel++;
            }
            else if (currentLevel >= totalLevels)
            {
                currentLevel = totalLevels - 1;
            }
            SpawnPos.transform.position = spawnPoints[currentLevel].transform.position;
            Respawn();
        }
        // go back a level
        if (other.gameObject.tag == "Entrance")
        {
            if (currentLevel >= 0)
            {
                currentLevel--;
            }
            else if (currentLevel < 0)
            {
                currentLevel = 1;
            }
            SpawnPos.transform.position = spawnPoints[currentLevel].transform.position;
            Respawn();
        }
        //End portal
        if (other.gameObject.tag == "End")
        {
            SceneManager.LoadScene(3);
        }

        // Fruit Grabber
        if (other.gameObject.tag == "Fruit")
        {
            other.gameObject.SetActive(false);
            fruitsCollected++;
        }
        if (other.gameObject.tag == "Chest" && isAttacking)
        {
            other.GetComponent<chest>().BreakChest();
        }
        if (other.gameObject.tag == "Secret" && !SeenSecret)
        {
            SeenSecret = true;
            transform.position = SecretSpawnPos.position;
        }
        if (other.gameObject.tag == "SecretEnd")
        {
            SpawnPos.transform.position = spawnPoints[currentLevel-1].transform.position;
            Respawn();
            SpawnPos.transform.position = spawnPoints[currentLevel].transform.position;
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

    public void gainALife()
    {
        Lives++;
        fruitsCollected -= 100;
    }


    public IEnumerator AttackMode()
    {
        canAttack = false;
        isAttacking = true;
        GetComponent<MeshRenderer>().material = Red;
        yield return new WaitForSeconds(1f);
        isAttacking = false;
        GetComponent<MeshRenderer>().material = Gold;
        yield return new WaitForSeconds(.5f);
        canAttack = true;
    }
}

