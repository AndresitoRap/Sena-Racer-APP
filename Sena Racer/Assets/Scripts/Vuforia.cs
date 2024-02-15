using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vuforia : MonoBehaviour
{
    public GameObject[] imageTargets;

    public void ActivateImageTarget(int eventID)
    {
        // Desactivar todos los ImageTargets al principio
        foreach (GameObject target in imageTargets)
        {
            target.SetActive(false);
        }

        // Activar el ImageTarget correspondiente al ID del evento
        if (eventID >= 0 && eventID < imageTargets.Length)
        {
            imageTargets[eventID].SetActive(true);
        }
        else
        {
            Debug.LogError("ID de evento fuera de rango: " + eventID);
        }
    }
    
}
