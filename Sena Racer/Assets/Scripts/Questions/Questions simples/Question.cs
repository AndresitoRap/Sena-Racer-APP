using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Question : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float typingSpeed = 0.1f;
    private string fullText;

    void Start()
    {
        // Obtiene el nombre de la escena activa
        string sceneName = SceneManager.GetActiveScene().name;

        // Selecciona la pregunta basándose en el nombre de la escena
        if (sceneName == "AQuestion")
        {
            fullText = "Ya conociendo un poco más sobre nuestra magnifica institución... institución... ¿Podrías recordarme el nombre?";
        }
        else if (sceneName == "MQuestionBee")
        {
            fullText = "Ya conociendo un poco más sobre la apicultura... ¿Cuál de los siguientes NO es un beneficio de la apicultura?";
        }
        if (sceneName == "XQuestion")
        {
            fullText = "Recuerda responder en el menor tiempo posible para obtener más puntos ¡Suerte!";
        }
        else if (sceneName == "DQuestion")
        {
            fullText = "Teniendo en cuenta la anterior información, responde la pregunta";
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
    }
}
