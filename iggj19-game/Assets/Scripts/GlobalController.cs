using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalController : MonoBehaviour
{

    [Header("Player Variables")]
    public GameObject playerPrefab;
    public float playerSpeed = 5f;

    public InputModel playerOneModel, playerTwoModel;

    public int playerLeftPoints, playerRightPoints;

    bool setReady;
    bool playersReady;
    public bool playerLeftReady, playerRightReady;

    bool gameOver;

    [Header("Flats")]
    public List<Flat> flats;
    int currentlyOn;
    public List<FlatModule> modulePrefabs;

    [Header("Sounds")]
    public AudioClip[] knockSounds;


    public static GlobalController instance;
    HUDManager hud;
    [Header("Timers")]
    float startCountdown = 4;

    float minToNextFlat = 5;
    float maxToNextFlat = 10;
    float currentTimer;
    float countingTo;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            
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


        for(int i = 0; i < flats.Count; i++)
        {
            int moduleIndex = Random.Range(0, modulePrefabs.Count);

            FlatModule flatModule = (FlatModule)Instantiate(modulePrefabs[moduleIndex]);
            modulePrefabs.RemoveAt(moduleIndex);
            flatModule.transform.position = flats[i].transform.position;
            flatModule.transform.SetParent(flats[i].transform);
            flats[i].flatModule = flatModule;
        }
    }

    private void Update()
    {
        if (gameOver) return;
        if (!playersReady) {

            if (Input.GetKeyDown(playerOneModel.knock)) playerLeftReady = true;
            if (Input.GetKeyDown(playerTwoModel.knock)) playerRightReady = true;

            if(playerRightReady && playerLeftReady && !setReady)
            {
                hud.StartGame();
                setReady = true;
                StartCoroutine(SetReady());
            }
            
            
            return;
        }
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
            
            TurnOnNextFlat();
        }

        if(playerLeftPoints > flats.Count / 2)
        {
            GameOver();
            Debug.Log("Player left wins");
        }
        if(playerRightPoints > flats.Count / 2)
        {
            GameOver();
            Debug.Log("Player right wins");
        }

    }
    IEnumerator SetReady()
    {
        yield return new WaitForSeconds(1);
        playersReady = true;
    }
    void GameOver()
    {
        gameOver = true;
        foreach(Flat f in flats)
        {
            f.flatModule.turnOff();
        }
        StartCoroutine(SetGameOver());
        
    }
    IEnumerator SetGameOver()
    {
        hud.GameOver();
        yield return new WaitForSeconds(0.5f);
        PlayerController.PlayerNumber winner;
        winner = PlayerController.PlayerNumber.LEFT;
        if (playerLeftPoints > flats.Count / 2)
        {
            winner = PlayerController.PlayerNumber.LEFT;
        }
        if (playerRightPoints > flats.Count / 2)
        {
            winner = PlayerController.PlayerNumber.RIGHT;
        }
        hud.AnnounceWinner(winner);

        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("main");

    }

    public void InstantStartNewFlat()
    {
        currentTimer = 999;
    }

    void TurnOnNextFlat()
    {
        int previouslyOn = currentlyOn;
        flats[currentlyOn].flatModule.turnOff();
        if (flats.Count >= 2)
        {
            while (currentlyOn == previouslyOn)
            {
                int newFlatToTurnOn = Random.Range(0, flats.Count);
                if (!flats[newFlatToTurnOn].flatModule.moduleClosed)
                {
                    currentlyOn = newFlatToTurnOn;
                }

            }
        }
        


        flats[currentlyOn].flatModule.turnOn();

    }


    public void AddPointForPlayer(PlayerController.PlayerNumber playerNumber)
    {
        if(playerNumber == PlayerController.PlayerNumber.LEFT)
        {
            playerLeftPoints++;

        }
        else
        {
            playerRightPoints++;
        }
    }

    public bool isCountdownOver()
    {
        return startCountdown <= 0;
    }

    public bool isGameOver()
    {
        return gameOver;
    }
}
