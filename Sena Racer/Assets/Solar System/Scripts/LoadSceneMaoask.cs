using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMaoask : MonoBehaviour
{
    // Este método se llama cuando el objeto 3D es tocado
    private void OnMouseDown()
    {
        // Cambia a la escena "MaoAsks"
        SceneManager.LoadScene("MExplicationPorcinos");
    }
}
