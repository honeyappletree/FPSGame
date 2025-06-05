using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private Button playButton; //게임 플레이 버튼
    [SerializeField] private Button tutorialButton; //튜토리얼 버튼
    [SerializeField] private GameObject tutorialImage; //튜토리얼 설명 이미지
    [SerializeField] private Button tutorialQuit; //튜토리얼 종료 버튼

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(GamePlay);
        tutorialButton.onClick.AddListener(GoTutorial);
        tutorialQuit.onClick.AddListener(QuitTutorial);
    }

    void GamePlay() //게임 플레이 버튼을 눌렀을 때
    {
        SceneManager.LoadScene("LoadingScene");
    }

    void GoTutorial() //튜토리얼 버튼을 눌렀을 때
    {
        tutorialImage.SetActive(true);
    }

    void QuitTutorial() //튜토리얼 종료 버튼을 눌렀을 때
    {
        tutorialImage.SetActive(false);
    }
}
