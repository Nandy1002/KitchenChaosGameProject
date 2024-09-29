using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliverManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recepieNameText;
    [SerializeField] private Image iconImage;
    [SerializeField] private Transform iconContainer;
    private void Awake() {
        iconImage.gameObject.SetActive(false);
    }
    public void SetRecepieName(RecepieSO recepieSO)
    {
        recepieNameText.text = recepieSO.recepieName;
    } 
    public void SetIcons(RecepieSO recepieSO){
        foreach(Transform child in iconContainer){
            if(child == iconImage.transform) continue;
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectScriptableObjects kitchenObject in recepieSO.recepie){
            Transform iconTransform = Instantiate(iconImage.transform, iconContainer);
            iconTransform.GetComponent<Image>().sprite = kitchenObject.sprite;
            iconTransform.gameObject.SetActive(true);
        }
    }
}
