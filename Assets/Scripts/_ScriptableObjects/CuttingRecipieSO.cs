using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipieSO", menuName = "Cutting Recepie")]
public class CuttingRecipieSO : ScriptableObject
{
   public KitchenObjectScriptableObjects input;
   public KitchenObjectScriptableObjects output; 
   public int cutNumbersMax;
}
