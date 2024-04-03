using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AnswerButton : MonoBehaviour
{
    public Button[] answerButtons;
    public TMP_Text questionText;
    public float delayBeforeRestart = 4f;
    public AudioClip correctSound;
    public AudioClip incorrectSound;
    public TMP_Text timerText;

    private int correctAnswerIndex;
    private Color correctColor = Color.green;
    private Color incorrectColor = Color.red;
    private Animator avatarAnimatorMen;
    private Animator avatarAnimatorWomen;
    private float timer;
    private bool timerRunning = true;
    private Coroutine timerCoroutine;
    private int score = 100;
    private float lastIncorrectAnswerTime;

    public bool Men;
    public bool Women;
    public GameObject MenCharacter;
    public GameObject WomenCharacter;

    // Añade un AudioSource a tu clase
    private AudioSource audioSource;

    // Diccionario de preguntas
    private Dictionary<string, QuestionDataMao[]> sceneQuestions = new Dictionary<string, QuestionDataMao[]>
    {
        {
            "AQuestion", new QuestionDataMao[]
            {
                new("¿Cúal es el nombre de nuestra institución?", new string[] { "Servicio Nacional de Aprendizaje", "Servicio de Negocios de Aprendizaje", "Sistema Nacional de Aprendizaje", "Servicio Internacional de Aprendizaje" }, "Servicio Nacional de Aprendizaje"),
                // Agrega más preguntas para la escena "AQuestion"
            }
        },
        {
            "MQuestionBee", new QuestionDataMao[]
            {
                new("¿Cuál de los siguientes NO es un beneficio de la apicultura?", new string[] { "Polinización de cultivos", "Producción de miel", "Mejora de la calidad del aire", "Producción de cera de abejas" }, "Mejora de la calidad del aire"),
                // Agrega más preguntas para la escena "MQuestion"
            }
        },
        {
            "XQuestion", new QuestionDataMao[]
            {
                new("¿Cuáles curiosidades son correctas?", new string[] { "Son timidos y saltarines", "Son limpios y se comen sus heces", "Miden 2 metros y son dormilones", "Son veloces y comen carne" }, "Son limpios y se comen sus heces"),
                // Agrega más preguntas para la escena "MQuestion"
            }
        },
        {
          "DQuestion", new QuestionDataMao[]
            {
                new QuestionDataMao("¿A que especie cientifica pertenecen las vacas?", new string[] { "Anatinade", "Gallus", "Bovidae", "Bos Taurus" }, "Bos Taurus"),
                // Agrega más preguntas para la escena "AQuestion"
            }
        }
        // Agrega más escenas y sus preguntas
    };

    void Start()
    {
        // Inicializa el AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        InitializeQuiz();
        StartTimer();
    }

    void StartTimer()
    {
        StartCoroutine(UpdateTimer());
    }

    void Update()
    {
        Men = PlayerPrefs.GetInt("MenSelect") == 1;
        Women = PlayerPrefs.GetInt("WomenSelect") == 1;

        float timeScale = timerRunning ? 1f : 0f;

        if (timerRunning)
        {
            timer += Time.deltaTime * timeScale;
            timerText.text = timer.ToString("F0");
        }
    }

    void InitializeQuiz()
    {
        StartTimer();
        QuestionDataMao[] questions = GetRandomQuestions();

        QuestionDataMao currentQuestion = questions[0];
        ShuffleAnswers(currentQuestion.answers);

        DisplayQuestion(currentQuestion);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int buttonIndex = i;
            answerButtons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    IEnumerator UpdateTimer()
    {
        while (true)
        {
            timer += Time.deltaTime;
            yield return null;

            if (timer >= 0)
            {
                Debug.Log("Cronómetro detenido");
                yield break;
            }
        }
    }

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

    void DisplayQuestion(QuestionDataMao question)
    {
        questionText.text = question.question;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = question.answers[i];
            answerButtons[i].GetComponent<Image>().color = Color.white;
            answerButtons[i].interactable = true;
        }

        correctAnswerIndex = System.Array.IndexOf(question.answers, question.correctAnswer);
    }

    void OnButtonClick(int buttonIndex)
    {
        foreach (var button in answerButtons)
        {
            button.interactable = false;
        }

        if (buttonIndex == correctAnswerIndex)
        {
            SetButtonColor(answerButtons[buttonIndex], correctColor);
            PlaySound(correctSound);

            if (Men && MenCharacter != null)
            {
                avatarAnimatorMen = MenCharacter.GetComponent<Animator>();
                if (avatarAnimatorMen != null)
                {
                    avatarAnimatorMen.SetTrigger("Correct");
                }

                Destroy(WomenCharacter);
            }

            if (Women && WomenCharacter != null)
            {
                avatarAnimatorWomen = WomenCharacter.GetComponent<Animator>();
                if (avatarAnimatorWomen != null)
                {
                    avatarAnimatorWomen.SetTrigger("Correct");
                }

                Destroy(MenCharacter);
            }

            timerRunning = false;

            Invoke("LoadNextScene", 3f);
        }
        else
        {
            SetButtonColor(answerButtons[buttonIndex], incorrectColor);
            PlaySound(incorrectSound);

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

            Invoke("RestartButtons", delayBeforeRestart);
        }
    }

    void LoadNextScene()
    {
        score -= Mathf.RoundToInt(timer);
        if (score < 0)
        {
            score = 0;
        }

        // Obtiene el nombre de la escena activa
        string sceneName = SceneManager.GetActiveScene().name;

        // Selecciona el puntaje y el tiempo basándose en el nombre de la escena
        if (sceneName == "AQuestion")
        {
            PlayerPrefs.SetInt("ScoreAndy", score);
            PlayerPrefs.SetFloat("TimeAndy", timer);
            PlayerPrefs.Save();

            SceneManager.LoadScene("AResults");
        }
        else if (sceneName == "MQuestion")
        {
            PlayerPrefs.SetInt("ScoreMao", score);
            PlayerPrefs.SetFloat("TimeMao", timer);
            PlayerPrefs.Save();

            SceneManager.LoadScene("MResults");
        }
        else if (sceneName == "MQuestionBee")
        {
            PlayerPrefs.SetInt("ScoreBee", score);
            PlayerPrefs.SetFloat("TimeBee", timer);
            PlayerPrefs.Save();

            SceneManager.LoadScene("MResultsBee");
        }
        else if (sceneName == "DQuestion")
        {
            PlayerPrefs.SetInt("ScoreDiego", score);
            PlayerPrefs.SetFloat("TimeDiego", timer);
            PlayerPrefs.Save();

            SceneManager.LoadScene("DResults");
        }

        else if (sceneName == "XQuestion")
        {
            PlayerPrefs.SetInt("ScoreXim", score);
            PlayerPrefs.SetFloat("TimeXim", timer);
            PlayerPrefs.Save();

            SceneManager.LoadScene("XResults");
        }
    }

    void RestartButtons()
    {
        timerRunning = true;
        foreach (var button in answerButtons)
        {
            button.interactable = true;
            button.GetComponent<Image>().color = Color.white;
        }

        InitializeQuiz();
    }

    void SetButtonColor(Button button, Color color)
    {
        button.GetComponent<Image>().color = color;
        button.interactable = false;
    }

    // Modifica tu método PlaySound
    void PlaySound(AudioClip sound)
    {
        if (sound != null)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
    }

    QuestionDataMao[] GetRandomQuestions()
    {
        string sceneName = SceneManager.GetActiveScene().name; // Obtiene el nombre de la escena activa

        if (sceneQuestions.ContainsKey(sceneName)) // Verifica si el diccionario contiene preguntas para la escena activa
        {
            return sceneQuestions[sceneName]; // Retorna las preguntas para la escena activa
        }
        else
        {
            Debug.LogError("No se encontraron preguntas para la escena: " + sceneName);
            return new QuestionDataMao[0]; // Retorna un array vacío si no se encontraron preguntas
        }
    }
}

[System.Serializable]
public class QuestionDataMao
{
    public string question;
    public string[] answers;
    public string correctAnswer;

    public QuestionDataMao(string question, string[] answers, string correctAnswer)
    {
        this.question = question;
        this.answers = answers;
        this.correctAnswer = correctAnswer;
    }
}
