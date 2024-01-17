using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
    public TMP_Text textProgress;
    public Slider sliderProgress;
    public float currentPercent;
    private float loadingTime = 3.0f;

    void Start() {
        StartCoroutine(LoadScene("GeospatialArf5"));
    }

    public IEnumerator LoadScene(string nameToLoad) {
        float elapsedTime = 0;
        textProgress.text = "Cargando 00%";
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(nameToLoad);
        loadAsync.allowSceneActivation = false;

        while (elapsedTime < loadingTime)
        {
            elapsedTime += Time.deltaTime;
            currentPercent = Mathf.Clamp01(elapsedTime / loadingTime) * 100;
            textProgress.text = "Cargando " + currentPercent.ToString("00")+"%";

            if (currentPercent >= 100)
            {
                loadAsync.allowSceneActivation = true;
            }
            yield return null;
        }
    } 

    private void Update () {
        sliderProgress.value = Mathf.MoveTowards(sliderProgress.value, currentPercent, 33.3f * Time.deltaTime);
    }
}