using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MyName : MonoBehaviour
{
    // Referencias a los objetos GameObject que representan a los personajes masculino y femenino
    public GameObject MenCharacter;
    public GameObject WomenCharacter;

    // Variables booleanas que indican si se ha seleccionado el avatar masculino o femenino
    public bool Men;
    public bool Women;

    // Referencia al campo de entrada de texto para el nombre del avatar
    public TMP_InputField Name;

    // Referencia al objeto de texto para mensajes de error
    public TMP_Text MessageError;

    // Referencias a los componentes de audio para los avatares masculino y femenino
    public AudioSource MenVoice;
    public AudioSource WomenVoice;

    // Método llamado en cada frame
    private void Update()
    {
        // Obtiene el valor almacenado en PlayerPrefs que indica si se ha seleccionado el avatar masculino o femenino
        Men = PlayerPrefs.GetInt("MenSelect") == 1;
        Women = PlayerPrefs.GetInt("WomenSelect") == 1;

        // Verifica si se ha seleccionado el avatar masculino
        if (Men == true)
        {
            // Obtiene la referencia al componente AudioSource del avatar masculino y lo activa
            MenVoice = MenCharacter.GetComponent<AudioSource>();
            MenCharacter.SetActive(true);
            
            // Destruye el objeto del avatar femenino
            Destroy(WomenCharacter);
        }
        
        // Verifica si se ha seleccionado el avatar femenino
        if (Women == true)
        {
            // Obtiene la referencia al componente AudioSource del avatar femenino y lo activa
            WomenVoice = WomenCharacter.GetComponent<AudioSource>();
            WomenCharacter.SetActive(true);
            
            // Destruye el objeto del avatar masculino
            Destroy(MenCharacter);
        }
    }

    // Método público para cambiar de escena
    public void Go()
    {
        // Verifica si el campo de entrada de texto para el nombre del avatar está vacío
        if (string.IsNullOrEmpty(Name.text))
        {
            // Muestra un mensaje de error si el campo está vacío
            MessageError.text = "Debes poner un nombre válido";
            MessageError.color = Color.red;
        }
        else
        {
            // Carga la escena especificada si el campo no está vacío
            SceneManager.LoadScene("Start");
            
        }
    }
}