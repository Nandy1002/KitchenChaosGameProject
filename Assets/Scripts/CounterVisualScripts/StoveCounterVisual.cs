using System;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stoveOnFryState;
    [SerializeField] private GameObject particleObject;
    [SerializeField] private StoveCounter stoveCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stoveCounter.OnStateChanged += stoveCounter_OnStateChanged;
    }

    private void stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Frying;
        stoveOnFryState.SetActive(showVisual);
        particleObject.SetActive(showVisual);
    }

}
