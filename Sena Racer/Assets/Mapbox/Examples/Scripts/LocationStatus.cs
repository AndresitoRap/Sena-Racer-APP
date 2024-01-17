namespace Mapbox.Examples
{
    using Mapbox.Unity.Location;
    using Mapbox.Utils;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class LocationStatus : MonoBehaviour
    {
        [SerializeField]
        Text _statusText;

        private AbstractLocationProvider _locationProvider = null;
        Location currLoc;

        void Start()
        {
            // Obtener la instancia predeterminada del proveedor de ubicación
            if (null == _locationProvider)
            {
                _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider as AbstractLocationProvider;
            }
        }

        void Update()
        {
            // Obtener la ubicación actual del proveedor de ubicación
            currLoc = _locationProvider.CurrentLocation;

            // Actualizar el texto del estado según el estado actual de los servicios de ubicación
            if (currLoc.IsLocationServiceInitializing)
            {
                _statusText.text = "location services are initializing";
            }
            else
            {
                if (!currLoc.IsLocationServiceEnabled)
                {
                    _statusText.text = "location services not enabled";
                }
                else
                {
                    if (currLoc.LatitudeLongitude.Equals(Vector2d.zero))
                    {
                        _statusText.text = "Waiting for location ....";
                    }
                    else
                    {
                        _statusText.text = string.Format("{0}", currLoc.LatitudeLongitude);
                    }
                }
            }
        }

        // Obtener la latitud de la ubicación actual
        public double GetLocationLat()
        {
            return currLoc.LatitudeLongitude.x;
        }

        // Obtener la longitud de la ubicación actual
        public double GetLocationLon()
        {
            return currLoc.LatitudeLongitude.y;
        }
    }
}