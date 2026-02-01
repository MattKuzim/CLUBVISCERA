using UnityEngine;

public class FootStepper : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private float baseStepInterval = 0.5f;
    [SerializeField] private float minVelocityThreshold = 0.1f;
    
    private AudioSource audioSource;
    private float stepTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float velocity = rb.linearVelocity.magnitude;
        
        if (velocity < minVelocityThreshold)
            return;
        
        float stepInterval = baseStepInterval / (velocity * 0.1f);
        stepTimer -= Time.deltaTime;
        
        if (stepTimer <= 0)
        {
            PlayFootstep();
            stepTimer = stepInterval;
        }
    }

    void PlayFootstep()
    {
        if (footstepSounds.Length > 0 && audioSource != null)
        {
            AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}