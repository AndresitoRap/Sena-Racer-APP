using UnityEngine;
using UnityEngine.UI;

public class AutoClick : MonoBehaviour
{
    public Button miBoton; // Asegúrate de asignar este botón en el Inspector de Unity

    void Start()
    {
        miBoton.onClick.Invoke(); // Esto invocará la función asignada al botón en el Inspector de Unity
    }
}
