using UnityEngine;
using UnityEngine.SceneManagement;
 
public class SceneLoader : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
 
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
 
    public void QuitGame()
    {
        Application.Quit();
        // This lets us see that the button works in the Unity Editor.
        Debug.Log("Quit Game");
    }
}