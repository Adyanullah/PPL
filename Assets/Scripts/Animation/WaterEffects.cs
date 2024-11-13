using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class WaterEffects : MonoBehaviour
{
    public CameraEffects cameraEffects;
    public PostProcessVolume underwaterVolume;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraEffects.StartShake();
            underwaterVolume.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraEffects.StopShake();
            underwaterVolume.enabled = false;
        }
    }
}
