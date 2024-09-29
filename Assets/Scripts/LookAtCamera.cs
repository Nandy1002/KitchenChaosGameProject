using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private new Transform camera;
    private enum Mode{
        CameraForward,
        CameraForwardInverted,
    }
    [SerializeField] private Mode mode;

    private void Awake() {
        
    }
    private void LateUpdate() {
    
        switch(mode){
            case Mode.CameraForward:
                transform.forward = camera.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -camera.transform.forward;
                break;
        }
    }
}
