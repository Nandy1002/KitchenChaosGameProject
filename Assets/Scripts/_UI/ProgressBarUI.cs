using UnityEngine;
using UnityEngine.UI;
public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private IHaveProgress hasProgress;
    [SerializeField] private GameObject hasProgressGameObject;
    private void Start(){
        hasProgress = hasProgressGameObject.GetComponent<IHaveProgress>();
        if(hasProgress == null){
            Debug.LogError("Game object" + hasProgressGameObject + "dont have game IhaveProgress componenet");
        }
        hasProgress.OnProgressChange += HasProgress_OnProgressChange;
        progressBar.fillAmount = 0f;
        ActiveBar();
    }
    private void Update(){
        ActiveBar();
    }
    private void HasProgress_OnProgressChange(object sender, IHaveProgress.OnProgressChangeEventArgs e)
    {
        progressBar.fillAmount = e.ProgressNormalized;
        // if(e.ProgressNormalized == 0f || e.ProgressNormalized == 1f){
        //     Hide();
        // }else{
        //     show();
        // }
    }

    private void ActiveBar()
    {
        if(hasProgress.HasKitchenObject()){
            GetComponent<Canvas>().enabled = true;
        }else{
            GetComponent<Canvas>().enabled = false;
        }
    }
    private void show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    }
}
