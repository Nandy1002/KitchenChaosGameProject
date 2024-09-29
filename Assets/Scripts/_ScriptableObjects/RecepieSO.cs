using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecepieSO", menuName = "RecepieSO")]
public class RecepieSO : ScriptableObject
{
    public string recepieName;
    public List<KitchenObjectScriptableObjects> recepie; 
}
