using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Mapbox.Examples;

public class Results : MonoBehaviour
{
    public TMP_Text scoreText; // Asigna el componente de texto desde el Editor Unity
    public TMP_Text timeText; // Asigna el componente de texto desde el Editor Unity

    void Start()
    {
        // Obtén la puntuación almacenada
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        // Muestra la puntuación en el componente de texto
        scoreText.text = "Puntuación Final: " + finalScore.ToString();

        float timeScore = PlayerPrefs.GetFloat("FinalTime", 0);
        timeText.text = "Tiempo Final: " + timeScore.ToString("F0");

        
    }
    public void backMap() {  
        
        SceneManager.LoadScene("Location-basedGame");
        
    }

}
