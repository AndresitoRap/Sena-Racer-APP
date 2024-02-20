using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    public TMP_InputField ID;
    public TMP_InputField Password;
    public TMP_Text MessageError;
    public Image ErrorMessageImage; // Referencia a la imagen del mensaje de error
    public Button ShowPasswordButton; // Botón para mostrar la contraseña
    public Sprite EyeOpen; // Icono del ojo abierto
    public Sprite EyeClosed; // Icono del ojo cerrado

    private bool isPasswordShown = false; // Variable para rastrear si la contraseña se está mostrando o no

    private void Start() {
        ShowPasswordButton.onClick.AddListener(TogglePasswordVisibility);
    }

    public void Log_In() {

        if (string.IsNullOrEmpty(ID.text) || string.IsNullOrEmpty(Password.text))
        {
            MessageError.text = "DEBES LLENAR TODOS LOS CAMPOS";
            StartCoroutine(ShowErrorMessage());
        }
        else {
            SceneManager.LoadScene("Welcome");
            ErrorMessageImage.gameObject.SetActive(false);
        }
    }

    IEnumerator ShowErrorMessage() {
        ErrorMessageImage.gameObject.SetActive(true);
        ErrorMessageImage.color = new Color(ErrorMessageImage.color.r, ErrorMessageImage.color.g, ErrorMessageImage.color.b, 0);
        ErrorMessageImage.rectTransform.anchoredPosition = new Vector2(-ErrorMessageImage.rectTransform.rect.width, ErrorMessageImage.rectTransform.anchoredPosition.y);

        LeanTween.moveX(ErrorMessageImage.rectTransform, 0, 1f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.alpha(ErrorMessageImage.rectTransform, 1f, 1f).setEase(LeanTweenType.easeInQuad);

        yield return new WaitForSeconds(1f);

        // Aquí puedes agregar más código para manejar lo que sucede después de que se muestra el mensaje de error
    }

    // Método para mostrar y ocultar la contraseña
    private void TogglePasswordVisibility() {
        isPasswordShown = !isPasswordShown;

        if (isPasswordShown) {
            Password.contentType = TMP_InputField.ContentType.Standard;
            ShowPasswordButton.image.sprite = EyeOpen; // Cambia el icono a ojo abierto
        } else {
            Password.contentType = TMP_InputField.ContentType.Password;
            ShowPasswordButton.image.sprite = EyeClosed; // Cambia el icono a ojo cerrado
        }

        Password.ForceLabelUpdate();
    }
}
