using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class RunnerAttributes
{
    public string identification;
    public string name;
    public string totalScore;
}

[System.Serializable]
public class Runner
{
    public RunnerAttributes attributes;
}

[System.Serializable]
public class Root
{
    public Runner[] data;
}

public class Leaderboard : MonoBehaviour
{
    public GameObject runnerRowPrefab;
    public Transform table;

    private string url = "https://backend-strapi-senaracer.onrender.com/api/runners";

    private void Start()
    {
        StartCoroutine(GetData());
    }

    private IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var runners = JsonUtility.FromJson<Root>(request.downloadHandler.text).data;
            var orderedRunners = runners.OrderByDescending(r => int.Parse(r.attributes.totalScore)).ToArray();

            UpdateTable(orderedRunners);
        }
        else
        {
            Debug.Log(request.error);
        }
    }

    private void UpdateTable(Runner[] runners)
    {
        // Elimina las filas existentes
        foreach (Transform child in table)
        {
            Destroy(child.gameObject);
        }

        // Agrega nuevas filas
        for (int i = 0; i < runners.Length; i++)
        {
            var row = Instantiate(runnerRowPrefab, table);
            var textFields = row.GetComponentsInChildren<TextMeshProUGUI>();

            textFields[0].text = (i + 1).ToString();
            textFields[1].text = runners[i].attributes.identification;
            textFields[2].text = runners[i].attributes.name;
            textFields[3].text = runners[i].attributes.totalScore;
        }
    }
}
