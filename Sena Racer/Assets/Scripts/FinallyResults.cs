using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinallyResults : MonoBehaviour
{
    public TMP_Text ScoreAndy;
    public TMP_Text TimeAndy;

    // Start is called before the first frame update
    void Start()
    {
        // Obtén la puntuación almacenada
        int _scoreAndy = PlayerPrefs.GetInt("ScoreAndy", 0);
        // Muestra la puntuación en el componente de texto
        ScoreAndy.text = _scoreAndy.ToString();

        float _timaAndy = PlayerPrefs.GetFloat("TimeAndy", 0);
        TimeAndy.text = _timaAndy.ToString("F0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
