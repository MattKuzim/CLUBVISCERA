using UnityEngine;


public interface IReceiveDamage
{
    


   public void ReceiveDamage(int damageAmount);
  
   
   public void OnShove(float forceAmount, Vector3 direction);
}
public class NPCBasic : MonoBehaviour, IReceiveDamage
{ 
    [SerializeField] float shotCooldown;
    public Animator NPCAnimator { get; set; }
    public bool beingShot;
    [SerializeField] Animator npcAnimator;
    
    Rigidbody rb { get; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npcAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        shotCooldown -= Time.deltaTime;
        if (shotCooldown <= 0.90f)
        {beingShot = false;}

        else
        {
            beingShot = true;
           
        }
        npcAnimator.SetBool("beingshot", beingShot);
    }
    
    public void ReceiveDamage(int damageAmount)
    {
        Debug.Log(gameObject.name + " received " + damageAmount + " damage.");

        shotCooldown = 1f;
    }
    public void OnShove(float forceAmount, Vector3 direction)
    {
        Debug.Log(gameObject.name + " was shoved.");
        
        if (npcAnimator != null)
        {
            npcAnimator.SetTrigger("shoved");
        }
    }
    // Update is called once per frame

}
