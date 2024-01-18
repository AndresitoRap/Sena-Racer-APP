using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    // La distancia máxima para activar eventos
    public int maxDistance = 70;

    // Método llamado al inicio del script
    void Start()
    {
        // Puedes agregar lógica de inicialización aquí si es necesario
    }

    // Método llamado en cada fotograma
    void Update()
    {
        // Puedes agregar lógica de actualización aquí si es necesario
    }

    // Método para activar eventos basado en un identificador de evento
    public void ActivateEvent(int eventID)
    {
        // Verifica el identificador del evento y realiza la acción correspondiente
        if (eventID == 1)
        {
            // Cargar la escena "FirstScene" cuando el identificador del evento es 1
            SceneManager.LoadScene("AExplication");
        }
        // else if (eventID == 2)
        // {
        //     // Cargar la escena "SecondScene" cuando el identificador del evento es 2
        //     SceneManager.LoadScene("Escena Mauricio, diego o ximena");
        // }
        // else if (eventID == 3)
        // {
        //     // Cargar la escena "SecondScene" cuando el identificador del evento es 2
        //     SceneManager.LoadScene("Escena Mauricio, diego o ximena");
        // }
        // else if (eventID == 4)
        // {
        //     // Cargar la escena "SecondScene" cuando el identificador del evento es 2
        //     SceneManager.LoadScene("Escena Mauricio, diego o ximena");
        // }

        PlayerPrefs.SetInt("EventCompleted_" + eventID, 1);
        PlayerPrefs.Save();
        // Actualiza el progreso después de completar el evento
        Progress.Instance.UpdateProgressAfterCompletion();
        // Puedes agregar más condiciones según sea necesario para otros identificadores de eventos
    }
}