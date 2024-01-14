using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Welcome : MonoBehaviour
{
    public void Continue() {

        // Carga la escena con el nombre especificado
        SceneManager.LoadScene("Choose Avatar");
    }
}
