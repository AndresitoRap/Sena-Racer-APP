using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject AR;
    public GameObject NotAR;

    void Start()
    {
        // Desactivar ambos objetos al inicio
        AR.SetActive(false);
        NotAR.SetActive(true);
    }

    public void ToggleARCanvas()
    {
        if (AR.activeSelf)
        {
            // Si AR está activo, desactívalo y activa el canvas
            AR.SetActive(false);
            NotAR.SetActive(true);
        }
        else
        {
            // Si el canvas está activo, desactívalo y activa AR
            AR.SetActive(true);
            NotAR.SetActive(false);
        }
    }
}
