using UnityEngine;

public class Object3D : MonoBehaviour
{
    public Progress progress; // Asigna el script de progreso desde el Editor Unity

    void OnMouseDown()
    {
        // Recupera el progreso actual de PlayerPrefs
        int visitedCount = PlayerPrefs.GetInt("VisitedCount", 0);
        int totalCount = PlayerPrefs.GetInt("TotalCount", 0);

        // Incrementa el conteo de visitas
        visitedCount++;

        // Actualiza el progreso
        progress.UpdateProgress(visitedCount, totalCount);

        // Carga la siguiente escena (aquí debes poner el nombre de tu escena)
        UnityEngine.SceneManagement.SceneManager.LoadScene("NombreDeTuEscena");
    }
}
