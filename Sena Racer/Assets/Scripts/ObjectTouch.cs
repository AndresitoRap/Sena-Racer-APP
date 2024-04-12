using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTouch : MonoBehaviour
{
    // Nombre de la escena en la que el objeto 3D debe estar para que el progreso aumente
    private string requiredSceneName = "GeospatialArf5";

    void OnMouseDown()
    {
        // Verifica si la escena actual es la escena requerida
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == requiredSceneName)
        {
            // Recupera el progreso actual
            int visitedCount = PlayerPrefs.GetInt("VisitedCount", 0);
            int totalCount = PlayerPrefs.GetInt("TotalCount", 0);

            // Solo aumenta el progreso si no se ha alcanzado el total
            if (visitedCount < totalCount)
            {
                // Incrementa el contador de visitas
                visitedCount++;

                // Guarda el progreso en PlayerPrefs
                PlayerPrefs.SetInt("VisitedCount", visitedCount);
            }
        }
    }
}
