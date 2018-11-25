using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetManager : MonoBehaviour
{
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
        string message = "Vous avez perdu :( Score: " + ScoreManager.score.ToString();
        deathPanel.GetComponentInChildren<Text>().text = message;
        Time.timeScale = 0;
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        string message = "Vous avez gagné! Score: " + ScoreManager.score.ToString();
        winPanel.GetComponentInChildren<Text>().text = message;
        Time.timeScale = 0;
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0, LoadSceneMode.Single);

    }
}
