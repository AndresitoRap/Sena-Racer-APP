using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    public TMP_InputField ID;
    public TMP_InputField Password;
    public TMP_Text MessageError;
    public Image ErrorMessageImage; // Referencia a la imagen del mensaje de error
    public GameObject loadingPanel; // Referencia al panel de carga en tu Canvas
    public Button StartButton; // Botón para iniciar sesión
    public Button ShowPasswordButton; // Botón para mostrar la contraseña
    public Sprite EyeOpen; // Icono del ojo abierto
    public Sprite EyeClosed; // Icono del ojo cerrado

    private bool isPasswordShown = false; // Variable para rastrear si la contraseña se está mostrando o no
    string url = "https://backend-strapi-senaracer.onrender.com/api/runners";

    [System.Serializable]
    public class RunnerAttributes
    {
        public string identification;
        public string password;
    }

    [System.Serializable]
    public class Runner
    {
        public RunnerAttributes attributes;
    }

    [System.Serializable]
    public class RunnerData
    {
        public Runner[] data;
    }

    private void Start()
    {
        StartButton.onClick.AddListener(Log_In); // Añade el listener al botón Start
        ShowPasswordButton.onClick.AddListener(TogglePasswordVisibility);
        loadingPanel.SetActive(false); // Desactiva el panel de carga al iniciar
    }

    public void Log_In()
    {
        if (string.IsNullOrEmpty(ID.text) || string.IsNullOrEmpty(Password.text))
        {
            ShowErrorMessage("DEBES LLENAR TODOS LOS CAMPOS");
            StartCoroutine(HideErrorMessage());
        }
        else
        {
            StartCoroutine(GetData(ID.text, Password.text));
        }
    }

    IEnumerator GetData(string identificationInput, string passwordInput)
    {
        ID.interactable = false;
        Password.interactable = false;
        StartButton.interactable = false;
        ShowPasswordButton.interactable = false;

        // Activa el panel de carga antes de enviar la solicitud
        loadingPanel.SetActive(true);

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        // Desactiva el panel de carga después de recibir la respuesta
        loadingPanel.SetActive(false);

        // Reactivar inputs y botones al finalizar la verificación
        ID.interactable = true;
        Password.interactable = true;
        StartButton.interactable = true;
        ShowPasswordButton.interactable = true;

        if (request.result == UnityWebRequest.Result.Success && request.downloadHandler.text != null)
        {
            // Parsea el JSON a un objeto que contiene un array de objetos Runner
            RunnerData runnerData = JsonUtility.FromJson<RunnerData>(request.downloadHandler.text);

            // Busca en el array de corredores el que tiene la identificación y contraseña correctas
            if (runnerData != null && runnerData.data != null)
            {
                foreach (Runner runner in runnerData.data)
                {
                    if (runner.attributes.identification == identificationInput && runner.attributes.password == passwordInput)
                    {
                        // Si los datos son correctos, cambia a la escena "Welcome"
                        SceneManager.LoadScene("Welcome");
                        yield break;
                    }
                }
            }

            // Si llegamos aquí, significa que no encontramos un corredor con la identificación y contraseña correctas
            ShowErrorMessage("Los datos ingresados no son correctos.");
            StartCoroutine(HideErrorMessage());
        }
        else
        {
            Debug.Log(request.error);
            ShowErrorMessage("Error al conectar con el servidor.");
            StartCoroutine(HideErrorMessage());
        }
    }

    void ShowErrorMessage(string errorMessage)
    {
        MessageError.text = errorMessage;
        ErrorMessageImage.gameObject.SetActive(true);
        ErrorMessageImage.color = new Color(ErrorMessageImage.color.r, ErrorMessageImage.color.g, ErrorMessageImage.color.b, 0);
        ErrorMessageImage.rectTransform.anchoredPosition = new Vector2(-ErrorMessageImage.rectTransform.rect.width, ErrorMessageImage.rectTransform.anchoredPosition.y);

        LeanTween.moveX(ErrorMessageImage.rectTransform, 0, 1f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.alpha(ErrorMessageImage.rectTransform, 1f, 1f).setEase(LeanTweenType.easeInQuad);
    }

    IEnumerator HideErrorMessage()
    {
        yield return new WaitForSeconds(3f);
        ErrorMessageImage.gameObject.SetActive(false);
    }

    // Método para mostrar y ocultar la contraseña
    private void TogglePasswordVisibility()
    {
        isPasswordShown = !isPasswordShown;

        if (isPasswordShown)
        {
            Password.contentType = TMP_InputField.ContentType.Standard;
            ShowPasswordButton.image.sprite = EyeOpen; // Cambia el icono a ojo abierto
        }
        else
        {
            Password.contentType = TMP_InputField.ContentType.Password;
            ShowPasswordButton.image.sprite = EyeClosed; // Cambia el icono a ojo cerrado
        }

        Password.ForceLabelUpdate();
    }
}
