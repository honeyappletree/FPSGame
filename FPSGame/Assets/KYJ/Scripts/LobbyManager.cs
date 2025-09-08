using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private Button playButton; //게임 플레이 버튼
    [SerializeField] private Button tutorialButton; //튜토리얼 버튼
    [SerializeField] private Button undoButton; //게임 첫화면 씬으로 돌아가는 버튼

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(GamePlay);
        tutorialButton.onClick.AddListener(GoTutorial);
        undoButton.onClick.AddListener(Undo);
    }

    void GamePlay() //게임 플레이 버튼을 눌렀을 때
    {
        SceneManager.LoadScene("LoadingScene");
    }

    void GoTutorial() //튜토리얼 버튼을 눌렀을 때
    {
        SceneManager.LoadScene("TutorialScene");
    }

    void Undo() //게임 첫화면 씬으로 돌아가는 버튼을 눌렀을 때
    {
        SceneManager.LoadScene("SampleScene");
    }

}
