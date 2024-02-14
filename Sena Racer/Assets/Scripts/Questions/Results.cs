using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Mapbox.Examples;

public class Results : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timeText;

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
        else if (sceneName == "MResults")
        {
            finalScore = PlayerPrefs.GetInt("ScoreMao", 0);
            timeScore = PlayerPrefs.GetFloat("TimeMao", 0);
        }

        // Muestra la puntuación y el tiempo en los componentes de texto
        scoreText.text = "Puntuación Final: " + finalScore.ToString();
        timeText.text = "Tiempo Final: " + timeScore.ToString("F0");
    }

    public void backMap()
    {
        SceneManager.LoadScene("Location-basedGame");
    }
}
