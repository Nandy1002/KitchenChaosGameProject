using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitchenObjectScriptableObjects kitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectScriptableObjects GetKitchenObjectSO => kitchenObjectSO;
    public void SetKitchObjectParent(IKitchenObjectParent kitchenObject)
    {
        if(kitchenObjectParent != null){
            kitchenObjectParent.RemoveKitchenObject();
        }
        kitchenObjectParent = kitchenObject;
        if(kitchenObject.HasKitchenObject()){
            Debug.LogError("Counter already has a kitchen object");
        }
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObject.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent(){
        return kitchenObjectParent;
    }

    public static KitchenObject SpwanKitchenObject(KitchenObjectScriptableObjects kitchenObjectSO, IKitchenObjectParent parent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchObjectParent(parent);
        return kitchenObject;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.RemoveKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlateObject(out PlateKitchenObject plateKitchenObject)
    {

        if(this is PlateKitchenObject){
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }else{
            plateKitchenObject = null;
            return false;
        }
    }
}
