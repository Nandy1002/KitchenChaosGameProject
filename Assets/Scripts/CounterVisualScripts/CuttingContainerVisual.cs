using System;
using UnityEngine;

public class CuttingContainerVisual : MonoBehaviour
{
    private const string CUT = "Cut";
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() {
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Start()
    {
        CuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
