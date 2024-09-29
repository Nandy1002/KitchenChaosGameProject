using UnityEngine;

[CreateAssetMenu(fileName = "FryingRecipie", menuName = "Frying Recipie")]
public class FryingRecipieSO : ScriptableObject
{
   public KitchenObjectScriptableObjects input;
   public KitchenObjectScriptableObjects output; 
   public float timeMax;
}
