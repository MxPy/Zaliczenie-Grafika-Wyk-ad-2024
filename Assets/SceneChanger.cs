using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string prevScene = "";
    public string nextScene = "";


    // Update is called once per frame
    void Update()
    {
        if((prevScene != "") && Input.GetKeyDown(KeyCode.Q)){
            SceneManager.LoadScene(prevScene);
        }
        else if((nextScene != "") && Input.GetKeyDown(KeyCode.E)){
            SceneManager.LoadScene(nextScene);
        }
        
    }
}
