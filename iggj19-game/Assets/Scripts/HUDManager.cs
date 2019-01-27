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
    public Image leftInputDisplay, rightInputDisplay;
    public Sprite WSD, ArrowKeys, Xbox;
    public TextMeshProUGUI leftReadyText, rightReadyText;

    public GameObject winnerScreen;
    public TextMeshProUGUI winnerTextLeft, winnerTextRight;

    bool saidKnockKnock;
    public AudioClip arnieKnockKnock;

    public TextMeshProUGUI leftGoodKnocksText, rightGoodKnocksText;
    public int leftGoodKnocks, rightGoodKnocks;
    private void Start()
    {
        winnerScreen.gameObject.SetActive(false);

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

        if (waitingScreen.activeSelf)
        {
            string[] joysticksConnected = Input.GetJoystickNames();
            if (joysticksConnected.Length > 0)
            {
                for (int i = 0; i < joysticksConnected.Length; i++)
                {

                    if (joysticksConnected[i].ToUpper().Contains("XBOX"))
                    {
                        if (i == 0)
                        {
                            leftInputDisplay.sprite = Xbox;
                        }
                        if (i == 1)
                        {
                            rightInputDisplay.sprite = Xbox;
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            leftInputDisplay.sprite = WSD;
                        }
                        if (i == 1)
                        {
                            rightInputDisplay.sprite = ArrowKeys;
                        }
                    }
                }
            }
        }
    }

    public void GoodKnock(PlayerController.PlayerNumber player)
    {
        if(player == PlayerController.PlayerNumber.LEFT)
        {
            leftGoodKnocks++;
            leftGoodKnocksText.text = "Good knocks\n" + leftGoodKnocks.ToString();
        }
        else
        {
            rightGoodKnocks++;
            rightGoodKnocksText.text = "Good knocks\n" + rightGoodKnocks.ToString();
        }
    }
    public void StartGame()
    {

        StartCoroutine(SetStartGame());
    }
    IEnumerator SetStartGame()
    {
        yield return new WaitForSeconds(1);
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
            if (!saidKnockKnock)
            {
                AudioSource audiosource = GetComponent<AudioSource>();
                audiosource.clip = arnieKnockKnock;
                audiosource.loop = false;
                audiosource.Play();

                saidKnockKnock = true;
            }
            startCountdown.text = "KNOCK KNOCK!";

        }
        else
        {
            startCountdown.gameObject.SetActive(false);
        }
    }

    public void GameOver()
    {
        winnerTextLeft.gameObject.SetActive(false);
        winnerTextRight.gameObject.SetActive(false);
        winnerScreen.SetActive(true);
    }

    public void AnnounceWinner(PlayerController.PlayerNumber playerNumber)
    {
        if (playerNumber == PlayerController.PlayerNumber.LEFT)
        {
            Debug.Log("WINNER IS LEFT");
            winnerTextLeft.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("WINNER IS RIGHT");
            winnerTextRight.gameObject.SetActive(true);
        }
    }
}
