using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private GameObject quitPanel;
    [SerializeField] private Button exitBtn;
    [SerializeField] private Button resumeBtn;

    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(GoLobby);
        quitBtn.onClick.AddListener(ActiveQuit);
        exitBtn.onClick.AddListener(GameExit);
        resumeBtn.onClick.AddListener(ResumeGame);
    }

    void GoLobby()
    {
        Time.timeScale = 1;
        if (TutorialFSM.doneTutorial == false)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else
        {
            SceneManager.LoadScene("LobbyScene");
        }
    }
    
    void ActiveQuit()
    {
        quitPanel.SetActive(true);
        Time.timeScale = 0;
    }

    void GameExit()
    {
        Application.Quit();
        Time.timeScale = 1;
        Debug.Log("게임 종료");
    }

    void ResumeGame()
    {
        quitPanel.SetActive(false);
        Time.timeScale = 1;
    }


}
