using UnityEngine;
using System.Collections.Generic;


public class MasqueradeGenerator : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
 [SerializeField] private int[] existingMasqIds;
    public GameObject masqueradeGuestPrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }
    
    public void GenerateqGuests()
    {
        existingMasqIds = new int[spawnPoints.Length];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            
            int newMasqId = GenerateRandomMasqueradeId();
            existingMasqIds[i] = newMasqId;
            
            GameObject newGuest = Instantiate(masqueradeGuestPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
            MasqueradeGuest guestScript = newGuest.GetComponent<MasqueradeGuest>();
            guestScript.guestId = newMasqId;
            guestScript.DeclareId();
        }
    }
    public int GetTargetMasqueradeId()
    {
        
        int index = Random.Range(0, existingMasqIds.Length -1);
        
        return existingMasqIds[index];
    }
   int GenerateRandomMasqueradeId()
    {
        int randId = Random.Range(1000, 9999);
        while (System.Array.Exists(existingMasqIds, element => element == randId))
        {
            randId = Random.Range(1000, 9999);
        }

        for (int i = 0; i < existingMasqIds.Length -1; i++)
        {
            if (randId == existingMasqIds[i])
            {

              GenerateRandomMasqueradeId();
            }   
        }
        return randId;
    }

}
