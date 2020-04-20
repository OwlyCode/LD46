using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public void Reboot(float delay = 0f)
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name, delay));
    }

    public void Victory(float delay = 0f)
    {
        StartCoroutine(LoadLevel("Credits", 0f));
    }

    IEnumerator LoadLevel (string level, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(level);
    }
}
