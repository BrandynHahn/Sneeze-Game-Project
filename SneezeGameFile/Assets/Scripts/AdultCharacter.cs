using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultCharacter : MonoBehaviour
{
    //Calling the RidgidBody
    private Rigidbody2D myRigidbody;

    //Speed for Adult
    public float speed = 1f;

    //Chance adult will change directions
    public float chanceDirections = 0.01f; 

    //Edge of map
    public float edge = 10f; 

    //Checks if the adult is walking
    public bool isWalking;

    //The amount of time the adult is walking
    public float walkTime;

    private float walkCounter;

    private float waitCounter; 

    //The amount of time the adult is not walking
    public float waitTime;

    private int WalkDirection;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = pos.x + speed * Time.deltaTime;
        pos.y = pos.y + speed * Time.deltaTime;
        transform.position = pos; 

        if (pos.x < -edge)
        {
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > edge)
        {
            speed = -Mathf.Abs(speed);
        }
        else if (pos.y < -edge)
        {
            speed = Mathf.Abs(speed);
        }
        else if (pos.y > edge)
        {
            speed = -Mathf.Abs(speed);
        }
        else if (Random.value < chanceDirections)
        {
            speed = -1 * speed; 
        }


        if (isWalking)
        {
            walkCounter -= Time.deltaTime;

            switch (WalkDirection) 
            {
                case 0:
                    myRigidbody.velocity = new Vector2(0, speed);
                    break;

                case 1:
                    myRigidbody.velocity = new Vector2(speed, 0);
                    break;

                case 2:
                    myRigidbody.velocity = new Vector2(0, -speed);
                    break;

                case 3:
                    myRigidbody.velocity = new Vector2(-speed, 0);
                    break; 
            
            }

            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }

        }
        else 
        {
            waitCounter -= Time.deltaTime;

            myRigidbody.velocity = Vector2.zero;

            if(waitCounter < 0) 
            {
                ChooseDirection();
            }
        }
    }

    public void ChooseDirection() 
    {
        WalkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    
    }
}
