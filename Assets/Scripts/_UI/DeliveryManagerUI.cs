using System;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recepieTemplateUI;
    
    private void Awake() {
        recepieTemplateUI.gameObject.SetActive(false);
    }
    private void Start() {
        DeliveryManager.instance.OnRecepieAdded += DeliveryManager_OnRecepieAdded;
        DeliveryManager.instance.OnRecepieCompleted += DeliveryManager_OnRecepieCompleted;

        UpdateVisual();
    }

    private void DeliveryManager_OnRecepieCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecepieAdded(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual(){
        foreach (Transform child in container)
        {
            if(child == recepieTemplateUI) continue;
            Destroy(child.gameObject);
        }
        foreach (RecepieSO recepie in DeliveryManager.instance.GetWaitingRecepieList())
        {
            Transform recepieTransform = Instantiate(recepieTemplateUI, container);
            recepieTransform.GetComponent<DeliverManagerSingleUI>().SetRecepieName(recepie);
            recepieTransform.GetComponent<DeliverManagerSingleUI>().SetIcons(recepie);
            recepieTransform.gameObject.SetActive(true);
        }
    }
}
