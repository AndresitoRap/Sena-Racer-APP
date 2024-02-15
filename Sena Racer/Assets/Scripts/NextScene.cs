using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    public void LoadNextScene(string nameScene) {

        // Carga la escena 
        SceneManager.LoadScene(nameScene);
    }
}
