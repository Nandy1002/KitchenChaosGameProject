using UnityEngine;

[CreateAssetMenu(fileName = "New Kitchen Object", menuName = "Kitchen Object")]
public class KitchenObjectScriptableObjects : ScriptableObject
{
    public string objectName;
    public Transform prefab;
    public Sprite sprite;
}
