using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Mapbox.Examples;
using UnityEngine.Networking;

public class Results : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public static string corridorID; // Variable estática para guardar el ID del corredor

    void Start()
    {
        // Obtiene el nombre de la escena activa
        string sceneName = SceneManager.GetActiveScene().name;

        int finalScore = 0;
        float timeScore = 0;

        // Selecciona los datos de puntuación y tiempo basándose en el nombre de la escena
        if (sceneName == "AResults")
        {
            finalScore = PlayerPrefs.GetInt("ScoreAndy", 0);
            timeScore = PlayerPrefs.GetFloat("TimeAndy", 0);
        }
        else if (sceneName == "DResults")
        {
            finalScore = PlayerPrefs.GetInt("ScoreDiego", 0);
            timeScore = PlayerPrefs.GetFloat("TimeDiego", 0);
        }
        else if (sceneName == "MResultsBee")
        {
            finalScore = PlayerPrefs.GetInt("ScoreBee", 0);
            timeScore = PlayerPrefs.GetFloat("TimeBee", 0);
        }
        else if (sceneName == "XResults")
        {
            finalScore = SwipeEffect2.currentScore; // Obtiene la puntuación de SwipeEffect2
            timeScore = SwipeEffect2.elapsedTime; // Obtiene el tiempo final de SwipeEffect2 
        }
        else if (sceneName == "MResultsporcinos")
        {
            finalScore = SwipeEffect.score; // Obtiene la puntuación de SwipeEffect
            timeScore = SwipeEffect.finalTime; // Obtiene el tiempo final de SwipeEffect 
        }

        // Muestra la puntuación y el tiempo en los componentes de texto
        scoreText.text = "Puntuación Final: " + finalScore.ToString();
        timeText.text = "Tiempo Final: " + Mathf.RoundToInt(timeScore).ToString();

        // Sube los resultados a la base de datos
        StartCoroutine(UploadResults(Login.corridorID, sceneName, finalScore, Mathf.RoundToInt(timeScore)));
    }

    IEnumerator UploadResults(string runnerID, string sceneName, int finalScore, int timeScore)
    {
        // Determina a qué campos subir basándose en el nombre de la escena
        string scoreField = "";
        string timeField = "";
        if (sceneName == "AResults")
        {
            scoreField = "score5";
            timeField = "time5";
        }
        else if (sceneName == "DResults")
        {
            scoreField = "score4";
            timeField = "time4";
        }
        else if (sceneName == "MResultsBee")
        {
            scoreField = "score2";
            timeField = "time2";
        }
        else if (sceneName == "XResults")
        {
            scoreField = "score1";
            timeField = "time1";
        }
        else if (sceneName == "MResultsporcinos")
        {
            scoreField = "score3";
            timeField = "time3";
        }

        // Crea un objeto JSON con los resultados
        string json = "{\"data\": {\"" + scoreField + "\":" + finalScore + ",\"" + timeField + "\":" + timeScore + "}}";

        Debug.Log("JSON to send: " + json); // Imprime el objeto JSON para depuración

        // Crea la solicitud
        string url = "https://backend-strapi-senaracer.onrender.com/api/runners/" + runnerID;
        Debug.Log("URL to send the request: " + url); // Imprime la URL en la consola
        UnityWebRequest www = new UnityWebRequest(url, "PUT");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // Envía la solicitud y espera la respuesta
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            Debug.Log("Server response: " + www.downloadHandler.text); // Imprime la respuesta del servidor para depuración
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }

    public void backMap()
    {
        SceneManager.LoadScene("Location-basedGame");
    }
}
