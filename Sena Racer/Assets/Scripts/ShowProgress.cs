using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowProgress : MonoBehaviour
{
    public TMP_Text progressText; // Asigna el componente de texto desde el Editor Unity

    void Start()
    {
        // Recupera el progreso de PlayerPrefs
        int visitedCount = PlayerPrefs.GetInt("VisitedCount", 0);
        int totalCount = PlayerPrefs.GetInt("TotalCount", 0);

        // Actualiza el texto del progreso en el canvas
        progressText.text = visitedCount + "/" + totalCount;
    }
}
