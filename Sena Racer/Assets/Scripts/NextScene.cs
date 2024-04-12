using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    public void LoadNextScene(string Ranking) {

        // Carga la escena 
        SceneManager.LoadScene(Ranking);
    }
}
