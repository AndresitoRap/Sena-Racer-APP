using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class FinallyResults : MonoBehaviour
{
    public TMP_Text TotalScore;
    public TMP_Text TotalTime;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateData", 0f, 300f);
    }

    void UpdateData()
    {
        StartCoroutine(GetUserData());
    }

    IEnumerator GetUserData()
    {
        string url = "https://backend-strapi-senaracer.onrender.com/api/runners/" + Login.corridorID;
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            RunnerData runnerData = JsonUtility.FromJson<RunnerData>(json);

            int totalScore = int.Parse(runnerData.data.attributes.score1) + int.Parse(runnerData.data.attributes.score2) + int.Parse(runnerData.data.attributes.score3) + int.Parse(runnerData.data.attributes.score4) + int.Parse(runnerData.data.attributes.score5);
            float totalTime = runnerData.data.attributes.time1 + runnerData.data.attributes.time2 + runnerData.data.attributes.time3 + runnerData.data.attributes.time4 + runnerData.data.attributes.time5;

            StartCoroutine(UpdateTotalScoreAndTime(Login.corridorID, totalScore, totalTime));
        }
    }

    IEnumerator UpdateTotalScoreAndTime(string runnerID, int totalScore, float totalTime)
    {
        string url = "https://backend-strapi-senaracer.onrender.com/api/runners/" + runnerID;

        string json = "{\"data\": {\"totalScore\":\"" + totalScore + "\",\"totalTime\":" + totalTime + "}}";

        UnityWebRequest www = new UnityWebRequest(url, "PUT");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            TotalScore.text = totalScore.ToString() + "Pts";
            TotalTime.text = totalTime.ToString("F0") + "s";
        }
    }
}

[System.Serializable]
public class RunnerData
{
    public Data data;
}

[System.Serializable]
public class Data
{
    public Attributes attributes;
}

[System.Serializable]
public class Attributes
{
    public string score1;
    public string score2;
    public string score3;
    public string score4;
    public string score5;
    public string totalScore;
    public float time1;
    public float time2;
    public float time3;
    public float time4;
    public float time5;
    public float totalTime;
}
