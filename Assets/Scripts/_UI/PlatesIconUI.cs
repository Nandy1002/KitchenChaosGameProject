using UnityEngine;

public class PlatesIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;
    
    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start() {
        plateKitchenObject.OnIngridentsAdded += PlateKitchenObject_OnIngridentsAdded;
    }

    private void PlateKitchenObject_OnIngridentsAdded(object sender, PlateKitchenObject.OnIngridentsAddedEventArgs e)
    {
        UpdateVisual();
    }
    private void UpdateVisual(){
        foreach(Transform child in transform){
            if(child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectScriptableObjects item in plateKitchenObject.GetKitchenObjectSOList()){
            iconTemplate.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(item);
            Transform instance = Instantiate(iconTemplate, transform);
            instance.gameObject.SetActive(true);
        }
    }
}
