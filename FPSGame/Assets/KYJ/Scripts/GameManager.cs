using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] float totalTime = 90f;
    private float startTime;
    private float endTime;
    int bestMin;
    int bestSec;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI bestTimeText;
    [SerializeField] TextMeshProUGUI bestEnemyText;
    [SerializeField] TextMeshProUGUI currentTimeText;

    [SerializeField] GameObject resultPanel;
    int perfectGame = 0;

    private bool gameEnded = false;
    [SerializeField] private AudioClip gameSound;

    void Awake()
    {
        instance = this;
        DataManager.instance.LoadData();
    }

    void Start()
    {
        resultPanel.SetActive(false);
        StartCoroutine(StartTimer());
        Cursor.visible = false;
        GameStart.audioSource = GetComponent<AudioSource>();
        GameStart.audioSource.clip = gameSound;
        GameStart.audioSource.Play();
    }

    IEnumerator StartTimer()
    {
        yield return null; // 한 프레임 대기 (씬 완전 로딩 후 정확한 시간 설정)
        startTime = Time.time;
        endTime = startTime + totalTime;

        while (Time.time < endTime && !gameEnded)
        {
            float remaining = endTime - Time.time;
            int min = Mathf.FloorToInt(remaining / 60f);
            int sec = Mathf.FloorToInt(remaining % 60f);

            timerText.text = min.ToString("00") + ":" + sec.ToString("00");

            if (remaining <= 30f)
                timerText.color = Color.red;

            yield return null;
        }

        EndGame(); // 시간 다 되면 자동 종료
    }

    public void EndGame()
    {
        if (gameEnded) return; // 중복 방지
        gameEnded = true;

        resultPanel.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;

        float elapsed = Time.time - startTime;
        int playMin = Mathf.FloorToInt(elapsed / 60f);
        int playSec = Mathf.FloorToInt(elapsed % 60f);
        currentTimeText.text = playMin.ToString("00") + ":" + playSec.ToString("00");

        // 최고 기록 비교 및 저장
        if (DataManager.instance.player1.bestPlaytime == 0f || elapsed < DataManager.instance.player1.bestPlaytime || EnemyFSM.dieEnemy > DataManager.instance.player1.bestEnemyHit)
        {
            DataManager.instance.player1.bestPlaytime = elapsed;
            DataManager.instance.SaveData();
        }
        if(DataManager.instance.player1.bestEnemyHit == 0 || EnemyFSM.dieEnemy > DataManager.instance.player1.bestEnemyHit)
        {
            DataManager.instance.player1.bestEnemyHit = EnemyFSM.dieEnemy;
            DataManager.instance.SaveData();
        }

        // 텍스트 갱신
        bestMin = Mathf.FloorToInt(DataManager.instance.player1.bestPlaytime / 60f);
        bestSec = Mathf.FloorToInt(DataManager.instance.player1.bestPlaytime % 60f);
        bestTimeText.text = bestMin.ToString("00") + ":" + bestSec.ToString("00");
        int bestEnemy = DataManager.instance.player1.bestEnemyHit;
        bestEnemyText.text = bestEnemy.ToString();
    }

    public void ManualGameClear() // 적 처치 등으로 수동 종료할 경우 호출
    {
        EndGame();
    }
}