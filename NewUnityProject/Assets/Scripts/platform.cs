//Taylor, Madi
//10/26/2023
//Handles the movement of the platforms
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour

{
    //game objects to determine how far left and right the enemy goes
    public GameObject LeftPoint;
    public GameObject RightPoint;

    //boundary points for left and right
    private Vector3 LeftPos;
    private Vector3 RightPos;

    //how fast enemy travels
    public float speed;

    //the direction enemy travels 
    public bool GoingLeft;

    // Start is called before the first frame update
    void Start()
    {
        LeftPos = LeftPoint.transform.position;
        RightPos = RightPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlatformMove();
    }

    //makes the enemy move left and right
    private void PlatformMove()
    {
        if (GoingLeft)
        {
            //once enemy reaches left point left turns false
            if (transform.position.x <= LeftPos.x)
            {
                GoingLeft = false;
            }

            else
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
        }
        else
        {
            //once the enemy reaches RightPosition, GoingLeft is true
            if (transform.position.x >= RightPos.x)
            {
                GoingLeft = true;
            }
            else
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
    }
}

