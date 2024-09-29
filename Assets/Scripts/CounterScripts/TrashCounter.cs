using System;
using UnityEngine;

public class TrashCounter : BaseCounter 
{
    public static event EventHandler OnKitchenObjectDestroyed;
    new public static void ResetStaticData(){
        OnKitchenObjectDestroyed = null;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Interact(Movement player){
        if(player.HasKitchenObject()){
                //counter dont have object but player have so lets switch
                player.GetKitchenObject().DestroySelf();
                OnKitchenObjectDestroyed?.Invoke(this, EventArgs.Empty);
            }
    }
}
