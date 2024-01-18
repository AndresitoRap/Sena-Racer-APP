using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Explication : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float typingSpeed = 0.1f;
    private string fullText = "El Servicio Nacional de Aprendizaje es un establecimiento público de educación en Colombia que ofrece formación gratuita con programas técnicos, tecnológicos y complementarios.";
    public Button nextSceneButton;

    void Start()
    {
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

    public void nextScene() {
        SceneManager.LoadScene("AQuestion");
    }
}
