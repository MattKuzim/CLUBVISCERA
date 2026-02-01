using UnityEngine;
using TMPro;
public class MasqueradeGuest : MonoBehaviour
{

[SerializeField] bool debugMode = true;
    
[SerializeField] TMP_Text guestNameTag;
    public int guestId = 0000;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnAwake()
    {
        Debug.Log("awake");
        DeclareId();
    }
    
    public void DeclareId()
    {
        if (debugMode)
        {
            Debug.Log("Guest ID is: " + guestId);
            guestNameTag.text = "Guest ID: " + guestId;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
