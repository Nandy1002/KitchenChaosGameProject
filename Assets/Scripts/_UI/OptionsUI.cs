using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI instance{get; private set;}
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Button closeButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() {
        instance = this;
        soundEffectButton.onClick.AddListener(() => 
            {
                SoundManager.instance.changeVolume();
                UpdateVisual();
            }
        );
        musicButton.onClick.AddListener(() => 
            {
                MusicManager.instance.changeVolume();
                UpdateVisual();
            }
        );
        closeButton.onClick.AddListener(() => 
            {
                Hide();
            }
        );
    }
    private void Start() {
        GameManager.instance.OnGamePaused += GamaManager_OnGamePaused;
        UpdateVisual();
        Hide();
    }

    private void GamaManager_OnGamePaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void Update() {
        
    }
    private void UpdateVisual(){
        soundEffectText.text = "Sound Volume: " + Mathf.Round(SoundManager.instance.GetVolume() * 10f);
        musicText.text = "Music Volume: " + Mathf.Round(MusicManager.instance.GetVolume() * 10f);
    }
    public void Show(){
        gameObject.SetActive(true);
    }
    public void Hide(){
        gameObject.SetActive(false);
    }
}
