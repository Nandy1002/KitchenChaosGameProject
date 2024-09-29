using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct PlateIngridents
    {
        public KitchenObjectScriptableObjects kitchenObjectSO;
        public GameObject visual;

    };
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<PlateIngridents> plateIngridentList;
    private void Start() {
        plateKitchenObject.OnIngridentsAdded += PlateKitchenObject_OnIngridentsAdded;
        foreach (var item in plateIngridentList)
        {
            item.visual.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngridentsAdded(object sender, PlateKitchenObject.OnIngridentsAddedEventArgs e)
    {
        foreach (var item in plateIngridentList)
        {
            if(item.kitchenObjectSO == e.kitchenObjectScriptableObjects){
                item.visual.SetActive(true);
            }
        }
    }
}
