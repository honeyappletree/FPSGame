using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject Pausepanel;
    private int escapeCount;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button QuitButton;
    [SerializeField] private Button RetryButton;

    // Start is called before the first frame update
    void Start()
    {
        Pausepanel.SetActive(false);
        escapeCount = 0;
        ResumeButton.onClick.AddListener(Resume);
        QuitButton.onClick.AddListener(Quit);
        RetryButton.onClick.AddListener(Retry);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(escapeCount == 0)
            {
                Pausepanel.SetActive(true);
                escapeCount++;
                Time.timeScale = 0;
                Debug.Log(escapeCount);
            }
            else if(escapeCount == 1)
            {
                Pausepanel.SetActive(false);
                escapeCount--;
                Time.timeScale = 1;
                Debug.Log(escapeCount);
            }
        }
    }

    void Resume()
    {
        Pausepanel.SetActive(false);
        Time.timeScale = 1;
    }

    void Quit()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

    void Retry()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }
}
