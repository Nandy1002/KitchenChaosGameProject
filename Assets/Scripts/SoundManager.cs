using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_VOLUME = "volume";
    public static SoundManager instance;
    [SerializeField] private AudioClipReferenceSO audioClipReferences;
    private float volume = 1f;
    private void Awake() {
        instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_VOLUME, 1f);  
    }
    private void Start() {
        instance = this;
        DeliveryManager.instance.onRecepieSuccess += DeliveryManager_onRecepieSuccess;
        DeliveryManager.instance.onRepieFailed += DeliveryManager_onRepieFailed;
        CuttingCounter.OnKitchenObjectCut += CuttingCounter_OnKitchenObjectCut;
        Movement.OnKitchenObjectGrabbed += Movement_OnKitchenObjectGrabbed;
        BaseCounter.OnKitchenObjectDrop += BaseCounter_OnKitchenObjectDrop;
        TrashCounter.OnKitchenObjectDestroyed += TrashCounter_OnKitchenObjectDestroyed;
    }

    private void TrashCounter_OnKitchenObjectDestroyed(object sender, EventArgs e)
    {
        PlaySound(audioClipReferences.trash, (sender as TrashCounter).transform.position);
    }

    private void BaseCounter_OnKitchenObjectDrop(object sender, EventArgs e)
    {
        PlaySound(audioClipReferences.drop, (sender as BaseCounter).transform.position);
    }

    private void Movement_OnKitchenObjectGrabbed(object sender, EventArgs e)
    {
        PlaySound(audioClipReferences.pickUp, (sender as Movement).transform.position);
    }

    private void CuttingCounter_OnKitchenObjectCut(object sender, EventArgs e)
    {
        PlaySound(audioClipReferences.chop, (sender as CuttingCounter).transform.position);
    }

    private void DeliveryManager_onRepieFailed(object sender, EventArgs e)
    {
        PlaySound(audioClipReferences.deliveryFailed, DeliveryCounter.instance.transform.position);
    }

    private void DeliveryManager_onRecepieSuccess(object sender, EventArgs e)
    {
        PlaySound(audioClipReferences.deliverySuccess, DeliveryCounter.instance.transform.position);
    }

    private void PlaySound(AudioClip[] audioList, Vector3 position, float volumnMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioList[UnityEngine.Random.Range(0,audioList.Length)], position, volumnMultiplier * volume);
    }
    public void PlayFootStepSound(Vector3 poisiton, float volumn = 1f)
    {
        PlaySound(audioClipReferences.footStep, poisiton, volumn);
    }
    
    public void changeVolume(){
        volume += 0.1f;
        if(volume > 1f){
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume(){
        return volume;
    }
}
