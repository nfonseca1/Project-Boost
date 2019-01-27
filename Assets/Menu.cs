using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image loadingScreen;

    Text loadingText;
    float minLoadBrightness = 50;
    float maxLoadBrightness = 256;
    bool gameStarted = false;
    AsyncOperation AO;

    const float tau = Mathf.PI * 2;

    private void Update()
    {
        button.onClick.AddListener(LoadScene);
        if (gameStarted)
        {
            SetLoadingScreen();
        }
    }

    private void LoadScene()
    {
        gameStarted = true;
        loadingScreen.gameObject.SetActive(true);
        loadingText = loadingScreen.GetComponentInChildren<Text>();

        AO = SceneManager.LoadSceneAsync(1);
    }

    private void SetLoadingScreen()
    {
        print("running");
        var sineWave = Mathf.Sin(Time.time * tau);
        var colorValue = Mathf.Clamp(sineWave * maxLoadBrightness, minLoadBrightness, maxLoadBrightness);
        loadingText.color = new Color(colorValue, colorValue, colorValue);
    }
}
