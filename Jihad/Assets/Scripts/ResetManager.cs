using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetManager : MonoBehaviour {
    public GameObject deathPanel;
    public GameObject winPanel;
    public Button ResetButton;

    private void Awake()
    {
        // Button = ResetButton.GetComponent<FlexibleUIButton>();
    }
    private void Start()
    {
        ResetButton.onClick.AddListener(ResetGame);
    }

    public void ShowDeathPanel()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        winPanel.GetComponentInChildren<Text>().text = "Vous avez gagné! Score: " + ScoreManager.score;
        Time.timeScale = 0;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);

    }
}
