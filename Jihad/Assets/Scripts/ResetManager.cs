using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetManager : MonoBehaviour {
    public GameObject endPanel;
	
    public void ShowEndPanel()
    {
        endPanel.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
