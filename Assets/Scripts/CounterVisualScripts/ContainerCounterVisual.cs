using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;
    private const string OPEN_CLOSE = "OpenClose";
    private void Awake() {
        animator = GetComponent<Animator>();
    }
        

    void Start()
    {
        containerCounter.OnPlayerGrabbedObject += HandlePlayerGrabbedObject;
    }

    private void HandlePlayerGrabbedObject(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
