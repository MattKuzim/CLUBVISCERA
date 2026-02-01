// csharp
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class SimpleCharacter : MonoBehaviour
{
    [Header("Speeds")]
    public float walkSpeed = 5f;
    public float sprintMultiplier = 1.75f;

    [Header("Acceleration")]
    public float groundAcceleration = 50f;
    public float airAcceleration = 10f;

    [Header("Jump")]
    public float jumpHeight = 1.6f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundMask;

    [Header("Mouse Look")]
    public float mouseSensitivity = 3f;

  public  Rigidbody rb;
    public Vector2 input;
    bool wantJump;
    bool sprinting;
    [SerializeField] public bool grounded;

    float yaw;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = true; // ensure gravity affects the player

        if (groundCheck == null)
        {
            // create a default groundCheck just below the transform if none assigned
            GameObject go = new GameObject("GroundCheck");
            go.hideFlags = HideFlags.DontSaveInBuild | HideFlags.HideInHierarchy;
            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.down * 0.9f;
            groundCheck = go.transform;
        }

        yaw = transform.eulerAngles.y;
    }

    void Update()
    {
        // Mouse look: horizontal mouse movement rotates yaw around Y axis
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        yaw += mouseX;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z);

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        sprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if (Input.GetButtonDown("Jump"))
            wantJump = true;
    }

    void FixedUpdate()
    {
        CheckGround();

        // compute desired horizontal velocity in world space
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 desiredDir = (right * input.x + forward * input.y);
        if (desiredDir.sqrMagnitude > 1f) desiredDir.Normalize();

        float targetSpeed = walkSpeed * (sprinting ? sprintMultiplier : 1f);
        Vector3 desiredVel = desiredDir * targetSpeed;

        // current horizontal velocity
        Vector3 vel = rb.linearVelocity;
        Vector3 horizontalVel = new Vector3(vel.x, 0f, vel.z);

        // choose acceleration based on grounded state
        float accel = grounded ? groundAcceleration : airAcceleration;

        // compute velocity change and apply (clamped by accel * dt)
        Vector3 velChange = desiredVel - horizontalVel;
        float maxChange = accel * Time.fixedDeltaTime;
        velChange = Vector3.ClampMagnitude(velChange, maxChange);

        rb.AddForce(velChange, ForceMode.VelocityChange);

        // Jump handling: apply an upward velocity impulse while preserving horizontal momentum
        if (wantJump && grounded)
        {
            float gravity = Physics.gravity.magnitude;
            float jumpVel = Mathf.Sqrt(2f * jumpHeight * gravity);

            Vector3 newVel = rb.linearVelocity;
            newVel.y = 0f; // reset vertical to ensure consistent jump impulse
            rb.linearVelocity = newVel;
            rb.AddForce(Vector3.up * jumpVel, ForceMode.VelocityChange);
        }

        wantJump = false;
    }

    void CheckGround()
    {
        // Use OverlapSphere to get all colliders in the ground mask, then exclude any that belong to the player.
        Collider[] hits = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundMask, QueryTriggerInteraction.Ignore);

        grounded = false;
        for (int i = 0; i < hits.Length; i++)
        {
            Collider col = hits[i];

            // Ignore colliders that are part of this player (either attached to the same Rigidbody or are a child transform)
            if (col.attachedRigidbody == rb) continue;
            if (col.transform.IsChildOf(transform)) continue;

            // If we reach here, this collider is a valid ground contact
            grounded = true;
            break;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
