using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    public Image imageToHide; // La imagen que quieres ocultar
    private Canvas canvas; // El Canvas adicional para controlar el orden de renderizado

    // Variable estática para rastrear si la imagen ya se ha mostrado
    private static bool hasImageBeenShown = false;

    void Start()
    {
        // Añade un nuevo Canvas al objeto de la imagen
        canvas = imageToHide.gameObject.AddComponent<Canvas>();

        // Asegúrate de que la imagen se renderice en la parte superior
        canvas.overrideSorting = true;
        canvas.sortingOrder = 10; // Un valor alto para asegurar que se renderice en la parte superior

        // Si la imagen ya se ha mostrado, la deshabilitamos
        if (hasImageBeenShown)
        {
            imageToHide.enabled = false;
        }
        else
        {
            // Si la imagen no se ha mostrado, la habilitamos y actualizamos hasImageBeenShown
            imageToHide.enabled = true;
            hasImageBeenShown = true;
        }
    }

    // Actualiza es llamado una vez por frame
    void Update()
    {
        // Verifica si el usuario ha tocado la pantalla
        if (Input.GetMouseButtonDown(0) && imageToHide.enabled)
        {
            // Cambia la visibilidad de la imagen
            imageToHide.enabled = !imageToHide.enabled;

            // Envía una señal de que la imagen se ha ocultado
            if (!imageToHide.enabled)
            {
                FindObjectOfType<SwipeEffect>().OnImageHidden();
            }
        }
    }
}
