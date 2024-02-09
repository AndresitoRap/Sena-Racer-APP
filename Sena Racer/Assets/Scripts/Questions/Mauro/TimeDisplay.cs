using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI timeText; // Asegúrate de asignar este campo en el Inspector de Unity

    void Update()
    {
        // Obtiene el tiempo transcurrido desde SwipeEffect y lo muestra en el texto
        float elapsedTime = Time.time - SwipeEffect.startTime;
        timeText.text = "Tiempo: " + elapsedTime.ToString("F2") + "s";
    }
}
