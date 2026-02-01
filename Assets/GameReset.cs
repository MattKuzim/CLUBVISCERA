using UnityEngine;
using UnityEngine.SceneManagement;
public class GameReset : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnEnable()
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
   
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
