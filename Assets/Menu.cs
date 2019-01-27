using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] Image loadingScreen;
    [SerializeField] Slider slider;

    Text loadingText;

    public void StartLoad(int sceneIndex)
    {
        StartCoroutine(LoadLevel(sceneIndex));
    }

    private IEnumerator LoadLevel(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.gameObject.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            
            yield return null;
        }
    }
}
