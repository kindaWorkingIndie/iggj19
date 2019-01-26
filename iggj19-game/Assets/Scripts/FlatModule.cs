using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlatModule : MonoBehaviour
{
    public AudioClip startSound;
    public AudioClip[] loopSounds;
    public AudioClip endSound;

    Animator anim;

    AudioSource audioSource;

    Flat flat;
    Resident resident;



    public UnityEvent turnOnEvent;
    public UnityEvent turnOffEvent;
    Vector3 startPoint;
    Vector3 endPointRight,endPointLeft;
    float stepSize;

    public bool moduleClosed;
    bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        resident = GetComponentInChildren<Resident>();
        flat = GetComponentInParent<Flat>();
        audioSource = GetComponent<AudioSource>();

        anim = GetComponent<Animator>();

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
        if (anim != null) anim.SetBool("active", true);
        StartCoroutine(StartNoise());
    }
    public void turnOff()
    {
        isOn = false;
        turnOffEvent.Invoke();
        audioSource.loop = false;
        if (endSound != null)
        {
            audioSource.PlayOneShot(endSound);
        }
        else
        {
            audioSource.Stop();
        }

        if (anim != null) anim.SetBool("active", false);
    }

    IEnumerator StartNoise()
    {
        if (startSound != null)
        {
            audioSource.clip = startSound;
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            audioSource.Stop();
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        audioSource.loop = true;
        if (loopSounds.Length > 0)
        {
            audioSource.clip = loopSounds[Random.Range(0, loopSounds.Length)];
            audioSource.time = Random.Range(0, audioSource.clip.length / 2);
            audioSource.Play();
        }
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
            turnOff();
            GlobalController.instance.InstantStartNewFlat();
            return;
        }
        else if(residentPosition.x <= endPointLeft.x)
        {
            //LEFT
            flat.PointFor(PlayerController.PlayerNumber.LEFT);
            moduleClosed = true;
            turnOff();
            GlobalController.instance.InstantStartNewFlat();
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
        resident.SetTargetPos(residentPosition);


    }
}
