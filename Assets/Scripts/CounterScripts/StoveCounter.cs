using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHaveProgress
{
    public class OnStateChangeEventArgs : EventArgs{
        public State state;
    }
    public event EventHandler<OnStateChangeEventArgs> OnStateChanged;
    public event EventHandler<IHaveProgress.OnProgressChangeEventArgs> OnProgressChange;
    public enum State{
        Idle,
        Frying,
        Fried,
        Burned,
    }
    [SerializeField] private FryingRecipieSO[] fryingRecipies;
    [SerializeField] private FryingRecipieSO[] burningRecipies;
    private float fryTime;
    private float burnTime;
    private FryingRecipieSO fryingRecipie;
    private FryingRecipieSO burningRecipie;
    private State state;

    private void Start() {
        fryTime = 0;
        burnTime = 0;
        state = State.Idle;
        OnStateChanged?.Invoke(this,new OnStateChangeEventArgs{
                        state = state
        }); 
    }
    private void Update() {

        switch(state){
            case State.Idle:
                OnStateChanged?.Invoke(this,new OnStateChangeEventArgs{
                        state = state
                    });
                break;
            case State.Burned:
                OnStateChanged?.Invoke(this,new OnStateChangeEventArgs{
                        state = state
                    });
                break;
            case State.Frying:
                fryTime += Time.deltaTime;
                if(fryTime >= fryingRecipie.timeMax){
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpwanKitchenObject(fryingRecipie.output,this);
                    //Debug.Log("Object Fried");
                    //Debug.Log(fryingRecipie.output);
                    burningRecipie = GetBurningRecepie(GetKitchenObject().GetKitchenObjectSO);  
                    state = State.Fried;
                }
                OnStateChanged?.Invoke(this,new OnStateChangeEventArgs{
                        state = state
                    });
                OnProgressChange?.Invoke(this,new IHaveProgress.OnProgressChangeEventArgs{
                        ProgressNormalized = fryTime/fryingRecipie.timeMax
                    });
                break;
            case State.Fried:
                burnTime += Time.deltaTime;
                if(burnTime >= burningRecipie.timeMax){
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpwanKitchenObject(burningRecipie.output,this);
                    state = State.Burned;
                }
                OnStateChanged?.Invoke(this,new OnStateChangeEventArgs{
                        state = state
                }); 
                OnProgressChange?.Invoke(this,new IHaveProgress.OnProgressChangeEventArgs{
                    ProgressNormalized = burnTime/burningRecipie.timeMax
                }); 
                break; 
        }

    }
    public override void Interact(Movement player)
    {
        if(!HasKitchenObject()){
            if(player.HasKitchenObject() && HasFryingRecepie(player.GetKitchenObject().GetKitchenObjectSO)){
                //counter dont have object but player have so lets switch
                player.GetKitchenObject().SetKitchObjectParent(this);
                fryingRecipie = GetFryingRecepie(GetKitchenObject().GetKitchenObjectSO);
                burnTime = 0f;
                state = State.Frying;
                fryTime = 0;
                /*OnStateChanged?.Invoke(this,new OnStateChangeEventArgs{
                        state = state
                    }); */
            }
        }else{
            if(!player.HasKitchenObject()){
                //player dont have object but counter have so lets switch
                GetKitchenObject().SetKitchObjectParent(player);
                state = State.Idle;
                fryTime = 0;
                /*OnStateChanged?.Invoke(this,new OnStateChangeEventArgs{
                        state = state
                    });*/
            }else{
                //both have kitchen object
                //if counter have plate then player can put food on it 
                if(player.GetKitchenObject().TryGetPlateObject(out PlateKitchenObject plate)){
                    //player is holding plate
                    if(plate.TryAddIngredients(GetKitchenObject().GetKitchenObjectSO)){
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;
                    }

                }
            }
        }
    }

    private bool HasFryingRecepie(KitchenObjectScriptableObjects input){
        FryingRecipieSO cuttingRecepie = GetFryingRecepie(input);
        return cuttingRecepie != null;
    }
    private KitchenObjectScriptableObjects GetFryingKichenObject(KitchenObjectScriptableObjects input){
        FryingRecipieSO cuttingRecepie = GetFryingRecepie(input);
        if(cuttingRecepie != null){
            return cuttingRecepie.output;
        }else{
            return null;
        }
    }
    public FryingRecipieSO GetFryingRecepie(KitchenObjectScriptableObjects input){
        foreach (FryingRecipieSO item in fryingRecipies)
        {
            if(item.input == input){
                return item;
            }
        }
        return null;
    }
    public FryingRecipieSO GetBurningRecepie(KitchenObjectScriptableObjects input){
        foreach (FryingRecipieSO item in burningRecipies)
        {
            if(item.input == input){
                return item;
            }
        }
        return null;
    } 
}
