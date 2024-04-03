using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Text : MonoBehaviour
{
    // Referencias a los objetos GameObject que representan a los personajes masculino y femenino
    public GameObject MenCharacter;
    public GameObject WomenCharacter;

    // Variables booleanas que indican si se ha seleccionado el personaje masculino o femenino
    public bool Men;
    public bool Women;

    // Referencia al componente TextMeshPro para mostrar el diálogo
    public TMP_Text dialogueText;

    // Arreglo de strings que contiene las líneas de diálogo
    private string[] lines =
    {
        "Bienvenido a Sena Racer",
        "Aquí podrás conocer las instalaciones del CBA de una forma divertida",
        "Escanea los QR mediante la cámara",
        "Aparecerá una pequeña información y una pregunta",
        "Si contestas bien y rápido, ¡más puntos tendrás!",
        "¿Estás listo para iniciar este juego? ¡Comencemos!"
    };

    // Componente AudioSource para reproducir audio
    public AudioSource audioSource;

    // Arreglos de clips de audio para los personajes masculino y femenino
    public AudioClip[] clipsMen;
    public AudioClip[] clipsWomen;

    // Velocidad de escritura del texto
    public float textSpeed = 0.2f;

    // Índice que representa la línea actual en el diálogo
    private int index;

    // Booleanos que controlan la reproducción de audio y si el texto se ha mostrado completamente
    private bool audioPlaying = false;
    private bool textFullyDisplayed = false;
    

    // Método llamado al inicio de la ejecución del script
    void Start()
    {
        // Obtiene el valor almacenado en PlayerPrefs que indica si se ha seleccionado el personaje masculino o femenino
        Men = PlayerPrefs.GetInt("MenSelect") == 1;
        Women = PlayerPrefs.GetInt("WomenSelect") == 1;

        // Inicializa el texto del diálogo y obtiene la referencia al componente AudioSource del personaje correspondiente
        dialogueText.text = "";
        if (Men == true) 
        {
            audioSource = MenCharacter.GetComponent<AudioSource>();
        }
        if (Women == true)
        {
            audioSource = WomenCharacter.GetComponent<AudioSource>();
        }

        // Detiene la reproducción de audio al inicio
        audioSource.Stop();

        // Inicia el diálogo
        StartDialogue();
    }

    // Método llamado en cada frame
    private void Update()
    {
        // Reproduce el audio correspondiente a la línea actual si no se está reproduciendo y no se ha llegado al final del diálogo
        if (!audioPlaying && !audioSource.isPlaying && index < lines.Length)
        {
            PlayAudioForLine(index);
            audioPlaying = true;
        }

        // Detecta clic del mouse para avanzar al siguiente diálogo
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])
            {
                // Si el texto no se ha mostrado completamente, detiene la escritura y muestra el texto completo
                if (!textFullyDisplayed)
                {
                    StopAllCoroutines();
                    dialogueText.text = lines[index];
                    textFullyDisplayed = true;
                    audioSource.Pause(); // Pausa el audio cuando el texto está completamente mostrado
                }
                else
                {
                    // Si el texto se ha mostrado completamente, pasa a la siguiente línea
                    NextLine();
                    textFullyDisplayed = false;
                }
            }
        }
    }

    // Inicia el diálogo
    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(WriteLine());
    }

    // Corrutina que escribe una línea de diálogo letra por letra
    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    // Pasa a la siguiente línea de diálogo
    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            audioPlaying = false;
            StartCoroutine(WriteLine());
        }
        else
        {
            // Si se ha llegado al final del diálogo, desactiva el objeto y carga la escena
            
            SceneManager.LoadScene("Loading");
        }
    }

    // Reproduce el audio correspondiente a la línea actual del diálogo
    void PlayAudioForLine(int lineIndex)
    {
        // Verifica qué personaje se ha seleccionado y reproduce el audio correspondiente
        if (Men == true)
        {
            if (lineIndex < clipsMen.Length)
            {
                audioSource.clip = clipsMen[lineIndex];
                audioSource.Play();
            }
        }
        else
        {
            if (lineIndex < clipsWomen.Length)
            {
                audioSource.clip = clipsWomen[lineIndex];
                audioSource.Play();
            }
        }
    }
}
