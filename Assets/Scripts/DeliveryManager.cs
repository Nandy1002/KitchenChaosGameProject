
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public event EventHandler OnRecepieAdded;
    public event EventHandler OnRecepieCompleted;
    public event EventHandler onRecepieSuccess;
    public event EventHandler onRepieFailed;
    public static DeliveryManager instance;
    [SerializeField]private List<RecepieSO> totalRecepieList;
    private List<RecepieSO> waitingRecepieList; 
    private float spwanTimer;
    private int recepiePoints;
    [SerializeField]private float spwanTimeMax = 5f;

    private void Awake() {
        instance = this;
        waitingRecepieList = new List<RecepieSO>();
        recepiePoints = 0;
    }

    private void Update() {
        spwanTimer += Time.deltaTime;
        if(spwanTimer >= spwanTimeMax)
        {
            spwanTimer = 0;
            if(waitingRecepieList.Count < 5)
            {
                RecepieSO recepie = totalRecepieList[UnityEngine.Random.Range(0, totalRecepieList.Count)];
                waitingRecepieList.Add(recepie);
                //Debug.Log(recepie.recepieName);
                OnRecepieAdded?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public void DeliverRecepie(PlateKitchenObject plate)
    {
        foreach(RecepieSO recepie in waitingRecepieList)
        {
            if(recepie.recepie.Count == plate.GetKitchenObjectSOList().Count)
            {
                Debug.Log("Recepie Ingredient Count Matched");
                bool isRecepieMatched = true;
                foreach (KitchenObjectScriptableObjects item in recepie.recepie)
                {
                    bool isItemMatched = false;
                    foreach (KitchenObjectScriptableObjects plateItem in plate.GetKitchenObjectSOList())
                    {
                        if(item == plateItem)
                        {
                            isItemMatched = true;
                            break;
                        }
                    }
                    if(!isItemMatched)
                    {
                        isRecepieMatched = false;
                        break;
                    }
                }
                if(isRecepieMatched)
                {
                    recepiePoints++;
                    //Debug.Log("Recepie Matched");
                    waitingRecepieList.Remove(recepie);
                    OnRecepieCompleted?.Invoke(this, EventArgs.Empty);
                    onRecepieSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }

        }
        //Debug.Log("Recepie Not Matched");
        onRepieFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecepieSO> GetWaitingRecepieList()
    {
        return waitingRecepieList;
    }
    public int GetRecepiePoints()
    {
        return recepiePoints;
    }
}
