using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneRA : MonoBehaviour
{
    public void ChangeScene(string SceneRAorMap) {
        SceneManager.LoadScene(SceneRAorMap);
    }
}
