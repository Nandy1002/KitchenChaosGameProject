using System;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject selectedCounterVisual;
   private void Start() {
        Movement.Instance.onSelectedCounterChanged += PlayerOnSelectedCounterChanged;
   }

    private void PlayerOnSelectedCounterChanged(object sender, Movement.SelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter != baseCounter){
            Hide();
        }else{
            Show();
        }
    }
    private void Show(){
        selectedCounterVisual.SetActive(true);
    }
    private void Hide(){
        selectedCounterVisual.SetActive(false);
    }

}
