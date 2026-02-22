using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }

    // Update is called once per frame
    void Update()
    {

    }




   public void Respawn()
    {
        SceneManager.LoadScene("Igra");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
