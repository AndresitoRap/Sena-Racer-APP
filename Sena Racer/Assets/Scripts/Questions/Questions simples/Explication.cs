
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
        if (sceneName == "XExplication")
        {
            fullText = "Los conejos son animalitos muy curiosos, se comen sus heces, son aventureros, son muy limpios, comen zanahorias y muchas más";
            nextSceneButton.onClick.AddListener(() => SceneManager.LoadScene("XQuestion"));
            StartCoroutine(TypeText());
        }
        else if (sceneName == "AExplication")
        {
            fullText = "El Servicio Nacional de Aprendizaje es un establecimiento público de educación en Colombia que ofrece formación gratuita con programas técnicos, tecnológicos y complementarios.";
            nextSceneButton.onClick.AddListener(() => SceneManager.LoadScene("AQuestion"));
            StartCoroutine(TypeText());
        }
        else if (sceneName == "DExplication")
        {
            fullText = "Las vacas son mamíferos rumiantes domesticados, que pertenecen a la especie Bos taurus. Son una de las especies de ganado más comunes en todo el mundo y han sido criadas por humanos durante miles de años por su carne, leche, cuero y otros subproductos.";
            nextSceneButton.onClick.AddListener(() => SceneManager.LoadScene("DQuestion"));
            StartCoroutine(TypeText());
        }
        else if (sceneName == "MExplicationBee")
        {
            fullText = "Las abejas son conocidas por su papel en la polinización y por producir miel y cera. Sin embargo, no tienen un impacto directo en la mejora de la calidad del aire.";
            nextSceneButton.onClick.AddListener(() => SceneManager.LoadScene("MQuestionBee"));
            StartCoroutine(TypeText());
        }

        else if (sceneName == "MExplicationPorcinos")
        {
            StartCoroutine(ShowTexts());
        }
        else
        {
            StartCoroutine(TypeText());
        }
    }

    IEnumerator ShowTexts()
    {
        // Muestra el primer texto
        fullText = "Los porcinos, también conocidos como cerdos, son una subespecie de mamífero que fueron domesticados hace unos 13,000 años en el Oriente Próximo y en China. Son omnívoros y conocidos por su rápido crecimiento, lo que los hace populares para la crianza con fines cárnicos.";
        yield return StartCoroutine(TypeText());

        // Espera 5 segundos (o el tiempo que quieras)
        yield return new WaitForSeconds(1);

        // Limpia el texto existente
        textMeshPro.text = "";

        // Muestra el segundo texto
        fullText = "En este juego desliza todas las tarjetas. Si deslizas la correcta a la derecha, ganas. Pero si deslizas una incorrecta antes de terminar, el juego no será válido y se reiniciará una vez que todas las tarjetas hayan sido movidas.";
        yield return StartCoroutine(TypeText());

        // Después de que se haya completado la escritura del segundo texto, activa el botón
        nextSceneButton.gameObject.SetActive(true);

        // Agrega una acción al botón para cargar la siguiente escena
        nextSceneButton.onClick.AddListener(() => SceneManager.LoadScene("MaoAsks"));

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
