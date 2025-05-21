using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject Pausepanel;
    private bool isActive;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button QuitButton;
    [SerializeField] private Button RetryButton;

    // Start is called before the first frame update
    void Start()
    {
        Pausepanel.SetActive(false);
        ResumeButton.onClick.AddListener(Resume);
        QuitButton.onClick.AddListener(Quit);
        RetryButton.onClick.AddListener(Retry);
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isActive != true)
            {
                Pausepanel.SetActive(true);
                Time.timeScale = 0;
                isActive = true;
                Debug.Log("판넬 활성화");
            }
            else if(isActive == true)
            {
                Pausepanel.SetActive(false);
                Time.timeScale = 1;
                isActive = false;
                Debug.Log("판넬 비활성화");
            }
        }
    }

    void Resume()
    {
        Pausepanel.SetActive(false);
        isActive = false;
        Time.timeScale = 1;
    }

    void Quit()
    {
        SceneManager.LoadScene("SampleScene");
        isActive = false;
        Time.timeScale = 1;
    }

    void Retry()
    {
        SceneManager.LoadScene("GameScene");
        isActive = false;
        Time.timeScale = 1;
    }
}
