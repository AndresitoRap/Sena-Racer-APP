﻿using System.Collections;
using UnityEngine;
using TMPro;

using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwipeEffect : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _initialPosition;
    private float _distanceMoved;
    private bool _swipeLeft;
    private bool _isPulsing = false;
    private bool _canMove = true;
    public static int imagesMoved = 0;
    public static int totalImagesMoved = 0;
    private bool _firstRightSwipe = false;
    public static bool nonSpecializationRightSwipe = false;
    public static float startTime;
    private int _errors = 0;
    public static float finalTime;
    public static int finalErrors;
    public static int score = 150; // Cambia el puntaje inicial a 300
    private float _lastScoreReductionTime;
    public static GameObject timerObject;
    public AudioSource audioCorrect;
    public AudioSource audioIncorrect;

    // Variable estática para rastrear si la pantalla ha sido presionada
    public static bool screenPressed = false;

    // Declara las variables timer y timerText
    private float timer = 0.0f;
    public TMP_Text timerText;

    void Start()
    {
        if (timerObject == null)
        {
            timerObject = new GameObject("TimerObject");
            DontDestroyOnLoad(timerObject);
            startTime = Time.time;
        }
        _lastScoreReductionTime = Time.time;
        imagesMoved = 0;
        nonSpecializationRightSwipe = false;

        // Recupera el tiempo de PlayerPrefs
        timer = PlayerPrefs.GetFloat("Time", 0);
    }

    void Update()
    {
        // Solo actualiza el tiempo si la pantalla ha sido presionada
        if (screenPressed)
        {
            timer += Time.deltaTime;
            timerText.text = timer.ToString("F0");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_canMove) return;

        transform.localPosition = new Vector2(transform.localPosition.x + eventData.delta.x, transform.localPosition.y);

        if (transform.localPosition.x - _initialPosition.x > 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, -30, (_initialPosition.x + transform.localPosition.x) / (Screen.width / 2)));
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, 30, (_initialPosition.x - transform.localPosition.x) / (Screen.width / 2)));
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _initialPosition = transform.localPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _distanceMoved = Mathf.Abs(transform.localPosition.x - _initialPosition.x);

        if (_distanceMoved < 0.4 * Screen.width)
        {
            transform.localPosition = _initialPosition;
        }
        else
        {
            if (transform.localPosition.x > _initialPosition.x)
            {
                _swipeLeft = false;

                if (!_firstRightSwipe)
                {
                    _firstRightSwipe = true;

                    if (gameObject.name != "ESPECIALIZACIÓN")
                    {
                        _firstRightSwipe = false;
                        nonSpecializationRightSwipe = true;
                        _errors++;
                        score -= 10;
                        if (score < 5) score = 5; // Garantiza un mínimo de 5 puntos
                    }
                }
            }
            else
            {
                _swipeLeft = true;
                _errors++;
                score -= 10;
                if (score < 5) score = 5; // Garantiza un mínimo de 5 puntos
            }

            StartCoroutine(MovedCard());
        }

        // La pantalla ha sido presionada
        screenPressed = true;
    }

    private IEnumerator MovedCard()
    {
        float time = 0;
        Color originalColor = GetComponent<Image>().color;

        while (GetComponent<Image>().color.a > 0)
        {
            time += Time.deltaTime;

            if (_swipeLeft)
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x,
                    transform.localPosition.x - Screen.width, time), transform.localPosition.y, 0);
            }
            else
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x,
                    transform.localPosition.x + Screen.width, time), transform.localPosition.y, 0);
            }

            GetComponent<Image>().color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.SmoothStep(1, 0, 4 * time));

            yield return null;
        }

       if (gameObject.name == "ESPECIALIZACIÓN" && !_swipeLeft && _firstRightSwipe && !nonSpecializationRightSwipe)
{
    // Detiene el tiempo antes de iniciar la animación de pulso
    screenPressed = false;

    // Registra el tiempo final
    finalTime = timer;

    transform.localPosition = _initialPosition;
    transform.localEulerAngles = Vector3.zero;
    GetComponent<Image>().color = originalColor;

    if (!_isPulsing)
    {
        StartCoroutine(Pulse());
    }

    _canMove = false;

    // Reproduce el sonido
    audioCorrect.Play();

    // Espera durante la duración del sonido (3 segundos en este caso)
    yield return new WaitForSeconds(4);

    SceneManager.LoadScene("MResultsporcinos");
}


        imagesMoved++;
        totalImagesMoved++;

        if (imagesMoved >= 6)
        {
            // Reproduce el sonido
            audioIncorrect.Play();

            // Espera durante la duración del sonido (2 segundos en este caso)
            yield return new WaitForSeconds(3);

            // Guarda el tiempo en PlayerPrefs
            PlayerPrefs.SetFloat("Time", timer);

            // Restablece el estado IsFirstRun
            PlayerPrefs.SetInt("IsFirstRun", 1);

            // Carga la escena
            SceneManager.LoadScene("MaoAsks");
        }
    }

    private IEnumerator Pulse()
    {
        _isPulsing = true;
        screenPressed = false; // Detiene el tiempo

        while (true)
        {
            for (float scale = 1; scale <= 1.1f; scale += Time.deltaTime / 2)
            {
                transform.localScale = new Vector3(0.9504963f * scale, 1.152072f * scale, 1);
                yield return null;
            }

            for (float scale = 1.1f; scale >= 1; scale -= Time.deltaTime / 2)
            {
                transform.localScale = new Vector3(0.9504963f * scale, 1.152072f * scale, 1);
                yield return null;
            }
        }
    }

    public void OnImageHidden() // Asegúrate de que este método sea público
    {
        // La pantalla ha sido presionada
        screenPressed = true;
    }
}
