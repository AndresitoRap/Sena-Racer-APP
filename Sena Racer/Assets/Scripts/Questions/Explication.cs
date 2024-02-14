using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Explication : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float typingSpeed = 0.1f;
    private string fullText;
    public Button nextSceneButton;

    void Start()
    {
        // Obtiene el nombre de la escena activa
        string sceneName = SceneManager.GetActiveScene().name;

        // Selecciona el texto y la siguiente escena basándose en el nombre de la escena
        if (sceneName == "AExplication")
        {
            fullText = "El Servicio Nacional de Aprendizaje es un establecimiento público de educación en Colombia que ofrece formación gratuita con programas técnicos, tecnológicos y complementarios.";
            nextSceneButton.onClick.AddListener(() => SceneManager.LoadScene("AQuestion"));
        }
        else if (sceneName == "MExplication")
        {
            fullText = "Los porcinos son mamíferos de la familia Suidae que se crían para la carne. Fueron domesticados hace unos 13,000 años en el Oriente Próximo y en China. Son omnívoros y comen de todo.";
            nextSceneButton.onClick.AddListener(() => SceneManager.LoadScene("MQuestion"));
        }

        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char c in fullText)
        {
            textMeshPro.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Después de que se haya completado la escritura del texto, activa el botón
        nextSceneButton.gameObject.SetActive(true);
    }
}
