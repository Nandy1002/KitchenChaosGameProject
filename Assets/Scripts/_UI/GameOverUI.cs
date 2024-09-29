using System;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI points;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
        GameManager.instance.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(object sender, EventArgs e)
    {
        if(GameManager.instance.isGameFinished()){
            gameObject.SetActive(true);
            points.gameObject.SetActive(true);
        }else{
            gameObject.SetActive(false);
            points.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        points.text = DeliveryManager.instance.GetRecepiePoints().ToString();
    }
}
