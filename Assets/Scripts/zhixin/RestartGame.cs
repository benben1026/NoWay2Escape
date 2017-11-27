using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {

   	    public void Reload()
     {
         int scene = SceneManager.GetActiveScene().buildIndex;
         SceneManager.LoadScene(scene, LoadSceneMode.Single);
         Time.timeScale = 1;
     }
}
