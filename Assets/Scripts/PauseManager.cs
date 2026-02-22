using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public bool isPaused=false;

    public MonoBehaviour playerScript;
    public MonoBehaviour weaponScript;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale=0f;
        isPaused=true;
        weaponScript.enabled=false;
        playerScript.enabled=false;
    }

     public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale=1f;
        isPaused=false;
        weaponScript.enabled=true;
        playerScript.enabled=true;
    }

    public void QuitToMainMenu()
    {
        Debug.Log("eeeeeeee");
        Time.timeScale=1f;
        SceneManager.LoadScene("MainMenu");
    }
}
