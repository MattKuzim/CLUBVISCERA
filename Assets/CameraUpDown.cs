using UnityEngine;

public class CameraUpDown : MonoBehaviour
{
    [Tooltip("Mouse sensitivity for vertical look")]
    public float sensitivity = 2f;

    [Tooltip("Maximum angle up/down from 0 in degrees")]
    public float maxAngle = 70f;

    [Tooltip("Maximum camera roll when strafing (degrees)")]
    public float maxRoll = 15f;

    [Tooltip("How quickly the roll interpolates to target")]
    public float rollSmooth = 8f;

    float pitch = 0f;
    float currentRoll = 0f;

    SimpleCharacter characterController;

    void Start()
    {
Cursor.lockState = CursorLockMode.Locked;

        float eulerX = transform.localEulerAngles.x;
        if (eulerX > 180f) eulerX -= 360f;
        pitch = eulerX;

        characterController = GetComponent<SimpleCharacter>();
    }

    void Update()
    {


        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -Mathf.Abs(maxAngle), Mathf.Abs(maxAngle));

        Vector3 localEuler = transform.localEulerAngles;
        float preservedY = localEuler.y;

        float targetRoll = 0f;
        if (characterController != null)
        {
            Vector3 velocity = characterController.rb.linearVelocity;
            Vector3 rightDir = transform.parent != null ? transform.parent.right : transform.right;
            float lateralVelocity = Vector3.Dot(velocity, rightDir);

            targetRoll = Mathf.Clamp(lateralVelocity / 5f, -1f, 1f) * maxRoll;
        }

        currentRoll = Mathf.Lerp(currentRoll, targetRoll, Time.deltaTime * rollSmooth);

        if (transform.parent != null)
        {
            Quaternion pitchInParent = Quaternion.Euler(pitch, 0f, 0f);
            Quaternion preservedYZ = Quaternion.Euler(0f, preservedY, currentRoll);
            transform.localRotation = pitchInParent * preservedYZ;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(pitch, preservedY, currentRoll);
        }
    }
}
