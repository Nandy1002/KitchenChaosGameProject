using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetKitchenObjectSO(KitchenObjectScriptableObjects kitchenObjectScriptableObjects)
    {
        image.sprite = kitchenObjectScriptableObjects.sprite;
    }
}
