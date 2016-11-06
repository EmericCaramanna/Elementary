using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    // Used to shake the camera
    public Transform camTransform;

    // Duration of the shaking in second
    public float shakeDuration = 0f;

    // Value of the intensity of the shaking
    public float shakeAmount = 0.1f;
    // Decrease factor for the duration
    public float decreaseFactor = 1.0f;
    private float previousDuration = 0f;

    // Original position of the Camera before the shaking
    Vector3 originalPos;

    // If the camTransform is not set
    // gets the transform of the attached gameObject
    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    // Sets the initial position of the camera
    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    // Move randomly the camera to other positions
    // and reset the camera position when the duration is not greater than zero
    void Update()
    {
        if (shakeDuration > 0 && previousDuration == 0)
        {
            originalPos = camTransform.localPosition;
        }
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            previousDuration = shakeDuration;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else if (previousDuration > 0)
        {
            previousDuration = 0;
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}
