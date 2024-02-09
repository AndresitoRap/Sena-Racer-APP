using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAndyask : MonoBehaviour
{
   
    // Este método se llama cuando el objeto 3D es tocado
    private void OnMouseDown()
    {
        // Cambia a la escena "AExplication"
        SceneManager.LoadScene("AExplication");
    }


}
