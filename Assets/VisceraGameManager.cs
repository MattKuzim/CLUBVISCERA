using UnityEngine;
using TMPro;
public class VisceraGameManager : MonoBehaviour
{

    [SerializeField] private Transform targetPanel;
   [SerializeField] private Transform winScreen;
   [SerializeField] private Transform loseScreen;
    [SerializeField] private TimerCountDown timer;
    private int targetId;
   [SerializeField] TMP_Text idText;
    [SerializeField] private MasqueradeGenerator masqueradeGenerator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPanel.gameObject.SetActive(true);
       masqueradeGenerator.GenerateqGuests();
      int id =  masqueradeGenerator.GetTargetMasqueradeId();
        targetId = id;
        idText.text = id.ToString();
        

    }

    public void Lose()
    {
        
        loseScreen.gameObject.SetActive(true);
        timer.PauseTimer();
        
    }
    public void CheckIfTargetKilled(int id)
    {
        
        if (id == targetId)
        {
            Debug.Log("Target Eliminated! You Win!");
            winScreen.gameObject.SetActive(true);
            timer.PauseTimer();
           
        }
        else
        {
            Debug.Log("Wrong Target! You Lose!");
            loseScreen.gameObject.SetActive(true);
            timer.PauseTimer();
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
