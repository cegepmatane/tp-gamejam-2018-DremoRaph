using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetManager : MonoBehaviour {
    public GameObject endPanel;
    public Button ResetButton;
    

    private void Awake()
    {
       // Button = ResetButton.GetComponent<FlexibleUIButton>();
    }
    private void Start()
    {
        ResetButton.onClick.AddListener(ResetGame);
    }

    public void ShowEndPanel()
    {
        endPanel.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);

    }
}
