using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreenBarSystem : MonoBehaviour
{
    public GameObject bar;
    [SerializeField] private TextMeshProUGUI loadingText;
    public bool backGroundImageAndLoop;
    public float LoopTime;
    public GameObject[] backgroundImages;
    [Range(0, 1f)] public float vignetteEfectVolue;
    private Image vignetteEfect;

    public void loadingScreen(int sceneNo)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(Loading(sceneNo));
    }

    private void Start()
    {
        vignetteEfect = transform.Find("VignetteEfect").GetComponent<Image>();
        vignetteEfect.color = new Color(vignetteEfect.color.r, vignetteEfect.color.g, vignetteEfect.color.b, vignetteEfectVolue);

        if (backGroundImageAndLoop)
            StartCoroutine(transitionImage());
    }

    IEnumerator transitionImage()
    {
        // Mezcla el orden de las imágenes
        for (int i = 0; i < backgroundImages.Length; i++)
        {
            GameObject temp = backgroundImages[i];
            int randomIndex = Random.Range(i, backgroundImages.Length);
            backgroundImages[i] = backgroundImages[randomIndex];
            backgroundImages[randomIndex] = temp;
        }

        while (true) // Cambia el tiempo de espera a 3 segundos
        {
            foreach (GameObject image in backgroundImages)
            {
                foreach (GameObject bgImage in backgroundImages)
                    bgImage.SetActive(false);
                image.SetActive(true);
                yield return new WaitForSeconds(2);
            }
        }
    }

    IEnumerator Loading(int sceneNo)
    {
        float timer = 0;
        float loadTime = 5.0f;

        while (timer < loadTime)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / loadTime);

            bar.transform.localScale = new Vector3(progress, 0.9f, 1);

            if (loadingText != null)
                loadingText.text = "%" + (100 * progress).ToString("0");

            yield return null;
        }

        bar.transform.localScale = new Vector3(1, 0.9f, 1);
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneNo);
    }
}
