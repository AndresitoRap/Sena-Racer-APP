using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingManager : MonoBehaviour
{
    private void Awake()
    {
        // Cambiar la variable RankingManager a true
        PlayerPrefs.SetInt("RankingManager", 1);
        PlayerPrefs.Save();
    }
}
