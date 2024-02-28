using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Explicacion : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float typingSpeed = 0.05f; // Reducido el tiempo para que sea más fácil de seguir
    private string fullText = "Los conejos son animalitos muy curiosos, se comen sus heces, son aventureros, son muy limpios, comen zanahorias y muchas más";
    public Button nextSceneButton;

    void Start()
    {
        if (textMeshPro == null || nextSceneButton == null)
        {
            Debug.LogError("Error: Asegúrate de asignar todos los objetos en el Inspector.");
            return;
        }

        // Establecer el texto completo al inicio
        textMeshPro.text = fullText;
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        // Vaciar el texto antes de comenzar a escribir
        textMeshPro.text = "";

        // Escribir el texto letra por letra con un retraso
        foreach (char c in fullText)
        {
            textMeshPro.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
 
        // Después de que se haya completado la escritura del texto, activa el botón
        nextSceneButton.gameObject.SetActive(true);
    }

    // Función para cargar la siguiente escena
    public void LoadNextScene()
    {
        SceneManager.LoadScene("Question");
    }
}
