using UnityEngine;
using TMPro; // Utiliza el espacio de nombres TMPro en lugar de UnityEngine.UI

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Cambia Text a TextMeshProUGUI

    void Start()
    {
        // Obtiene la puntuación de SwipeEffect y la muestra en el texto
        scoreText.text = "Puntuación: " + SwipeEffect.score.ToString();
    }
}
