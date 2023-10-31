using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Author: [Aquino, Vicky]
 *  Date: [10/31/2023]
 *  [Make enemy move]
 */
public class Enemy : MonoBehaviour
{
    //control how fast the enemy moves
    public float speed;

    //control the direction the enemy is moving
    public bool movingLeft;

    //get the tranform positions of the left and right limits
    private Vector3 leftPosition, rightPosition;

    //get the objects that represent the left and right limits (walls)
    public GameObject leftLimit, rightLimit;

    private void Start()
    {
        leftPosition = leftLimit.transform.position;
        rightPosition = rightLimit.transform.position;
    }

    /// <summary>
    /// make the enemy move left and right
    /// </summary>

    // Update is called once per frame
    private void Update()
    {
        if (movingLeft)
        {
            //once the enemy reaches the left position- going left is false.
            if (transform.position.x <= leftPosition.x)
            {
                movingLeft = false;
            }
            else
            {
                //translate the enemy left by speed using Time.deltaTime
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
        }
        else
        {
            //once the enemy reaches the right position- going left is true
            if (transform.position.x >= rightPosition.x)
            {
                movingLeft = true;
            }
            else
            {
                //translate the enemy right by speed using Time.deltaTime
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
    }
}
