using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Mapbox.Examples;

public class Resultados : MonoBehaviour
{
    public TMP_Text scoreText; // Asigna el componente de texto desde el Editor Unity
    public TMP_Text timeText; // Asigna el componente de texto desde el Editor Unity

    void Start()
    {
        // Obt�n la puntuaci�n almacenada
        int finalScore = PlayerPrefs.GetInt("ScoreXimena", 0);
        // Muestra la puntuaci�n en el componente de texto
        scoreText.text = "Puntuaci�n Final: " + finalScore.ToString();

        float timeScore = PlayerPrefs.GetFloat("TimeXimena", 0);
        timeText.text = "Tiempo Final: " + timeScore.ToString("F0");


    }
    public void backMap()
    {

        SceneManager.LoadScene("Location-basedGame");

    }

}
