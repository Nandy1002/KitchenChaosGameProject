using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
   [SerializeField] private Transform counterTopPoint;
   [SerializeField] private PlatesCounter platesCounter;
   [SerializeField] private Transform plateVisualPrefab;
    private List<GameObject> plateVisualGameObjectList;

    

    private void Awake() {
        plateVisualGameObjectList = new List<GameObject>();
    }
   private void Start() {
    platesCounter.OnPlateSpwaned += PlatesCounter_OnPlateSpwaned;
    platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
   }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject plateVisual = plateVisualGameObjectList[plateVisualGameObjectList.Count-1];
        plateVisualGameObjectList.Remove(plateVisual);
        Destroy(plateVisual);
    }

    private void PlatesCounter_OnPlateSpwaned(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab,counterTopPoint);
        float plateoffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0,plateoffsetY * plateVisualGameObjectList.Count,0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}


