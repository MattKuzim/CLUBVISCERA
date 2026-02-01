using UnityEngine;
using TMPro;

public class TimerCountDown : MonoBehaviour
{
    [SerializeField] private float startTime = 60f;
    [SerializeField] private TMP_Text timerText = null;
    [SerializeField] private bool startOnAwake = true;
    [SerializeField] private string timeFormat = "F2"; // F2 = two decimal places
    
    [SerializeField] VisceraGameManager gameManager;

    private float timeLeft;
    private bool running;

    void Awake()
    {
        timeLeft = Mathf.Max(0f, startTime);
        running = startOnAwake;
        UpdateText();
    }

    void Update()
    {
        if (!running) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            
            gameManager.Lose();
            running = false;
            // Timer reached zero — additional logic can be added here
        }

        UpdateText();
        
    }

    private void UpdateText()
    {
        if (timerText != null)
            timerText.text = timeLeft.ToString(timeFormat);
    }

    // Public control methods
    public void AddTime(float seconds)
    {
        timeLeft = Mathf.Max(0f, timeLeft + seconds);
        UpdateText();
    }

    public void RemoveTime(float seconds)
    {
        timeLeft = Mathf.Max(0f, timeLeft - seconds);
        UpdateText();
    }

    public void SetTime(float seconds)
    {
        timeLeft = Mathf.Max(0f, seconds);
        UpdateText();
    }

    public float GetTime()
    {
        return timeLeft;
    }

    public void StartTimer()
    {
        if (timeLeft > 0f) running = true;
    }

    public void PauseTimer()
    {
        running = false;
    }

    public bool IsRunning()
    {
        return running;
    }
}