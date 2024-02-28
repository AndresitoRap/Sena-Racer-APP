using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pregunta : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float typingSpeed = 0.1f;
    private string fullText = "Recuerda responder en el menor tiempo posible para obtener más puntos ¡Suerte!";

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
    }
}

