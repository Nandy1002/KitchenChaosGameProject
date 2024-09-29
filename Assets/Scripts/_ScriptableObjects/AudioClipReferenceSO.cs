using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipReferenceSO", menuName = "AudioClipReferenceSO")]
public class AudioClipReferenceSO : ScriptableObject
{
    public AudioClip[] deliverySuccess;
    public AudioClip[] deliveryFailed;
    public AudioClip[] chop;
    public AudioClip[] fry;
    public AudioClip[] footStep;
    public AudioClip[] trash;
    public AudioClip[] pickUp;
    public AudioClip[] drop; 
    public AudioClip[] warning;
}
