using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerNumber { ONE,TWO}
    public PlayerNumber playerNumber;
    public GameObject model;

    float moveSpeed;

    InputModel myInputModel;
    public PlayerWaypoints playerWaypoints;

    Transform targetPosition;
    
    void Start()
    {
        if(playerNumber == PlayerNumber.ONE)
        {
            myInputModel = GlobalController.instance.playerOneModel;
        }else if(playerNumber == PlayerNumber.TWO)
        {
            myInputModel = GlobalController.instance.playerTwoModel;
        }

        moveSpeed = GlobalController.instance.playerSpeed;
        targetPosition = playerWaypoints.waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
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
            transform.position = Vector3.Lerp(transform.position, targetPosition.position, moveSpeed);
        }

    }


    void GoUp()
    {
        targetPosition = playerWaypoints.goUp();
    }

    void GoDown()
    {
        targetPosition = playerWaypoints.goDown();
    }

    void Knock()
    {

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
