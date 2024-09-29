using System;
using UnityEngine;

public class StoveFrySound : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    private AudioSource audioSource; 
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if(!audioSource.isPlaying){
            if(playSound){
                audioSource.Play();
            }else{
                audioSource.Pause();
            }
        }
        if(audioSource.isPlaying && !playSound){
            audioSource.Pause();
        }
    }
}
