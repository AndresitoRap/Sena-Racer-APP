namespace Mapbox.Examples
{
    using UnityEngine;
    using Mapbox.Utils;
    using Mapbox.Unity.Map;
    using Mapbox.Unity.MeshGeneration.Factories;
    using Mapbox.Unity.Utilities;
    using System.Collections.Generic;
    using UnityEngine.SceneManagement;
    using TMPro;

    public class SpawnOnMap : MonoBehaviour
    {
        [SerializeField]
        AbstractMap _map;

        [SerializeField]
        [Geocode]
        string[] _locationStrings;
        Vector2d[] _locations;

        [SerializeField]
        float _spawnScale = 100f;

        [SerializeField]
        GameObject _markerPrefab;
        List<GameObject> _spawnedObjects;

        public TMP_Text progressText; // Asigna el componente de texto desde el Editor Unity
        public GameObject Finally; // Asigna el cuadro blanco desde el Editor Unity
        int visitedStationsCount = 0;


        void Start()
        {
            // Inicializar arreglos y listas
            _locations = new Vector2d[_locationStrings.Length];
            _spawnedObjects = new List<GameObject>();

            // Verificar la longitud de _spawnedObjects
            Debug.Log("_spawnedObjects length: " + _spawnedObjects.Count);

            // Verificar si _locationStrings tiene elementos
            if (_locationStrings.Length == 0)
            {
                Debug.LogError("_locationStrings is empty!");
                return;
            }

            // Iterar a través de las ubicaciones especificadas en el inspector
            for (int i = 0; i < _locationStrings.Length; i++)
            {
                // Convertir la cadena de ubicación a coordenadas latitud y longitud
                var locationString = _locationStrings[i];
                _locations[i] = Conversions.StringToLatLon(locationString);

                // Instanciar un marcador y configurar sus propiedades
                var instance = Instantiate(_markerPrefab);
                instance.GetComponent<EventPointer>().eventPoss = _locations[i];
                instance.GetComponent<EventPointer>().eventID = i + 1;
                instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
                instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                // Desactivar la estación si ha sido visitada
                bool stationVisited = PlayerPrefs.GetInt("Station_" + i, 0) == 1;
                instance.SetActive(!stationVisited);
                // Incrementar el contador si la estación ha sido visitada
                if (stationVisited)
                {
                    visitedStationsCount++;
                }
                // Agregar el marcador a la lista de objetos instanciados
                _spawnedObjects.Add(instance);
                // Agregar mensaje de depuración para verificar la instancia
                Debug.Log("Spawned object at index " + i);
            }
            // Actualizar el progreso utilizando el nuevo script
            UpdateProgress();
        }

        // Método para desactivar una estación cuando el jugador la visita
        public void DeactivateStation(int stationIndex)
        {
            // Verificar si stationIndex está dentro del rango de _spawnedObjects
            if (stationIndex >= 0 && stationIndex < _spawnedObjects.Count)
            {
                _spawnedObjects[stationIndex].SetActive(false);
                PlayerPrefs.SetInt("Station_" + stationIndex, 1); // Marcar la estación como visitada

                // Agregar mensaje de depuración para verificar la desactivación
                Debug.Log("Deactivated station at index " + stationIndex);
            }
            else
            {
                Debug.LogError("Invalid station index: " + stationIndex);
            }
        }

        // Método para actualizar el progreso y mostrar el cuadro blanco si es necesario
        void UpdateProgress()
        {
            // Obtén el script Progress
            Progress progress = FindObjectOfType<Progress>();

            // Actualiza el progreso utilizando el nuevo script
            progress.UpdateProgress(visitedStationsCount, _spawnedObjects.Count);

            // Verifica si todas las estaciones han sido visitadas y muestra el cuadro blanco si es necesario
            if (visitedStationsCount == _spawnedObjects.Count)
            {
                // Activa el cuadro blanco en el centro del canvas
                Finally.SetActive(true);
            }
        }

        private void Update()
        {
             // Actualizar la posición y escala de los objetos instanciados en cada fotograma
            int count = _spawnedObjects.Count;
            for (int i = 0; i < count; i++)
            {
                var spawnedObject = _spawnedObjects[i];
                var location = _locations[i];
                
                // Actualizar la posición y escala del objeto según las coordenadas geográficas y la escala especificada
                spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }
        }
    }
}
