using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Examples;
using UnityEngine.SceneManagement;

public class Progress : MonoBehaviour
{     // Referencia al componente Image de la barra de progreso
    public TMP_Text progressText;      // Referencia al componente TextMeshPro para mostrar el progreso
    private int totalStations;    // Número total de estaciones
    private int completedStations = 0; // Número de estaciones completadas
    public GameObject completionImage; 

    // Singleton
    public static Progress Instance { get; private set; }


     private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(progressText.gameObject); // Solo mantenemos el GameObject del texto persistente
            DontDestroyOnLoad(completionImage.gameObject);
        }
        else
        {
            Destroy(gameObject); // Destruimos cualquier instancia adicional del script
        }
    }

    void Start()
    {
        // Inicializa el número total de estaciones
        totalStations = GameObject.FindObjectOfType<SpawnOnMap>().GetTotalStations();
        
        // Asegúrate de cargar el progreso guardado al iniciar
        UpdateProgress(PlayerPrefs.GetInt("CompletedStations", 0));

        CheckAndShowCompletionImage();
    }

    private void CheckAndShowCompletionImage()
    {
        if (completedStations == totalStations)
        {
            // Mostrar la imagen de finalización
            completionImage.SetActive(true);
        }
        else{
            completionImage.SetActive(false);
        }
    }

    public void UpdateProgress(int completed)
    {
        completedStations = completed;
        progressText.text = $"{completedStations}/{totalStations}"; // Actualiza el texto del progreso

        // Guarda el número de estaciones completadas en PlayerPrefs
        PlayerPrefs.SetInt("CompletedStations", completedStations);
        PlayerPrefs.Save();

        // Verificar si todas las estaciones se han completado
        CheckAndShowCompletionImage();
    }

    public int GetTotalStations()
    {
        return totalStations;
    }
     public void UpdateProgressAfterCompletion()
    {
        completedStations++;

        progressText.text = $"{completedStations}/{totalStations}";
        PlayerPrefs.SetInt("CompletedStations", completedStations);
        PlayerPrefs.Save();

    // Verificar si todas las estaciones se han completado
        CheckAndShowCompletionImage();
        
    }

    public void Finally() {
        SceneManager.LoadScene("Finally");
    }
    

}
