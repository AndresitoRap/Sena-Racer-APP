using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Aquestion : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float typingSpeed = 0.1f;
    private string fullText = "Ya conociendo un poco más sobre nuestra magnifica institución... institución...   ¿Podrías recordarme el nombre?";

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
