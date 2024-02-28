using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Explicacion : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float typingSpeed = 0.05f; // Reducido el tiempo para que sea m�s f�cil de seguir
    private string fullText = "Los conejos son animalitos muy curiosos, se comen sus heces, son aventureros, son muy limpios, comen zanahorias y muchas m�s";
    public Button nextSceneButton;

    void Start()
    {
        if (textMeshPro == null || nextSceneButton == null)
        {
            Debug.LogError("Error: Aseg�rate de asignar todos los objetos en el Inspector.");
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
 
        // Despu�s de que se haya completado la escritura del texto, activa el bot�n
        nextSceneButton.gameObject.SetActive(true);
    }

    // Funci�n para cargar la siguiente escena
    public void LoadNextScene()
    {
        SceneManager.LoadScene("Question");
    }
}
