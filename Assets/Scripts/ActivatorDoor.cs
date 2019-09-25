using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorDoor : MonoBehaviour
{
    Animator anim;
    AudioSource audioData;
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // anim.enabled = false;
        isOpen = false;
       audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenDoor()
    {
        Debug.Log("second door opened");
        anim.SetTrigger("DoorOpen");
        if(!isOpen)
        {
            isOpen = true;
            audioData.Play(0);
        }

    }


    private void pauseAnimationEvent()
    {
        anim.enabled = false;
    }
}
