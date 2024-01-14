using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Login : MonoBehaviour
{

    // Referencias a los campos de entrada de texto y texto para mostrar mensajes de error
    public TMP_InputField ID;
    public TMP_InputField Password;
    public TMP_Text MessageError;

    // Método público para iniciar sesión y cambiar de escena
    public void Log_In() {

         // Verifica si alguno de los campos de entrada de texto está vacío
        if (string.IsNullOrEmpty(ID.text) || string.IsNullOrEmpty(Password.text))
        {
             // Muestra un mensaje de error si algún campo está vacío
            MessageError.text = "DEBES LLENAR TODOS LOS CAMPOS";
        }
        else {
            // Carga la escena especificada si ambos campos están completos
            SceneManager.LoadScene("Welcome");
        }
    }
}
