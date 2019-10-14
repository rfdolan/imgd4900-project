using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clampy : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(rb.position.y <=0.0f)
        {
            rb.position = new Vector3(rb.position.x, 0.0f, rb.position.z);
        }
        
    }
}
