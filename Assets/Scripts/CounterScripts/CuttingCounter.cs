using System;
using UnityEngine;


public class CuttingCounter : BaseCounter, IHaveProgress 
{
    [SerializeField] private CuttingRecipieSO[] cuttingRecepies;
    private int cuttingProgress;
    public static event EventHandler OnCut;
    public event EventHandler<IHaveProgress.OnProgressChangeEventArgs> OnProgressChange;
    
    public static event EventHandler OnKitchenObjectCut;

    new public static void ResetStaticData(){
        OnCut = null;
        OnKitchenObjectCut = null;
    }
    public override void Interact(Movement player)
    {
        if(!HasKitchenObject()){
            if(player.HasKitchenObject() && HasCuttingRecepie(player.GetKitchenObject().GetKitchenObjectSO)){
                //counter dont have object but player have so lets switch
                player.GetKitchenObject().SetKitchObjectParent(this);
                cuttingProgress = 0;
                CuttingRecipieSO cuttingRecipieSO = GetCuttingRecepie(GetKitchenObject().GetKitchenObjectSO);
                OnProgressChange?.Invoke(this, new IHaveProgress.OnProgressChangeEventArgs{
                        ProgressNormalized = (float)cuttingProgress/cuttingRecipieSO.cutNumbersMax   
                    });
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

                }
            }
        }
    }
    public override void InteractAlternate(Movement player)
    {
        if(HasKitchenObject() && HasCuttingRecepie(GetKitchenObject().GetKitchenObjectSO)){
            cuttingProgress++;
            OnKitchenObjectCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipieSO cuttingRecipieSO = GetCuttingRecepie(GetKitchenObject().GetKitchenObjectSO);
            OnCut?.Invoke(this, EventArgs.Empty);
            OnProgressChange?.Invoke(this, new IHaveProgress.OnProgressChangeEventArgs{
                        ProgressNormalized = (float)cuttingProgress/cuttingRecipieSO.cutNumbersMax   
                    });
            if(cuttingProgress >= cuttingRecipieSO.cutNumbersMax){
                KitchenObjectScriptableObjects slicedObjectSO = GetCuttingKichenObject(GetKitchenObject().GetKitchenObjectSO);
                GetKitchenObject().DestroySelf();
                KitchenObject.SpwanKitchenObject(slicedObjectSO, this);
                
            }
        }
    }
    private bool HasCuttingRecepie(KitchenObjectScriptableObjects input){
        CuttingRecipieSO cuttingRecepie = GetCuttingRecepie(input);
        return cuttingRecepie != null;
    }
    private KitchenObjectScriptableObjects GetCuttingKichenObject(KitchenObjectScriptableObjects input){
        CuttingRecipieSO cuttingRecepie = GetCuttingRecepie(input);
        if(cuttingRecepie != null){
            return cuttingRecepie.output;
        }else{
            return null;
        }
    }
    public CuttingRecipieSO GetCuttingRecepie(KitchenObjectScriptableObjects input){
        foreach (CuttingRecipieSO item in cuttingRecepies)
        {
            if(item.input == input){
                return item;
            }
        }
        return null;
    }
}
