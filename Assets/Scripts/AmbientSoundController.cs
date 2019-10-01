using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundController : MonoBehaviour
{
    public AudioSource sound1;
    public AudioSource sound2;
    public AudioSource sound3;
    private IEnumerator audioCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        audioCoroutine = TryToPlayAudio(15.0f);
        StartCoroutine(audioCoroutine);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TryToPlayAudio(float seconds)
    {
        while(true)
        {
            yield return new WaitForSeconds(seconds);
            float randomNum = Random.Range(0, 5);
            switch(randomNum)
            {
                case 0:
                    Debug.Log("Playing sound 1");
                    sound1.Play(0);
                    break;
                case 1:
                    Debug.Log("Playing sound 2");
                    sound2.Play(0);
                    break;
                case 2:
                    Debug.Log("Playing sound 3");
                    sound3.Play(0);
                    break;
                default:
                    Debug.Log("Playing no sound");
                    break;
            }
        }
    }
}
