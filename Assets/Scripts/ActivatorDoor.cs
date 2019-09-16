using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorDoor : MonoBehaviour
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

    public void OpenDoor()
    {
        Debug.Log("second door opened");
        anim.SetTrigger("DoorOpen");
    }


    private void pauseAnimationEvent()
    {
        anim.enabled = false;
    }
}
