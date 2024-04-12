using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Librería de Mapbox
using Mapbox.Examples;
using Mapbox.Utils;
using Mapbox.Geocoding;

public class LoadSceneAndyBee : MonoBehaviour
{
    // Referencias a los managers
    MenuUIManager menuUIManager;
    EventManager eventManager;

    // Estado de la ubicación del jugador
    LocationStatus playerLocation;

    // Posición del evento en coordenadas geográficas
    public Vector2d eventPoss;

    // Identificador del evento
    public int eventID;

    // Método llamado al inicio del script
    void Start()
    {
        // Obtener referencias a los managers mediante sus GameObjects
        menuUIManager = GameObject.Find("Canvas").GetComponent<MenuUIManager>();
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
    }

    // Este método se llama cuando el objeto 3D es tocado

    private void OnMouseDown()
    {
        // Obtener la ubicación actual del jugador desde el componente LocationStatus
        playerLocation = GameObject.Find("Canvas").GetComponent<LocationStatus>();
        
        // Crear objetos GeoCoordinate con las coordenadas del jugador y del evento
        var currentPlayerLocation = new GeoCoordinatePortable.GeoCoordinate(playerLocation.GetLocationLat(), playerLocation.GetLocationLon());
        var eventLocation = new GeoCoordinatePortable.GeoCoordinate(eventPoss[0], eventPoss[1]);

        // Calcular la distancia entre el jugador y el evento
        var distance = currentPlayerLocation.GetDistanceTo(eventLocation);

        // Verificar si el jugador está dentro del rango del evento
        if (distance < 5)
        {
            // Mostrar el panel de inicio del evento
            menuUIManager.DisplayStartEventPanel(eventID);
        }
        else
        {
            // Mostrar el panel de usuario fuera de rango
            menuUIManager.DisplayUserNotInRange();
        }
    }


}
