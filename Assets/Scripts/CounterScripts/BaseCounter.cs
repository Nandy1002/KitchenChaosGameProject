using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnKitchenObjectDrop;
    public static void ResetStaticData(){
        OnKitchenObjectDrop = null;
    }
    private KitchenObject kitchenObject;
    public Transform clearCounterTopPoint;
    public virtual void Interact(Movement player){
        Debug.LogError("Interacting with base counter");
    }
    public virtual void InteractAlternate(Movement player){
        //Debug.LogError("Interacting Alternative with base counter");
    }
    public virtual Transform GetKitchenObjectFollowTransform(){
        return clearCounterTopPoint;
    }
    public virtual void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null){
            OnKitchenObjectDrop?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }
    public bool HasKitchenObject(){
        return kitchenObject != null;
    }
    public void RemoveKitchenObject(){
        kitchenObject = null;
    }
}
