using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRSceneChange : MonoBehaviour
{
    
    public void LaunchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
