using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject 
{
    public event EventHandler<OnIngridentsAddedEventArgs> OnIngridentsAdded;
    public class OnIngridentsAddedEventArgs : EventArgs
    {
        public KitchenObjectScriptableObjects kitchenObjectScriptableObjects;
    }
    private List<KitchenObjectScriptableObjects> kitchenObjectList;
    [SerializeField]private List<KitchenObjectScriptableObjects> validKitcenObjectList;
    private void Awake() {
        kitchenObjectList = new List<KitchenObjectScriptableObjects>();
    }
    public bool TryAddIngredients(KitchenObjectScriptableObjects kitchenObjectScriptableObjects){
        if(kitchenObjectList.Contains(kitchenObjectScriptableObjects) ||  !validKitcenObjectList.Contains(kitchenObjectScriptableObjects)){
            return false;
        }else{
            kitchenObjectList.Add(kitchenObjectScriptableObjects);
            OnIngridentsAdded?.Invoke(this, new OnIngridentsAddedEventArgs{ kitchenObjectScriptableObjects = kitchenObjectScriptableObjects});
            return true;
        }
    }

    public List<KitchenObjectScriptableObjects> GetKitchenObjectSOList(){
        return kitchenObjectList;
    }


}
