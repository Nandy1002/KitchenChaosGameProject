using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Movement player;
    private float footStepTimer = 0f;
    private float footStepInterval = 0.1f;
    private void Awake() {
        player = GetComponent<Movement>();
    }
    private void Update() {
        footStepTimer -= Time.deltaTime;
        if(footStepTimer < 0f){
            footStepTimer = footStepInterval;
            if(player.IsWalking()){
                SoundManager.instance.PlayFootStepSound(transform.position);
            }
        }
    }
}
