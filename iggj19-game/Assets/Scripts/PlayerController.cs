using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerNumber { ONE,TWO}
    public PlayerNumber playerNumber;
    public GameObject model;

    float moveSpeed;

    public InputModel myInputModel;
    

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
    }

    // Update is called once per frame
    void Update()
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
