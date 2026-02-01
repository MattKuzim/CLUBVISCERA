// csharp
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class CameraTilter : MonoBehaviour
{
    [Tooltip("Transform to sample velocity from (player).")]
    [SerializeField] private Transform target;

    [Tooltip("Optional: if assigned, Rigidbody.velocity will be used instead of position delta.")]
    [SerializeField] private Rigidbody targetRigidbody;

    [SerializeField, Range(0f, 45f)] private float maxTiltAngle = 30f;
    [SerializeField] private float tiltSensitivity = 0.02f;
    [SerializeField] private float smoothTime = 0.08f;
    [SerializeField] private float deadZone = 0.01f;

    private Vector3 lastTargetPos;
    private float currentRoll = 0f;
    private float rollVelocity = 0f;

    void Awake()
    {
        if (target != null) lastTargetPos = target.position;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Reset local Z rotation to zero so we start from a clean base each frame
        Vector3 e = transform.localEulerAngles;
        e.z = 0f;
        transform.localEulerAngles = e;

        // Sample world-space velocity from Rigidbody or position delta
        Vector3 worldVel;
        if (targetRigidbody != null)
        {
            worldVel = targetRigidbody.linearVelocity;
        }
        else
        {
            float dt = Time.deltaTime > 0f ? Time.deltaTime : 1f;
            worldVel = (target.position - lastTargetPos) / dt;
            lastTargetPos = target.position;
        }

        // Convert velocity into the target's local space and use X component
        Vector3 localVel = target.InverseTransformDirection(worldVel);
        float localVelX = localVel.x;

        // Compute desired roll (negative so moving right visually tilts right)
        float targetRoll = 0f;
        if (Mathf.Abs(localVelX) > deadZone)
            targetRoll = -Mathf.Clamp(localVelX * tiltSensitivity, -maxTiltAngle, maxTiltAngle);

        // Smooth and clamp the roll
        currentRoll = Mathf.SmoothDamp(currentRoll, targetRoll, ref rollVelocity, smoothTime);
        currentRoll = Mathf.Clamp(currentRoll, -maxTiltAngle, maxTiltAngle);

        // Apply the roll into the camera's local Z Euler (preserving X/Y)
        e = transform.localEulerAngles;
        e.z = currentRoll;
        transform.localEulerAngles = e;
    }
}
