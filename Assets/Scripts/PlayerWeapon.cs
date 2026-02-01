using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Animator bobberAnimator;
    [SerializeField] private Animator gunAnimator;

    [SerializeField] VisceraGameManager gameManager;
 
    public float range = 50f;

    void Start()
    {
        Debug.Log("Weapon Active");
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
            Debug.Log("Ray Casted");

            if (gunAnimator != null && bobberAnimator != null)
            {
                gunAnimator.SetBool("firing", true);
                bobberAnimator.SetBool("firing", true);
            
            }
            else
            {
                Debug.LogWarning("Animators not assigned in PlayerWeapon script.");
            }
            
        }

       else{
            if (gunAnimator != null && bobberAnimator != null)
            {
                gunAnimator.SetBool("firing", false);
                bobberAnimator.SetBool("firing", false);
           
            }
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        // direction for raycast
        Vector3 fwd = transform.forward;

        if (Physics.Raycast(Camera.main.transform.position, fwd, out hit, range))
        {
            
            if(hit.transform.GetComponent<IReceiveDamage>() != null)
            {
                IReceiveDamage damageable = hit.transform.GetComponent<IReceiveDamage>();
                
                MasqueradeGuest guest = hit.transform.GetComponent<MasqueradeGuest>();
                if (guest != null)
                {
                    gameManager.CheckIfTargetKilled(guest.guestId);
                }
                
                
                damageable.ReceiveDamage(10);
            }
            Debug.Log("Hit: " + hit.transform.name);
        }
    }
}
