using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultCharacter : MonoBehaviour
{
    //Calling the RidgidBody
    private Rigidbody2D myRigidbody;

    //Speed for Adult
    public float speed = 1f;

    public bool isWalking;

    public float walkTime;

    public float waitTime; 


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
