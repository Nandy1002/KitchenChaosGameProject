
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    private void Awake() {
        quitButton.onClick.AddListener(() => Application.Quit());
        playButton.onClick.AddListener(() => Loader.LoadScene(Loader.Scene.MainGameScene));
    } 
}
