using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{

    [Header("Player Variables")]
    public float playerSpeed = 5f;

    public InputModel playerOneModel, playerTwoModel;



    public static GlobalController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        Destroy(gameObject);
    }

    
    
}
