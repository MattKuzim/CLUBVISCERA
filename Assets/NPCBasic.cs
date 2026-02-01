using UnityEngine;


public interface IReceiveDamage
{
    

   public void ReceiveDamage(int damageAmount);

   public void OnShove(float forceAmount, Vector3 direction);
}
public class NPCBasic : MonoBehaviour, IReceiveDamage
{
    
    
    Rigidbody rb { get; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    
    public void ReceiveDamage(int damageAmount)
    {
        Debug.Log(gameObject.name + " received " + damageAmount + " damage.");
    }
    public void OnShove(float forceAmount, Vector3 direction)
    {
        Debug.Log(gameObject.name + " was shoved.");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
