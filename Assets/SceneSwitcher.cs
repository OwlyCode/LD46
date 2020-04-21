using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
    
public class SceneSwitcher : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("World");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Title()
    {
        SceneManager.LoadScene("Title");
    }
    public void EndGame()
    {
        SceneManager.LoadScene("EndGame");
    }

    public void Quit() {
        Application.Quit();
    }
}
