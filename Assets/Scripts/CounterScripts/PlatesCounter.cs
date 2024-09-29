using System;
using UnityEngine;

public class PlatesCounter : BaseCounter 
{
    
    public event EventHandler OnPlateSpwaned;
    public event EventHandler OnPlateRemoved;
    [SerializeField] private KitchenObjectScriptableObjects plateKitchObjectSO;

    private float spwanPlateTimer;
    private float spwanPlateTimerMax = 4f;
    private int plateSpwanAmount;
    private int plateSpwanAmountMax = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spwanPlateTimer += Time.deltaTime;
        if(spwanPlateTimer > spwanPlateTimerMax){
            spwanPlateTimer = 0f;
            //KitchenObject.SpwanKitchenObject(plateKitchObjectSO,this);
            if(plateSpwanAmount < plateSpwanAmountMax){
                OnPlateSpwaned?.Invoke(this,EventArgs.Empty);
                plateSpwanAmount++;
                
            }
        }
    }
    public override void Interact(Movement player)
    {
        if(!player.HasKitchenObject()){
            if(plateSpwanAmount > 0){
                plateSpwanAmount--;
                KitchenObject.SpwanKitchenObject(plateKitchObjectSO,player);
                OnPlateRemoved?.Invoke(this,EventArgs.Empty);
            }
        }
    }
}
