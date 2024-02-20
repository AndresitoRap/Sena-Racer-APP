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
    else if (sceneName == "MResultsBee") // Añade esta condición
    {
        finalScore = PlayerPrefs.GetInt("ScoreBee", 0);
        timeScore = PlayerPrefs.GetFloat("TimeBee", 0);
    }
    else if (sceneName == "MResultsporcinos")
    {
       finalScore = SwipeEffect.score; // Obtiene la puntuación de SwipeEffect
        timeScore = SwipeEffect.finalTime; // Obtiene el tiempo final de SwipeEffect 
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
