using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
       // anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("has collieded");
        //anim.enabled = true;
        if (other.CompareTag("Player"))
        {
            Debug.Log("collided with player");
            anim.SetTrigger("DoorOpen");
        }
            // Vector3 move = new Vector3(0.45f, 0.0f, 0.01f);
        //move = move.normalized; //magnitute of 1 
        //move *= 0.5f;

        //Rigidbody body = this.GetComponent<Rigidbody>(); 
        //body.velocity = move;
        
    }

    private void OnTriggerExit(Collider other)
    {
        //anim.enabled = true;
    }

    private void pauseAnimationEvent()
    {
        anim.enabled = false;
    }
}
