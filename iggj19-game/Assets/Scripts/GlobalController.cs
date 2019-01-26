using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{

    [Header("Player Variables")]
    public GameObject playerPrefab;
    public float playerSpeed = 5f;

    public InputModel playerOneModel, playerTwoModel;

    [Header("Flats")]
    public List<Flat> flats;
    int currentlyOn;
    public List<FlatModule> modulePrefabs;

    [Header("Sounds")]
    public AudioClip[] knockSounds;


    public static GlobalController instance;
    HUDManager hud;
    [Header("Timers")]
    float startCountdown = 3;

    float minToNextFlat = 4;
    float maxToNextFlat = 8;
    float currentTimer;
    float countingTo;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            
        }
        else
        {
            Destroy(gameObject);
            return;
        }

       

    }
    private void Start()
    {
        hud = FindObjectOfType<HUDManager>();

        GameObject pLeft = Instantiate(playerPrefab);
        pLeft.GetComponent<PlayerController>().playerNumber = PlayerController.PlayerNumber.LEFT;
        pLeft.transform.position = flats[0].leftOutside.position;
        GameObject pRight = Instantiate(playerPrefab);
        pRight.GetComponent<PlayerController>().playerNumber = PlayerController.PlayerNumber.RIGHT;
        pRight.transform.position = flats[0].rightOutside.position;

    }

    private void Update()
    {
        if(startCountdown >= 0)
        {
            startCountdown -= Time.deltaTime;
            hud.SetCountdown(Mathf.Floor(startCountdown));
            return;
        }
        currentTimer += Time.deltaTime;
        if(countingTo <= currentTimer)
        {
            currentTimer = 0;
            countingTo = Random.Range(minToNextFlat, maxToNextFlat);
            //TODO: Turn on a flat
            flats[currentlyOn].flatModule.turnOff();

            currentlyOn = Random.Range(0, flats.Count);
            flats[currentlyOn].flatModule.turnOn();
        }

    }


    public bool isCountdownOver()
    {
        return startCountdown <= 0;
    }
}
