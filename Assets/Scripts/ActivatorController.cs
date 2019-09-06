using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorController : MonoBehaviour
{
    private Rigidbody rb;
    float pushPower = 2.0f;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision hit)
    {
        Debug.Log("first line");

        Vector3 newVector = (transform.position - hit.transform.position).normalized;

        //Vector3 myVector = new Vector3(1.0f, 0.0f, 1.0f);
        rb.velocity = newVector * pushPower;
        Debug.Log("ldldkds");
    } 
}
