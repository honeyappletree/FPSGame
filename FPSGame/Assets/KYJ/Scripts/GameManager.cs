using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float time = 120;
    private float currentTime;
    private int minute;
    private int second;
    [SerializeField] private GameObject resultPanel;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(GameTimer());
    }

    void Start()
    {
        resultPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
