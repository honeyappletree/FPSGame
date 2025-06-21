using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] private Button finishButton;

    // Start is called before the first frame update
    void Start()
    {
        finishButton.onClick.AddListener(GameFinish);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameFinish()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
        Debug.Log("데이터를 불러왔습니다.");
    }
}
