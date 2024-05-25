using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsHandler : MonoBehaviour
{
    public GameObject scoreBoardGO;
    public GameObject saveRecordGO;
    public InputField playerNameText;
    public GameObject pausePanel;

    private GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        //SceneManager.LoadScene("MainMenu");
        Application.Quit();
    }

    public void CloseScoreBoard()
    {
        scoreBoardGO.SetActive(false);
    }

    public void OpenScoreBoard()
    {
        scoreBoardGO.SetActive(true);
    }

    public void SaveRecord()
    {
        // Save score to list
        PlayerScore ps = new PlayerScore();
        ps.playerName = playerNameText.text;
        ps.time = FindObjectOfType<TimeManager>().GetClearedTime();
        gm.AddScore(ps);
        gm.SortScoreList();
        gm.GenerateScoreBoard();
        saveRecordGO.SetActive(false);
        OpenScoreBoard();
    }

    public void CloseSaveRecordPanel()
    {
        gm.SortScoreList();
        gm.GenerateScoreBoard();
        saveRecordGO.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

}
