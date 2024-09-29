using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "musicVolume";
    public static MusicManager instance {get; private set;}
    [SerializeField] private AudioSource audioSource;
    private float volume = 1f;
    public void changeVolume(){
        volume += 0.1f;
        if(volume > 1f){
            volume = 0f;
        }
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume(){
        return volume;
    } 
    private void Awake() {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 1f);
        audioSource.volume = volume;
    }
    private void Start() {
        
    }
}
