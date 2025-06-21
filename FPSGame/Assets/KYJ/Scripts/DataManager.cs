using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Enumeration;

[System.Serializable]
public class PlayerData
{
    public bool tutorial; //튜토리얼 클리어 여부
    public int bestEnemyHit = 0; //제일 많이 무찌른 적의 수
    public float bestPlaytime = 0; //제일 빠르게 게임을 클리어한 시간
    public int gameClear = 0; //게임을 클리어했는지 여부
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public PlayerData player1 = new PlayerData();
    public string path; //파일 경로를 지정해주기 위함

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/save"; //유니티가 알아서 현재 프로젝트의 개별 경로를 할당함.
        Debug.Log(path);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SaveData() //데이터 저장하기
    {
        string playData = JsonUtility.ToJson(player1);
        File.WriteAllText(path, playData);
    }

    public void LoadData() //데이터 불러오기
    {
        string playData = File.ReadAllText(path);
        player1 = JsonUtility.FromJson<PlayerData>(playData);
    }
}
