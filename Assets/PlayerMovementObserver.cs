using UnityEngine;
using TMPro;
public class PlayerMovementObserver : MonoBehaviour
{
    
    
    string currentState = "Idle";
    [SerializeField] TMP_Text stateText;

    [SerializeField] private Animator bobberAnimator;
  //  [SerializeField] private Animator gunAnimator; WE MIGHT NEED THIS LATER
    public SimpleCharacter playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        if (playerController.grounded)
        {
            if (playerController.rb.linearVelocity.magnitude > 0.1f)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    currentState = "Sprinting";
                }
                else
                {
                    currentState = "Walking";
                    if (playerController.input.x < -0.1f || playerController.input.x > 0.1f)
                    {
                        currentState = "Strafing";
                    }
                }
            }
            else
            {
                currentState = "Idle";
            }
        }
        else
        {
            currentState = "In Air";
        }

        Debug.Log("Player State: " + currentState);
        
        stateText.text = "State: " + currentState;
        bobberAnimator.SetBool("walk", false);
        bobberAnimator.SetBool("strafe", false);
        bobberAnimator.SetBool("idle", false);
        bobberAnimator.SetBool("jump", false);
        bobberAnimator.SetBool("sprint", false);
        
        switch (currentState)
        {  
              
            case "Idle":
            
              
         
                // Handle idle state
                break;
            case "Walking":
                bobberAnimator.SetBool("walk", true);
              
                // Handle walking state
                break;
            case "Sprinting":
                bobberAnimator.SetBool("sprint", true);
                // Handle sprinting state
                break;
            case "Strafing":
                bobberAnimator.SetBool("strafe", true);
                // Handle strafing state
                break;
            case "In Air":
                bobberAnimator.SetBool("jump", true);
            // Handle in air statebreak;
                break;
            default:
                Debug.Log("uh oh no state detected");

                break;
        }
    }
    
   
}
