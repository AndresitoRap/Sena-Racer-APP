using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAndyBee : MonoBehaviour
{
   
    // Este método se llama cuando el objeto 3D es tocado
    private void OnMouseDown()
    {
        // Cambia a la escena "AExplication"
        SceneManager.LoadScene("MExplicationBee");
    }


}
