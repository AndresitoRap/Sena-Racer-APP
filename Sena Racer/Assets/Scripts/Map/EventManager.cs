using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    // La distancia máxima para activar eventos
    public int maxDistance = 70;

    // Método para activar eventos basado en un identificador de evento
    public void ActivateEvent(int eventID)
    {
        // Verifica el identificador del evento y realiza la acción correspondiente
        if (eventID == 1)
        {
            // Cargar la escena "FirstScene" cuando el identificador del evento es 1
            SceneManager.LoadScene("Vuforia");
        }
        else if (eventID == 2)
        {
            //Cargar la escena "FirstScene" cuando el identificador del evento es 2
            SceneManager.LoadScene("Vuforia");
        }
    
    }
}