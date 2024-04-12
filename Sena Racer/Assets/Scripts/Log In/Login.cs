using System.Collections;
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
    public Image ErrorMessageImage;
    public GameObject loadingPanel;
    public Button StartButton;
    public Button ShowPasswordButton;
    public Sprite EyeOpen;
    public Sprite EyeClosed;

    private bool isPasswordShown = false;
    public static string corridorID; // Variable estática para guardar el ID del corredor
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
        public int id;
        public RunnerAttributes attributes;
    }

    [System.Serializable]
    public class RunnerData
    {
        public Runner[] data;
    }

    private void Start()
    {
        StartButton.onClick.AddListener(Log_In);
        ShowPasswordButton.onClick.AddListener(TogglePasswordVisibility);
        loadingPanel.SetActive(false);
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

        loadingPanel.SetActive(true);

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        loadingPanel.SetActive(false);

        ID.interactable = true;
        Password.interactable = true;
        StartButton.interactable = true;
        ShowPasswordButton.interactable = true;

        if (request.result == UnityWebRequest.Result.Success && request.downloadHandler.text != null)
        {
            RunnerData runnerData = JsonUtility.FromJson<RunnerData>(request.downloadHandler.text);

            if (runnerData != null && runnerData.data != null)
            {
                foreach (Runner runner in runnerData.data)
                {
                    if (runner.attributes.identification == identificationInput && runner.attributes.password == passwordInput)
                    {
                        corridorID = runner.id.ToString();
                        Debug.Log("ID del corredor: " + corridorID);

                        // Verificar la variable RankingManager
                        if (PlayerPrefs.GetInt("RankingManager", 0) == 1)
                        {
                            SceneManager.LoadScene("Ranking"); // Si es true, cargar la escena "Ranking"
                        }
                        else
                        {
                            SceneManager.LoadScene("Welcome"); // Si es false, cargar la escena "Welcome"
                        }

                        yield break;
                    }
                }
            }

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

    IEnumerator HideErrorMessage()
    {
        yield return new WaitForSeconds(3f);
        ErrorMessageImage.gameObject.SetActive(false);
    }

    private void TogglePasswordVisibility()
    {
        isPasswordShown = !isPasswordShown;

        if (isPasswordShown)
        {
            Password.contentType = TMP_InputField.ContentType.Standard;
            ShowPasswordButton.image.sprite = EyeOpen;
        }
        else
        {
            Password.contentType = TMP_InputField.ContentType.Password;
            ShowPasswordButton.image.sprite = EyeClosed;
        }

        Password.ForceLabelUpdate();
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
}
