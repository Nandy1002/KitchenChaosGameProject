using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectScriptableObjects kitchenObjectSO;
    public override void Interact(Movement player){
        // if(kitchenObject == null){
        //     //instanciate object if kitchen object is not there...
        //     Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab,clearCounterTopPoint);
        //     kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchObjectParent(this);
        // }else{
        //     //if kitchen object is there
        //     // Debug.Log(kitchenObject.GetClearCounter().name);
        //     //give it to the player
        //     kitchenObject.SetKitchObjectParent(player);
        // }
        if(!player.HasKitchenObject()){
            KitchenObject.SpwanKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
