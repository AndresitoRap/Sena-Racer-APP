using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Progress : MonoBehaviour
{
    public TMP_Text progressText; // Asigna el componente de texto desde el Editor Unity
    public GameObject Finally; // Asigna el cuadro blanco desde el Editor Unity

    public void UpdateProgress(int visitedCount, int totalCount)
    {
        // Actualiza el texto del progreso en el canvas
        progressText.text = visitedCount + "/" + totalCount;

        // Guarda el progreso en PlayerPrefs
        PlayerPrefs.SetInt("VisitedCount", visitedCount);
        PlayerPrefs.SetInt("TotalCount", totalCount);

        // Verifica si todas las estaciones han sido visitadas y muestra el cuadro blanco si es necesario
        if (visitedCount == totalCount)
        {
            // Activa el cuadro blanco en el centro del canvas
            Finally.SetActive(true);
        }
    }
}
