using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinallyResults : MonoBehaviour
{
    public TMP_Text TotalScoreAndy;
    public TMP_Text TotalTimeAndy;

    // Start is called before the first frame update
    void Start()
    {
        // Obtén la puntuación y tiempo almacenados
        int _scoreAndy = PlayerPrefs.GetInt("ScoreAndy", 0);
        float _timeAndy = PlayerPrefs.GetFloat("TimeAndy", 0);

        // Obtén los totales acumulados
        int _totalScoreAndy = PlayerPrefs.GetInt("TotalScoreAndy", 0);
        float _totalTimeAndy = PlayerPrefs.GetFloat("TotalTimeAndy", 0);

        // Suma los nuevos resultados a los totales
        _totalScoreAndy += _scoreAndy;
        _totalTimeAndy += _timeAndy;

        // Almacena los nuevos totales
        PlayerPrefs.SetInt("TotalScoreAndy", _totalScoreAndy);
        PlayerPrefs.SetFloat("TotalTimeAndy", _totalTimeAndy);

        // Muestra los totales en los componentes de texto
        TotalScoreAndy.text = _totalScoreAndy.ToString();
        TotalTimeAndy.text = _totalTimeAndy.ToString("F0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
