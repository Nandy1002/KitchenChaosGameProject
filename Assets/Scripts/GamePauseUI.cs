using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() {
        mainMenuButton.onClick.AddListener(()=>{
            Loader.LoadScene(Loader.Scene.MainMenuScene);
        });
        optionButton.onClick.AddListener(()=>{
            OptionsUI.instance.Show();
        });
    }
    void Start()
    {
        gameObject.SetActive(false);
        GameManager.instance.OnGamePaused += Instance_OnGamePaused;
    }

    private void Instance_OnGamePaused(object sender, EventArgs e)
    {
        if(Time.timeScale == 0f){
            gameObject.SetActive(true);
        }else{
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
