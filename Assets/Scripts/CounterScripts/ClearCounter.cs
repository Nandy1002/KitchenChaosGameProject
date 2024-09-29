using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectScriptableObjects kitchenObjectSO;
    public override void Interact(Movement player){
        if(!HasKitchenObject()){
            if(player.HasKitchenObject()){
                //counter dont have object but player have so lets switch
                player.GetKitchenObject().SetKitchObjectParent(this);
            }
        }else{
            if(!player.HasKitchenObject()){
                //player dont have object but counter have so lets switch
                GetKitchenObject().SetKitchObjectParent(player);
            }else{
                //both have kitchen object
                //if counter have plate then player can put food on it 
                if(player.GetKitchenObject().TryGetPlateObject(out PlateKitchenObject plate)){
                    //player is holding plate
                    if(plate.TryAddIngredients(GetKitchenObject().GetKitchenObjectSO)){
                        GetKitchenObject().DestroySelf();
                    }

                }else{
                    //player is not holding plate
                    if(GetKitchenObject().TryGetPlateObject(out plate)){
                        //counter is holding plate
                        if(plate.TryAddIngredients(player.GetKitchenObject().GetKitchenObjectSO)){
                        player.GetKitchenObject().DestroySelf();
                    }
                }
                
            }
            }
            
        }
    }

    

}