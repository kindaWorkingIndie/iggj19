using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerNumber { LEFT,RIGHT}
    public PlayerNumber playerNumber;
    public GameObject model;

    float moveSpeed;

    InputModel myInputModel;

    public List<Transform> waypoints;
    int currentWaypointIndex;


    Transform targetPosition;
    AudioSource knockSource;

    
    void Start()
    {
        for(int i = 0; i < GlobalController.instance.flats.Count;i++)
        {
            if(playerNumber == PlayerNumber.LEFT)
            {
                waypoints.Add(GlobalController.instance.flats[i].leftOutside);
            }
            else
            {
                waypoints.Add(GlobalController.instance.flats[i].rightOutside);
            }
        }
        knockSource = GetComponent<AudioSource>();

        if(playerNumber == PlayerNumber.LEFT)
        {
            myInputModel = GlobalController.instance.playerOneModel;
        }else if(playerNumber == PlayerNumber.RIGHT)
        {
            myInputModel = GlobalController.instance.playerTwoModel;
        }

        moveSpeed = GlobalController.instance.playerSpeed;
        targetPosition = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!GlobalController.instance.isCountdownOver()) return;

        if (Input.GetKeyDown(myInputModel.up))
        {
            GoUp();
        }
        if (Input.GetKeyDown(myInputModel.down))
        {
            GoDown();
        }
        if (Input.GetKeyDown(myInputModel.knock))
        {
            Knock();
        }

        if(transform.position != targetPosition.position)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
        }

    }


    void GoUp()
    {
        if (currentWaypointIndex != 0)
        {
            currentWaypointIndex--;
        }
        targetPosition = waypoints[currentWaypointIndex];
    }

    void GoDown()
    {
        if (currentWaypointIndex + 1 != waypoints.Count)
        {
            currentWaypointIndex++;
        }
        targetPosition = waypoints[currentWaypointIndex];
    }

    void Knock()
    {
        knockSource.clip = GlobalController.instance.knockSounds[Random.Range(0, GlobalController.instance.knockSounds.Length)];
        knockSource.Play();

        GlobalController.instance.flats[currentWaypointIndex].Knock(playerNumber);


    }


}

[System.Serializable]
public class InputModel
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode knock;


    public void SetUpKey(KeyCode key)
    {
        this.up = key;
    }
    public void SetDownKey(KeyCode key)
    {
        this.down = key;
    }
    public void SetKnockKey(KeyCode key)
    {
        this.knock = key;
    }
}
