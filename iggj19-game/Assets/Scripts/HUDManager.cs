using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI startCountdown;

    public GameObject waitingScreen;
    public GameObject leftPanel, rightPanel;
    public TextMeshProUGUI leftReadyText, rightReadyText;
    private void Start()
    {
        waitingScreen.SetActive(true);
        startCountdown.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (GlobalController.instance.playerLeftReady) {
            leftReadyText.text = "READY";
        }
        if (GlobalController.instance.playerRightReady) {
            rightReadyText.text = "READY";
        }
    }


    public void StartGame()
    {
        waitingScreen.SetActive(false);
        startCountdown.gameObject.SetActive(true);
    }
    public void SetCountdown(float countdown)
    {
        if(countdown > 0)
        {
            startCountdown.text = countdown.ToString("N0");
        }else if(countdown == 0)
        {
            startCountdown.text = "KNOCK!";
        }
        else
        {
            startCountdown.gameObject.SetActive(false);
        }
    }

    public void GameOver()
    {

    }

    public void AnnounceWinner(PlayerController.PlayerNumber playerNumber)
    {
        if (playerNumber == PlayerController.PlayerNumber.LEFT)
        {
            Debug.Log("WINNER IS LEFT");
        }
        else
        {
            Debug.Log("WINNER IS RIGHT");
        }
    }
}
