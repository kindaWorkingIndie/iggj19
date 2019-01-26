using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI startCountdown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCountdown(float countdown)
    {
        if(countdown > 0)
        {
            startCountdown.text = countdown.ToString("N0");
        }else if(countdown == 0)
        {
            startCountdown.text = "GO!";
        }
        else
        {
            startCountdown.gameObject.SetActive(false);
        }
    }
}
