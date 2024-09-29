using System;
using UnityEngine;

public class DeliveryCounter : BaseCounter 
{
    public static DeliveryCounter instance{get; private set;}
    private DeliveryManager deliveryManager;
    private void Awake() {
        instance = this;
    }

    public override void Interact(Movement player)
    {
        if(player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlateObject(out PlateKitchenObject plateKitchenObject))
            {
                DeliveryManager.instance.DeliverRecepie(plateKitchenObject);
                player.GetKitchenObject().DestroySelf();
            }

        }
    }
}
