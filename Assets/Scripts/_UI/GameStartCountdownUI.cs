using System;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countdownText.gameObject.SetActive(false);
        GameManager.instance.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(object sender, EventArgs e)
    {
        if(GameManager.instance.isCountdown()){
            countdownText.gameObject.SetActive(true);
        }else{
            countdownText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        countdownText.text = Mathf.Ceil(GameManager.instance.GetCountDownTimer()).ToString(); 
    }
}
