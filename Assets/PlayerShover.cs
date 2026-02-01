using UnityEngine;

public class PlayerShover : MonoBehaviour
{
    [SerializeField] private float shoveForce = 10f;
    private Rigidbody rb;
    
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        IReceiveDamage damageable = collision.gameObject.GetComponent<IReceiveDamage>();
        
        if (damageable != null)
        {
            // Calculate direction from collision point to the other object
            Vector3 collisionDirection = (collision.transform.position - transform.position).normalized;
            
            // Pass force and direction (using x-axis as reference for direction float)
            damageable.OnShove(shoveForce, collisionDirection);
        }
    }
}