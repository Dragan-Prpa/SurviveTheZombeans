using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
 public void StartGame()
    {
        SceneManager.LoadScene("Igra");

    }


 public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

 public void Settings()
    {
        //SceneManager.LoadScene("Settings");
    }

 public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
