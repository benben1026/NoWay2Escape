using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMenu : MonoBehaviour {

    // Use this for initialization
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
