using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlatModule : MonoBehaviour
{
    public AudioClip startSound;
    public AudioClip loopSound;
    public AudioClip endSound;


    AudioSource audioSource;

    Flat flat;
    Resident resident;



    public UnityEvent turnOnEvent;
    public UnityEvent turnOffEvent;
    Vector3 startPoint;
    Vector3 endPointRight,endPointLeft;
    float stepSize;

    bool moduleClosed;
    bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        resident = GetComponentInChildren<Resident>();
        flat = GetComponentInParent<Flat>();
        audioSource = GetComponent<AudioSource>();


        startPoint = flat.transform.position;
        endPointRight = flat.rightInside.position;
        endPointLeft = flat.leftInside.position;
        float distance = endPointRight.x - startPoint.x;
        stepSize = distance / 20;
        moduleClosed = false;
        turnOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOn()
    {
        isOn = true;
        turnOnEvent.Invoke();
        StartCoroutine(StartNoise());
    }
    public void turnOff()
    {
        isOn = false;
        turnOffEvent.Invoke();
        audioSource.loop = false;
        audioSource.PlayOneShot(endSound);
    }

    IEnumerator StartNoise()
    {
        audioSource.clip = startSound;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.clip = loopSound;
        audioSource.Play();
    }

    public void Knock(PlayerController.PlayerNumber playerNumber)
    {
        if (moduleClosed || !isOn) return;
        Vector3 residentPosition = resident.transform.position;
        
        if(residentPosition.x >= endPointRight.x)
        {
            //RIGHT
            flat.PointFor(PlayerController.PlayerNumber.RIGHT);
            moduleClosed = true;
            Debug.Log("Right Won");
            return;
        }
        else if(residentPosition.x <= endPointLeft.x)
        {
            //LEFT
            flat.PointFor(PlayerController.PlayerNumber.LEFT);
            moduleClosed = true;
            Debug.Log("Left Won");
            return;
        }
        
        if (playerNumber == PlayerController.PlayerNumber.LEFT)
        {
            residentPosition.x -= stepSize;
            
        }
        else
        {
            residentPosition.x += stepSize;
        }
        resident.transform.position = residentPosition;


    }
}
