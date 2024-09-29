using System;
using UnityEngine;


public class Movement : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnKitchenObjectGrabbed;
    public static Movement Instance
    {
        get;
        private set;
    }
    public event EventHandler<SelectedCounterChangedEventArgs> onSelectedCounterChanged; 
    public class SelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private KitchenObject kitchenObject;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private bool isWalking = false;
    private Vector3 lastMoveDirection;
    private BaseCounter selectedCounter;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake() {
        if(Instance != null){
            Debug.LogError("There is more than one instance of Player in the scene");
        }
        Instance = this;
    }
    void Start(){
        gameInput.OnInteractAction += GameInputOnInteractAction;
        gameInput.OnInteractActionAlternative += GameInputOnInteractActionAlternative; 
    }


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    private void GameInputOnInteractAction(object sender, EventArgs e)
    {
        if(!GameManager.instance.isGamePlaying()){
            return;
        }
        if(selectedCounter != null){
            selectedCounter.Interact(this);
        }
    }
    private void GameInputOnInteractActionAlternative(object sender, EventArgs e)
    {
        if(!GameManager.instance.isGamePlaying()){
            return;
        }
       if(selectedCounter != null){
            selectedCounter.InteractAlternate(this);
        } 
    }
    private void HandleInteractions()
    {
        RaycastHit raycastHit;
        float interactionDistance = 1f;
        Vector2 inputVector = gameInput.GetMovementNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y); 
        if(moveDirection != Vector3.zero){
            lastMoveDirection = moveDirection;
        }

        bool hit = Physics.Raycast(transform.position,lastMoveDirection,out raycastHit,interactionDistance);
        if(hit){
            // Debug.Log("Hit something");
            // Debug.Log(raycastHit.collider.gameObject.name);
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)){
            //has clearCounter
            // Debug.Log(clearCounter.gameObject.name);
                if(baseCounter != selectedCounter){
                    SetSelectedCounter(baseCounter);
                }
            }else{
                SetSelectedCounter(null);
            }

        }else{
            SetSelectedCounter(null);
        }
        //Debug.Log(selectedCounter);
    }
    private void HandleMovement(){
        // actual movement part
        Vector2 inputVector = gameInput.GetMovementNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        
        float moveDistance = moveSpeed*Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up*playerHeight,
            playerRadius,
            moveDirection, moveDistance
        );
        //transform.position += moveDirection * moveSpeed * Time.deltaTime;
        if(canMove){
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }else{
            // cant move toward move direction
            // trying to move towards x
            Vector3 moveDirectionX = new Vector3(moveDirection.x,0,0);
            canMove = moveDirection.x != 0 && !Physics.CapsuleCast(
                transform.position,
                transform.position + Vector3.up*playerHeight,
                playerRadius,
                moveDirectionX, moveDistance
            );
            if(!canMove){
                // cant move towards x direction
                //trying to move towards z
                Vector3 moveDirectionZ = new Vector3(0,0,moveDirection.z);
                moveDirection = moveDirectionZ;
                canMove = moveDirection.z !=0 && !Physics.CapsuleCast(
                    transform.position,
                    transform.position + Vector3.up*playerHeight,
                    playerRadius,
                    moveDirection, moveDistance
                );
                if(canMove){
                    //can move only in Z
                    moveDirection = moveDirectionZ;
                    transform.position += moveDirection * moveSpeed * Time.deltaTime;
                }
            }else{
                // can move towards x
                moveDirection = moveDirectionX;
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }
        }

        // for rotation
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, turnSpeed * Time.deltaTime);
        // movement checking part for animation
        isWalking = moveDirection != Vector3.zero;
        
    }
    public bool IsWalking()
    {
        return isWalking;
    }
    private void SetSelectedCounter(BaseCounter baseCounter)
    {
        selectedCounter = baseCounter;
        onSelectedCounterChanged?.Invoke(this, new SelectedCounterChangedEventArgs{selectedCounter = selectedCounter}); 
    }
    public Transform GetKitchenObjectFollowTransform(){
        return kitchenObjectHoldPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null){
            OnKitchenObjectGrabbed?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }
    public bool HasKitchenObject(){
        return kitchenObject != null;
    }
    public void RemoveKitchenObject(){
        kitchenObject = null;
    }
}
