using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tips : MonoBehaviour
{
    public TMP_Text tipsText;
    public string[] tips =
    {
        "Hay códigos QR escondidos, encuéntralos",
        "Responde lo más rápido y sabiamente posible",
        "En una vaca podría haber un QR escondido, tal vez",
        "Hazlo lo más rápido que puedas.",
    };
    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(ChangeTipAfterDelay(3f));
    }

    IEnumerator ChangeTipAfterDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            currentIndex = (currentIndex + 1) % tips.Length;
            tipsText.text = tips[currentIndex];
        }
    }
}