using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultCharacter : MonoBehaviour
{
    //Calling the RidgidBody
    private Rigidbody2D myRigidbody;

    //Speed for Adult
    public float moveSpeed;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseDirection() 
    {
        WalkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    
    }
}
