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
        flatModule = (FlatModule)Instantiate(GlobalController.instance.modulePrefabs[0]);
        flatModule.transform.position = this.transform.position;
        flatModule.transform.SetParent(this.transform);
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
            leftDoor.Find("door").gameObject.SetActive(false);
        }
        else
        {
            rightDoor.Find("door").gameObject.SetActive(false);
        }
        GlobalController.instance.AddPointForPlayer(playerNumber);
    }
}
