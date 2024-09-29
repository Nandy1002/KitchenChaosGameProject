using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractActionAlternative;
    private InputSystem_Actions playerInputManager;
    public event EventHandler OnPauseAction;
    private void Awake() {

        Instance = this;
        playerInputManager = new InputSystem_Actions();
        playerInputManager.Player.Enable();

        playerInputManager.Player.Interact.performed += InteractPerformed;
        playerInputManager.Player.InteractAlt.performed += InteractAltPerformed;
        playerInputManager.Player.Pause.performed += PausePerformed;
        
    }
    private void OnDestroy() {
        playerInputManager.Player.Interact.performed -= InteractPerformed;
        playerInputManager.Player.InteractAlt.performed -= InteractAltPerformed;
        playerInputManager.Player.Pause.performed -= PausePerformed; 

        playerInputManager.Dispose();
    }
    private void PausePerformed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractPerformed(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    private void InteractAltPerformed(InputAction.CallbackContext context)
    {
        OnInteractActionAlternative?.Invoke(this, EventArgs.Empty);
    }


/*
    private void Interact()
    {
        Debug.Log("Interact");
    }
*/ 
    public Vector2 GetMovementNormalized()
    {
        Vector2 inputVector = playerInputManager.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
