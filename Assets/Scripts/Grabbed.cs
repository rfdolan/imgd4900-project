using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbed : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 targetPoint;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
    }
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        this.GetComponent<Grabbed>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.position.y > targetPoint.y)
        {
            Vector3 vec = targetPoint - rb.position;
            vec = vec.normalized * speed;
            rb.velocity = vec;

        }
        else
        {
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
    void OnEnable()
    {
        rb.useGravity = false;
        Vector3 start = targetPoint;
        start.y = start.y + 0.5f; 
        rb.position = start;
        //Debug.Log("Enabled being held");

    }
    void OnDisable()
    {
        rb.useGravity = true;
        //Debug.Log("Disabled being held");
    }
}
