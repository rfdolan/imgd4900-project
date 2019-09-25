using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingHeld : MonoBehaviour
{
    public Transform hand;
    private Vector3 handPoint;
    private Rigidbody rb;
    public float speed;
    public Pickup parentScript;
    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody, hand point, and disable the script.
        this.GetComponent<BeingHeld>().enabled = false;
    }

    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Find the vector from where the object is to where the hand is.
        handPoint = hand.position;
        Vector3 currentPos = this.GetComponent<Transform>().position;
        Vector3 direction = handPoint - currentPos;

        // If they are too far away from the object, drop it.
        // TODO test this a little more.
        if(direction.magnitude > 2)
        {
            parentScript.dropObject();

        }

        // Calculate the speed based on how close we are to the hand.
        float scaledSpeed = direction.magnitude;
        scaledSpeed *= speed;

        // Move in the direction at the given speed.
        direction = direction.normalized;
        direction = direction * scaledSpeed;
        rb.velocity = direction;
        
    }
    void OnEnable()
    {
        rb.useGravity = false;
        //Debug.Log("Enabled being held");

    }
    void OnDisable()
    {
        rb.useGravity = true;
        //Debug.Log("Disabled being held");
    }

}
