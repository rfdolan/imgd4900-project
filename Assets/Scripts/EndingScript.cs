using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    public DoorScript door1;
    public DoorScript door2;
    public DoorScript door3;
    private Transform transform;
    public Vector3 firstKeyPoint;
    public Vector3 secondKeyPoint;
    public GameObject eyes;
    public AudioSource errorNoise;
    public AudioSource scream;
    private IEnumerator headCoroutine;
    private IEnumerator doorCoroutine;
    private IEnumerator eyeCoroutine;
    private IEnumerator otherHeadCoroutine;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnEnable()
    {
        Debug.Log("Trigger the ending!");
        transform = this.GetComponent<Transform>();
        this.GetComponent<CharacterController>().enabled = false;
        this.GetComponent<PlayerController>().enabled = false;
        transform.GetChild(0).gameObject.GetComponent<CamMouseLook>().enabled = false;
        transform.GetChild(0).gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
        transform.position = firstKeyPoint;
        errorNoise.Play(0);
        transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
        headCoroutine = turnHead(0.05f);
        doorCoroutine = closeDoors(1.0f);
        eyeCoroutine = eyeSoundsAndTurn(1.0f);
        //otherHeadCoroutine = turnHeadAgain(0.05f);
        StartCoroutine(headCoroutine);
    }
    private IEnumerator turnHead(float seconds)
    {
        Debug.Log("Calling turnhead routine.");
        while(transform.rotation.y > -0.75f)
        {
            Debug.Log(transform.rotation.y);
            transform.Rotate(transform.rotation.x, transform.rotation.y - 5.0f, transform.rotation.z);
            yield return new WaitForSeconds(seconds);

        }
        StopCoroutine(headCoroutine);
        StartCoroutine(doorCoroutine);

    }
    private IEnumerator closeDoors(float seconds)
    {
        door1.CloseDoor();
        yield return new WaitForSeconds(seconds);
        door2.CloseDoor();
        yield return new WaitForSeconds(seconds);
        door3.CloseDoor();
        StopCoroutine(doorCoroutine);
        StartCoroutine(eyeCoroutine);
        
    }
    private IEnumerator eyeSoundsAndTurn(float seconds)
    {
        eyes.SetActive(true);
        Debug.Log("Calling eye routine");
        scream.Play(0);
        
        //StartCoroutine(otherHeadCoroutine);
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Credits");
    }
    /*
    private IEnumerator turnHeadAgain(float seconds)
    {
        Debug.Log("Calling turnhead routine.");
        while(transform.rotation.y < -0.5f)
        {
            Debug.Log(transform.rotation.y);
            transform.Rotate(transform.rotation.x, transform.rotation.y - 5.0f, transform.rotation.z);
            yield return new WaitForSeconds(seconds);

        }
        StopCoroutine(otherHeadCoroutine);

    }
    */

    // Update is called once per frame
    void Update()
    {
        
    }
}
