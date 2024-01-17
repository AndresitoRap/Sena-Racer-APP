namespace Mapbox.Examples
{
    using UnityEngine;
    using Mapbox.Utils;
    using Mapbox.Unity.Map;
    using Mapbox.Unity.MeshGeneration.Factories;
    using Mapbox.Unity.Utilities;
    using System.Collections.Generic;
    using UnityEngine.SceneManagement;


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

        void Start()
        {
            // Inicializar arreglos y listas
            _locations = new Vector2d[_locationStrings.Length];
            _spawnedObjects = new List<GameObject>();

            // Iterar a través de las ubicaciones especificadas en el inspector
            for (int i = 0; i < _locationStrings.Length; i++)
            {
                // Convertir la cadena de ubicación a coordenadas latitud y longitud
                var locationString = _locationStrings[i];
                _locations[i] = Conversions.StringToLatLon(locationString);

               // Verificar si el evento ya ha sido completado
                if (PlayerPrefs.GetInt("EventCompleted_" + (i + 1), 0) == 0)
                {
                    // Instanciar un marcador y configurar sus propiedades
                    var instance = Instantiate(_markerPrefab);
                    instance.GetComponent<EventPointer>().eventPoss = _locations[i];
                    instance.GetComponent<EventPointer>().eventID = i + 1;
                    instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
                    instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);

                    // Agregar el marcador a la lista de objetos instanciados
                    _spawnedObjects.Add(instance);
                }
            }
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            int totalStations = Progress.Instance.GetTotalStations();
            int completedStationsCount = totalStations - _spawnedObjects.Count;
            Progress.Instance.UpdateProgress(completedStationsCount);
        }


        public int GetTotalStations()
        {
            return _locationStrings.Length;
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