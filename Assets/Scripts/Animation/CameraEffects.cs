using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public Transform cameraTransform;
    public float shakeIntensity = 0.1f;
    public float shakeSpeed = 1.0f;
    private bool isShaking = false;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = cameraTransform.localPosition;
    }

    public void StartShake()
    {
        isShaking = true;
    }

    public void StopShake()
    {
        isShaking = false;
        cameraTransform.localPosition = originalPosition;
    }

    private void Update()
    {
        if (isShaking)
        {
            float shakeOffsetX = Mathf.Sin(Time.time * shakeSpeed) * shakeIntensity;
            float shakeOffsetY = Mathf.Cos(Time.time * shakeSpeed) * shakeIntensity;
            cameraTransform.localPosition = originalPosition + new Vector3(shakeOffsetX, shakeOffsetY, 0);
        }
    }
}
