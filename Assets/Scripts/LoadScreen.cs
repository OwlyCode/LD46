using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    // RÃ©fÃ©rences aux objets
    public GameObject LoadingScreen;
    public Slider slider;
    public Text progressText;

    // Fonction de chargement de niveau appelÃ© via un bouton
    public void LoadLevel(int sceneIndex)
    {
        // DÃ©marrage de la coroutine
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    public void DoExit()
    {
        print("in");
        Application.Quit();
    }
    // Coroutine permettant de charger la scÃ¨ne et de mettre Ã  jour le UI en fonction de la progression
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        // Chargement de la scÃ¨ne en background
        // On stock cette ligne dans operation pour accÃ©der au pourcentage de chargement
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        // Activation de l'Ã©cran de chargement
        LoadingScreen.SetActive(true);

        // Tant que le chargement n'est pas terminÃ© on lit cette boucle
        while (!operation.isDone)
        {
            // On recupÃ¨re la progression, Clamp01 bloque la valeur entre 0 et 1
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // Attribution de la nouvelle valeur au slider
            slider.value = progress;

            // Mise Ã  jour du pourcentage de chargement
            progressText.text = progress * 100 + "%";

            // On attend un frame puis on recommance la boucle
            yield return null;
        }
    }

}

