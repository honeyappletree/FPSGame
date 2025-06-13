using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI timerText;
    private float time = 150; //제한 시간: 2분 30초
    private float currentTime;
    private int minute;
    private int second;
    [SerializeField] private GameObject resultPanel;
    private float playerMin = 0;
    public float playerSec = 0;
    [SerializeField] TextMeshProUGUI currentTimeText; //플레이 타임을 표시하기 위한 텍스트
    [SerializeField] TextMeshProUGUI bestTimeText; //최단 클리어 기록을 표시하기 위한 텍스트
    [SerializeField] TextMeshProUGUI bestEnemyText; //많이 물리친 적의 수를 표시하기 위한 텍스트
    private bool gameOver = false;
    public int perfectGame = 0;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        StartCoroutine(GameTimer());
    }

    void Start()
    {
        resultPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerSec += Time.deltaTime;
        if(playerSec > 59)
        {
            playerMin++; //sec가 59보다 크게 된다면 1분 이상 시간이 흘렀다는 얘기인데 sec가 1분이 됐을 떄 0으로 정리해주면 자연스럽게 바꿀 수 있음
            playerSec = 0;
        }
        currentTimeText.text = playerMin.ToString("00") + ":" + playerSec.ToString("00"); //플레이 타임을 측정하기 위함.

        if(PlayerHP.currentHP > 0)
        {
            gameOver = false;
            resultPanel.SetActive(false);
        }
        else if(PlayerHP.currentHP <= 0)
        {
            gameOver = true;
            resultPanel.SetActive(true);
        }
        GameClear();

    }

    void GameClear()
    {
        if(EnemyFSM.dieEnemy == 2 && PlayerHP.currentHP >= 0)
        {
            resultPanel.SetActive(true);
            Time.timeScale = 0;
            perfectGame++;
            Debug.Log("perfectGame: " + perfectGame);
        }
    }

    IEnumerator GameTimer()
    {
        currentTime = time;

        while (currentTime >= 0)
        {
            currentTime -= Time.deltaTime;

            minute = (int)currentTime / 60; //currentTime을 60으로 나눈 값의 몫을 표시함.
            second = (int)currentTime % 60; //currentTime을 60으로 나눈 값의 나머지를 표시함.

            timerText.text = minute.ToString("00") + ":" + second.ToString("00");
            yield return null;

            if (currentTime <= 31) //30초 이하면 텍스트가 빨간색으로 변하도록 함.
            {
                timerText.color = Color.red;
            }

            if (currentTime <= 0)
            {
                resultPanel.SetActive(true);
                Time.timeScale = 0;

                yield break;
            }
            
        }
        
    }

}
