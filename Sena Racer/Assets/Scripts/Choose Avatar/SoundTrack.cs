using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SoundTrack : MonoBehaviour
{
   void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Reemplaza 'NombreTerceraEscena' con el nombre real de tu tercera escena
        if (SceneManager.GetActiveScene().name == "Start")
        {
            Destroy(gameObject);
        }
    }
}
