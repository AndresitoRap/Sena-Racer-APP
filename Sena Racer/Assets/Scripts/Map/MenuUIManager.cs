using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;

public class MenuUIManager : MonoBehaviour
{
    // Paneles de eventos para usuario en rango y usuario fuera de rango
    [SerializeField] private GameObject eventPanelUserInRange;
    [SerializeField] private GameObject eventPanelUserNotRange;

    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private AudioClip errorSFX;
    [SerializeField] private AudioClip okeySFX;
 

    // Variable que indica si algún panel UI está activo
    bool isUIPanelActive;

    // Almacena temporalmente el identificador del evento
    int tempEvent;

    // Referencia al EventManager para activar eventos
    [SerializeField] private EventManager eventManager;

    // Método llamado al inicio del script
    void Start()
    {
        // Puedes agregar lógica de inicialización aquí si es necesario
    }

    // Método llamado en cada fotograma
    void Update()
    {
        // Puedes agregar lógica de actualización aquí si es necesario
    }

    // Método para mostrar el panel de inicio del evento
    public void DisplayStartEventPanel(int eventID)
    {
        // Verifica si algún panel UI ya está activo
        if (isUIPanelActive == false)
        {
            // Reproduce el sonido
            if (soundEffect != null && okeySFX != null)
            {
                soundEffect.PlayOneShot(okeySFX);
            }
            // Almacena temporalmente el identificador del evento
            tempEvent = eventID;

            // Activa el panel de usuario en rango
            eventPanelUserInRange.SetActive(true);

            // Marca que un panel UI está activo
            isUIPanelActive = true;
        }
    }

    // Método llamado cuando se hace clic en el botón "Join"
    public void OnJoinButtonClick()
    {
        // Activa el evento correspondiente usando el EventManager
        eventManager.ActivateEvent(tempEvent);
        // Desactiva la estación en el mapa después de visitarla
        FindObjectOfType<SpawnOnMap>().DeactivateStation(tempEvent - 1);
    }

    // Método para mostrar el panel de usuario fuera de rango
    public void DisplayUserNotInRange()
    {
        // Verifica si algún panel UI ya está activo
        if (isUIPanelActive == false)
        {
        // Reproduce el sonido
        if (soundEffect != null && errorSFX != null)
        {
            soundEffect.PlayOneShot(errorSFX);
        }

        // Activa el panel de usuario fuera de rango
        eventPanelUserNotRange.SetActive(true);

        // Marca que un panel UI está activo
        isUIPanelActive = true;
        }

        // Activa el panel de usuario fuera de rango (duplicado)
        eventPanelUserNotRange.SetActive(true);
    }

    // Método llamado cuando se hace clic en el botón de cierre
    public void CloseButtonClick()
    {
        // Desactiva ambos paneles de eventos y restablece la bandera
        eventPanelUserInRange.SetActive(false);
        eventPanelUserNotRange.SetActive(false);
        isUIPanelActive = false;
    }
}