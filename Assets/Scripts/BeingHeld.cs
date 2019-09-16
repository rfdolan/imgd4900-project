using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingHeld : MonoBehaviour
{
    public Transform hand;
    private Vector3 handPoint;
    private Rigidbody rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody, hand point, and disable the script.
        rb = this.GetComponent<Rigidbody>();
        this.GetComponent<BeingHeld>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Find the vector from where the object is to where the hand is.
        handPoint = hand.position;
        Vector3 currentPos = this.GetComponent<Transform>().position;
        Vector3 direction = handPoint - currentPos;

        // Calculate the speed based on how close we are to the hand.
        float scaledSpeed = direction.magnitude;
        scaledSpeed *= speed;

        // Move in the direction at the given speed.
        direction = direction.normalized;
        direction = direction * scaledSpeed;
        rb.velocity = direction;
        
    }

}
