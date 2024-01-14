using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneAvatar : MonoBehaviour
{
     // Referencia al objeto Functions que tiene el script ChooseAvatar adjunto
    public GameObject Functions;

    // Método público que cambia la escena y llama al método Save en el script ChooseAvatar
    public void ChangeScene()
    {
        // Carga la escena con el nombre "My Name"
        SceneManager.LoadScene("My Name");

        // Obtiene la referencia al script ChooseAvatar y llama al método Save()
        Functions.GetComponent<ChooseAvatar>().Save();
    }
}
