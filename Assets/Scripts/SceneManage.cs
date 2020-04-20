using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public string SceneToLoad;
    void Start()
    {

    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
