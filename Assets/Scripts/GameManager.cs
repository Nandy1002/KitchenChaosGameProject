using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public event EventHandler OnGameStateChanged;
    public event EventHandler OnGamePaused;
    private  enum State{
        waitingToStart,
        countdownToStart,
        running,
        finished
    }
    private State state;
    float waitingTime = 3f;
    float timeToStart = 3f;
    float runningTime;
    float runningTimeMax = 300f;
    bool isGamePaused = false;  
    private void Awake() {
        instance = this;
        Time.timeScale = 1f;
    }
    private void Start() {
        state = State.waitingToStart;
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        PauseGame();
    }

    private void PauseGame()
    {
        isGamePaused = !isGamePaused;
        if(isGamePaused){
            Debug.Log("Game Paused");
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }else{
            Time.timeScale = 1f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update() {
       switch(state){
           case State.waitingToStart:
                waitingTime -= Time.deltaTime;
                if(waitingTime <= 0){
                    state = State.countdownToStart;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);              
                }
                break;
            case State.countdownToStart:
                timeToStart -= Time.deltaTime;
                if(timeToStart <= 0){
                    state = State.running;
                    runningTime = runningTimeMax;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.running:
                runningTime -= Time.deltaTime;
                if(runningTime <= 0){
                    state = State.finished;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.finished:
                Debug.Log("Game Finished");
                OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                break;
       } 
       //Debug.Log(state);
    }

public bool isGamePlaying(){
    return state == State.running;
}
public bool isCountdown(){
    return state == State.countdownToStart;
}
public float GetCountDownTimer(){
    return timeToStart;
}
public float GetRunningTimer(){
    return runningTime;
}
public bool isGameFinished(){
    return state == State.finished;
}
public float GetGameRunningTimerNormalized(){
    return  1 - (runningTime / runningTimeMax);
}
}

