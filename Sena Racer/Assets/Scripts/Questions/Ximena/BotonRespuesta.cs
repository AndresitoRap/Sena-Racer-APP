using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BotonRespuesta : MonoBehaviour
{
    // Variables p�blicas para asignar desde el Editor Unity
    public Button[] answerButtons;       // Botones de respuesta
    public TMP_Text questionText;        // Texto de la pregunta
    public float delayBeforeRestart = 4f; // Tiempo de espera antes de reiniciar
    public AudioClip correctSound;       // Sonido para respuesta correcta
    public AudioClip incorrectSound;     // Sonido para respuesta incorrecta
    public TMP_Text timerText;           // Texto del cron�metro

    // Variables privadas
    private int correctAnswerIndex;      // �ndice de la respuesta correcta
    private Color correctColor = Color.green;   // Color para respuesta correcta
    private Color incorrectColor = Color.red;   // Color para respuesta incorrecta
    private Animator avatarAnimatorMen;
    private Animator avatarAnimatorWomen;
    private float timer;                 // Variable para el cron�metro
    private bool timerRunning = true;
    private Coroutine timerCoroutine;    // Referencia a la rutina del cron�metro
    private int score = 100;
    private float lastIncorrectAnswerTime;

    // Variables booleanas que indican si se ha seleccionado el avatar masculino o femenino
    public bool Men;
    public bool Women;
    public GameObject MenCharacter;
    public GameObject WomenCharacter;

    void Start()
    {
        InitializeQuiz();   // Inicia el cuestionario
        StartTimer();       // Inicia el cron�metro
    }

    // M�todo para iniciar el cron�metro
    void StartTimer()
    {
        StartCoroutine(UpdateTimer());   // Inicia la rutina del cron�metro
    }

    // M�todo llamado en cada fotograma
    void Update()
    {
        // Obtiene el valor almacenado en PlayerPrefs que indica si se ha seleccionado el avatar masculino o femenino
        Men = PlayerPrefs.GetInt("MenSelect") == 1;
        Women = PlayerPrefs.GetInt("WomenSelect") == 1;

        // Ajusta la velocidad del cron�metro seg�n tus preferencias
        float timeScale = timerRunning ? 1f : 0f;  // Cambia la velocidad seg�n si el cron�metro est� en ejecuci�n

        // Actualiza el tiempo del cron�metro solo si est� en ejecuci�n
        if (timerRunning)
        {
            timer += Time.deltaTime * timeScale;
            timerText.text = timer.ToString("F0");   // Actualiza el texto del cron�metro en formato entero
        }
    }

    // M�todo para inicializar el cuestionario
    void InitializeQuiz()
    {
        StartTimer();   // Inicia el cron�metro al inicializar el cuestionario
        QuestionData[] questions = GetRandomQuestions();   // Obtiene preguntas aleatorias

        QuestionData currentQuestion = questions[0];   // Obtiene la primera pregunta
        ShuffleAnswers(currentQuestion.answers);   // Mezcla las respuestas

        DisplayQuestion(currentQuestion);   // Muestra la pregunta y respuestas en los botones

        // Configura el comportamiento de los botones
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int buttonIndex = i;
            answerButtons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    // Rutina para actualizar el cron�metro
    IEnumerator UpdateTimer()
    {
        while (true)
        {
            timer += Time.deltaTime;
            yield return null;

            // Agrega la siguiente verificaci�n dentro del bucle
            if (timer >= 0) // Ajusta seg�n tu l�gica de tiempo l�mite
            {
                // Det�n el cron�metro y sal del bucle
                Debug.Log("Cron�metro detenido");
                yield break;
            }
        }
    }

    // M�todo para mezclar las respuestas utilizando el algoritmo Fisher-Yates
    void ShuffleAnswers(string[] answers)
    {
        for (int i = answers.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            string temp = answers[i];
            answers[i] = answers[j];
            answers[j] = temp;
        }
    }

    // M�todo para mostrar la pregunta y las respuestas en los botones
    void DisplayQuestion(QuestionData question)
    {
        questionText.text = question.question;   // Establece el texto de la pregunta

        // Configura los botones con las respuestas
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = question.answers[i];   // Establece el texto de respuesta en el bot�n
            answerButtons[i].GetComponent<Image>().color = Color.white;   // Restaura el color original del bot�n
            answerButtons[i].interactable = true;   // Habilita los clics nuevamente
        }

        // Configura el �ndice de la respuesta correcta en las respuestas mezcladas
        correctAnswerIndex = System.Array.IndexOf(question.answers, question.correctAnswer);
    }

    // M�todo llamado al hacer clic en un bot�n de respuesta
    void OnButtonClick(int buttonIndex)
    {
        // Desactiva la interactividad de todos los botones
        foreach (var button in answerButtons)
        {
            button.interactable = false;
        }

        // Verifica si la respuesta es correcta
        if (buttonIndex == correctAnswerIndex)
        {
            // Respuesta correcta
            SetButtonColor(answerButtons[buttonIndex], correctColor);
            PlaySound(correctSound);

            // Verifica si se ha seleccionado el avatar masculino
            if (Men && MenCharacter != null)
            {
                avatarAnimatorMen = MenCharacter.GetComponent<Animator>();
                if (avatarAnimatorMen != null)
                {
                    // Ejecuta la animaci�n "Correcta" en el avatar
                    avatarAnimatorMen.SetTrigger("Correct");
                }

                // Destruye el objeto del avatar femenino
                Destroy(WomenCharacter);
            }

            // Verifica si se ha seleccionado el avatar femenino
            if (Women && WomenCharacter != null)
            {
                avatarAnimatorWomen = WomenCharacter.GetComponent<Animator>();
                if (avatarAnimatorWomen != null)
                {
                    // Ejecuta la animaci�n "Correcta" en el avatar
                    avatarAnimatorWomen.SetTrigger("Correct");
                }

                // Destruye el objeto del avatar masculino
                Destroy(MenCharacter);
            }

            // Det�n el cron�metro solo en caso de respuesta correcta
            timerRunning = false;

            // Realiza acciones espec�ficas para la respuesta correcta despu�s de un retraso de 3 segundos
            Invoke("LoadNextScene", 3f);
        }
        else
        {
            // Respuesta incorrecta
            SetButtonColor(answerButtons[buttonIndex], incorrectColor);
            PlaySound(incorrectSound);

            // Ejecuta la animaci�n "Incorrecta" en el avatar
            if (Men && MenCharacter != null)
            {
                avatarAnimatorMen = MenCharacter.GetComponent<Animator>();
                if (avatarAnimatorMen != null)
                {
                    avatarAnimatorMen.SetTrigger("Incorrect");
                }
            }

            if (Women && WomenCharacter != null)
            {
                avatarAnimatorWomen = WomenCharacter.GetComponent<Animator>();
                if (avatarAnimatorWomen != null)
                {
                    avatarAnimatorWomen.SetTrigger("Incorrect");
                }
            }

            Debug.Log("Respuesta incorrecta");

            // Realiza acciones espec�ficas para la respuesta incorrecta despu�s de un retraso
            Invoke("RestartButtons", delayBeforeRestart);
        }
    }

    // M�todo para cargar la siguiente escena
    void LoadNextScene()
    {
        // Pasa la puntuaci�n a la siguiente escena
        score -= Mathf.RoundToInt(timer);
        // Aseg�rate de que la puntuaci�n no sea negativa
        if (score < 0)
        {
            score = 0;
        }
        PlayerPrefs.SetInt("ScoreXimena", score);
        PlayerPrefs.SetFloat("TimeXimena", timer);
        PlayerPrefs.Save();

        // Carga la escena llamada "Results"
        SceneManager.LoadScene("Results");
    }

    // M�todo para reiniciar la interactividad y el color de los botones
    void RestartButtons()
    {
        // Reinicia el cron�metro
        timerRunning = true;
        // Habilita la interactividad y restablece el color de los botones
        foreach (var button in answerButtons)
        {
            button.interactable = true;
            button.GetComponent<Image>().color = Color.white;
        }

        // Aseg�rate de tener una nueva pregunta y respuestas aqu�  
        InitializeQuiz();
    }

    // M�todo para configurar el color de un bot�n y deshabilitar los clics
    void SetButtonColor(Button button, Color color)
    {
        // Configura el color del bot�n y deshabilita los clics
        button.GetComponent<Image>().color = color;
        button.interactable = false;
    }

    // M�todo para reproducir un sonido proporcionado
    void PlaySound(AudioClip sound)
    {
        // Reproduce el sonido en la posici�n de la c�mara principal
        if (sound != null)
        {
            AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
        }
    }

    // M�todo para obtener preguntas aleatorias
    QuestionData[] GetRandomQuestions()
    {
        // Aqu� debes tener tu l�gica para obtener preguntas aleatorias
        // Retorna un array de preguntas (cada pregunta debe ser un objeto que contenga la pregunta, respuestas y la respuesta correcta)
        return new QuestionData[]
        {
            new QuestionData("�Cu�les curiosidades son correctas?", new string[] { "Son timidos y saltarines", "Son limpios y se comen sus heces", "Miden 2 metros y son dormilones", "Son veloces y comen carne" }, "Son limpios y se comen sus heces"),
            // Agrega m�s preguntas seg�n tus necesidades
        };
    }
}

// Clase que representa la estructura de datos de una pregunta
[System.Serializable]
public class QuestionData
{
    public string question;        // Texto de la pregunta
    public string[] answers;       // Array de respuestas posibles
    public string correctAnswer;   // Respuesta correcta

    // Constructor para inicializar una pregunta con su texto, respuestas y respuesta correcta
    public QuestionData(string question, string[] answers, string correctAnswer)
    {
        this.question = question;
        this.answers = answers;
        this.correctAnswer = correctAnswer;
    }
}
