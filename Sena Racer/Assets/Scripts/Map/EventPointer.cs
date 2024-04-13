using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Librería de Mapbox
using Mapbox.Examples;
using Mapbox.Utils;
using Mapbox.Geocoding;

public class EventPointer : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] float amplitude = 2.0f;
    [SerializeField] float frequency = 0.50f;

    // Estado de la ubicación del jugador
    LocationStatus playerLocation;

    // Posición del evento en coordenadas geográficas
    public Vector2d eventPoss;

    // Identificador del evento
    public int eventID;

    // Referencias a los managers
    MenuUIManager menuUIManager;
    EventManager eventManager;

    // Método llamado al inicio del script
    void Start()
    {
        // Obtener referencias a los managers mediante sus GameObjects
        menuUIManager = GameObject.Find("Canvas").GetComponent<MenuUIManager>();
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
    }

    // Método llamado en cada fotograma
    void Update()
    {
        // Rotar y actualizar la posición del puntero
        FloatAndUpdatePointer();
    }

    // Método para hacer flotar y actualizar la posición del puntero
    void FloatAndUpdatePointer()
    {
        // Rotar el puntero alrededor del eje Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Hacer flotar el puntero en dirección vertical
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude) + 15, transform.position.z);
    }

    // Método llamado al hacer clic en el objeto
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
        if (distance < 50)
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