using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BotonRespuesta : MonoBehaviour
{
    // Variables públicas para asignar desde el Editor Unity
    public Button[] answerButtons;       // Botones de respuesta
    public TMP_Text questionText;        // Texto de la pregunta
    public float delayBeforeRestart = 4f; // Tiempo de espera antes de reiniciar
    public AudioClip correctSound;       // Sonido para respuesta correcta
    public AudioClip incorrectSound;     // Sonido para respuesta incorrecta
    public TMP_Text timerText;           // Texto del cronómetro

    // Variables privadas
    private int correctAnswerIndex;      // Índice de la respuesta correcta
    private Color correctColor = Color.green;   // Color para respuesta correcta
    private Color incorrectColor = Color.red;   // Color para respuesta incorrecta
    private Animator avatarAnimatorMen;
    private Animator avatarAnimatorWomen;
    private float timer;                 // Variable para el cronómetro
    private bool timerRunning = true;
    private Coroutine timerCoroutine;    // Referencia a la rutina del cronómetro
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
        StartTimer();       // Inicia el cronómetro
    }

    // Método para iniciar el cronómetro
    void StartTimer()
    {
        StartCoroutine(UpdateTimer());   // Inicia la rutina del cronómetro
    }

    // Método llamado en cada fotograma
    void Update()
    {
        // Obtiene el valor almacenado en PlayerPrefs que indica si se ha seleccionado el avatar masculino o femenino
        Men = PlayerPrefs.GetInt("MenSelect") == 1;
        Women = PlayerPrefs.GetInt("WomenSelect") == 1;

        // Ajusta la velocidad del cronómetro según tus preferencias
        float timeScale = timerRunning ? 1f : 0f;  // Cambia la velocidad según si el cronómetro está en ejecución

        // Actualiza el tiempo del cronómetro solo si está en ejecución
        if (timerRunning)
        {
            timer += Time.deltaTime * timeScale;
            timerText.text = timer.ToString("F0");   // Actualiza el texto del cronómetro en formato entero
        }
    }

    // Método para inicializar el cuestionario
    void InitializeQuiz()
    {
        StartTimer();   // Inicia el cronómetro al inicializar el cuestionario
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

    // Rutina para actualizar el cronómetro
    IEnumerator UpdateTimer()
    {
        while (true)
        {
            timer += Time.deltaTime;
            yield return null;

            // Agrega la siguiente verificación dentro del bucle
            if (timer >= 0) // Ajusta según tu lógica de tiempo límite
            {
                // Detén el cronómetro y sal del bucle
                Debug.Log("Cronómetro detenido");
                yield break;
            }
        }
    }

    // Método para mezclar las respuestas utilizando el algoritmo Fisher-Yates
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

    // Método para mostrar la pregunta y las respuestas en los botones
    void DisplayQuestion(QuestionData question)
    {
        questionText.text = question.question;   // Establece el texto de la pregunta

        // Configura los botones con las respuestas
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = question.answers[i];   // Establece el texto de respuesta en el botón
            answerButtons[i].GetComponent<Image>().color = Color.white;   // Restaura el color original del botón
            answerButtons[i].interactable = true;   // Habilita los clics nuevamente
        }

        // Configura el índice de la respuesta correcta en las respuestas mezcladas
        correctAnswerIndex = System.Array.IndexOf(question.answers, question.correctAnswer);
    }

    // Método llamado al hacer clic en un botón de respuesta
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
                    // Ejecuta la animación "Correcta" en el avatar
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
                    // Ejecuta la animación "Correcta" en el avatar
                    avatarAnimatorWomen.SetTrigger("Correct");
                }

                // Destruye el objeto del avatar masculino
                Destroy(MenCharacter);
            }

            // Detén el cronómetro solo en caso de respuesta correcta
            timerRunning = false;

            // Realiza acciones específicas para la respuesta correcta después de un retraso de 3 segundos
            Invoke("LoadNextScene", 3f);
        }
        else
        {
            // Respuesta incorrecta
            SetButtonColor(answerButtons[buttonIndex], incorrectColor);
            PlaySound(incorrectSound);

            // Ejecuta la animación "Incorrecta" en el avatar
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

            // Realiza acciones específicas para la respuesta incorrecta después de un retraso
            Invoke("RestartButtons", delayBeforeRestart);
        }
    }

    // Método para cargar la siguiente escena
    void LoadNextScene()
    {
        // Pasa la puntuación a la siguiente escena
        score -= Mathf.RoundToInt(timer);
        // Asegúrate de que la puntuación no sea negativa
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

    // Método para reiniciar la interactividad y el color de los botones
    void RestartButtons()
    {
        // Reinicia el cronómetro
        timerRunning = true;
        // Habilita la interactividad y restablece el color de los botones
        foreach (var button in answerButtons)
        {
            button.interactable = true;
            button.GetComponent<Image>().color = Color.white;
        }

        // Asegúrate de tener una nueva pregunta y respuestas aquí  
        InitializeQuiz();
    }

    // Método para configurar el color de un botón y deshabilitar los clics
    void SetButtonColor(Button button, Color color)
    {
        // Configura el color del botón y deshabilita los clics
        button.GetComponent<Image>().color = color;
        button.interactable = false;
    }

    // Método para reproducir un sonido proporcionado
    void PlaySound(AudioClip sound)
    {
        // Reproduce el sonido en la posición de la cámara principal
        if (sound != null)
        {
            AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
        }
    }

    // Método para obtener preguntas aleatorias
    QuestionData[] GetRandomQuestions()
    {
        // Aquí debes tener tu lógica para obtener preguntas aleatorias
        // Retorna un array de preguntas (cada pregunta debe ser un objeto que contenga la pregunta, respuestas y la respuesta correcta)
        return new QuestionData[]
        {
            new QuestionData("¿Cuáles curiosidades son correctas?", new string[] { "Son timidos y saltarines", "Son limpios y se comen sus heces", "Miden 2 metros y son dormilones", "Son veloces y comen carne" }, "Son limpios y se comen sus heces"),
            // Agrega más preguntas según tus necesidades
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
