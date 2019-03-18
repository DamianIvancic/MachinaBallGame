using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager GM;
    public playerManager PM;
    public cameraController CamController;
    public MessageManager MM;
    
	void Start () {

        Application.targetFrameRate = 60;

        if (GM == null)
            GM = GetComponent<GameManager>();
    }
	
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
