using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flat : MonoBehaviour
{
    public Transform leftDoor;
    public Transform leftInside;
    public Transform leftOutside;

    public Transform rightDoor;
    public Transform rightInside;
    public Transform rightOutside;


    public FlatModule flatModule;
    public int knockCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Knock(PlayerController.PlayerNumber playerNumber)
    {
        knockCounter++;
        flatModule.Knock(playerNumber);
    }

    public void PointFor(PlayerController.PlayerNumber playerNumber)
    {
        if(playerNumber == PlayerController.PlayerNumber.LEFT)
        {
            leftDoor.Find("door").transform.Rotate(Vector3.up * 90);
        }
        else
        {
            rightDoor.Find("door").transform.Rotate(Vector3.up * -90);
        }
        GlobalController.instance.AddPointForPlayer(playerNumber);
    }
}
