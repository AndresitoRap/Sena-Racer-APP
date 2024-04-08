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
    public TMP_Text Rank;

    // Start is called before the first frame update
    void Start()
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

            StartCoroutine(GetAllRunnersData(totalScore, totalTime));
        }
    }

    IEnumerator GetAllRunnersData(int userScore, float userTime)
    {
        string url = "https://backend-strapi-senaracer.onrender.com/api/runners";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            AllRunnersData allRunnersData = JsonUtility.FromJson<AllRunnersData>(json);

            if (allRunnersData != null && allRunnersData.data != null)
            {
                int userRank = 1;
                foreach (RunnerData runnerData in allRunnersData.data)
                {
                    if (runnerData != null && runnerData.data != null && runnerData.data.attributes != null)
                    {
                        int totalScore = int.Parse(runnerData.data.attributes.score1) + int.Parse(runnerData.data.attributes.score2) + int.Parse(runnerData.data.attributes.score3) + int.Parse(runnerData.data.attributes.score4) + int.Parse(runnerData.data.attributes.score5);
                        if (totalScore > userScore)
                        {
                            userRank++;
                        }
                    }
                }

                TotalScore.text = userScore.ToString() + "Pts";
                TotalTime.text =   userTime.ToString("F0") + "s";
                Rank.text = userRank + " de " + allRunnersData.data.Count;
            }
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
    public float time1;
    public float time2;
    public float time3;
    public float time4;
    public float time5;
}

[System.Serializable]
public class AllRunnersData
{
    public List<RunnerData> data;
}
