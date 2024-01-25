using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneVF : MonoBehaviour
{
    // Nombre de la escena para cargar
    public string sceneToLoad;

    // Método que se llama cuando se toca el prefab
    void OnMouseDown()
    {
        // Cambiar a la escena especificada
        SceneManager.LoadScene(sceneToLoad);
    }
}
