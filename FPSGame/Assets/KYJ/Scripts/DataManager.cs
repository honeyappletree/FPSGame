using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public static string path;
    public static SaveData saveData;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/playerdata.json";
        JsonLoad();
        Debug.Log("저장 경로: " + path);
    }

    public void JsonLoad()
    {
        SaveData saveData = new SaveData();

        if(File.Exists(path) == false)
        {
            GameManager.instance.playerSec = 0;
            EnemyFSM.dieEnemy = 0;
            JsonSave();
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if(saveData != null)
            {
                EnemyFSM.dieEnemy = saveData.bestEnemyHit;
                TutorialFSM.doneTutorial = saveData.tutorial;
                GameManager.instance.playerSec = saveData.bestPlaytime;
                GameManager.instance.perfectGame = saveData.gameClear;
            }
        }
    }

    public void JsonSave()
    {
        SaveData saveData = new SaveData();

        saveData.tutorial = TutorialFSM.doneTutorial;
        saveData.gameClear = GameManager.instance.perfectGame;

        if(saveData.gameClear == 1)
        {
            saveData.bestPlaytime = GameManager.instance.playerSec;
            saveData.bestEnemyHit = EnemyFSM.dieEnemy;
        }
        else if(saveData.gameClear >= 2)
        {
            if(EnemyFSM.dieEnemy > saveData.bestEnemyHit)
            {
                saveData.bestEnemyHit = EnemyFSM.dieEnemy;
                saveData.bestPlaytime = GameManager.instance.playerSec;
            }
        }

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class SaveData
{
    public bool tutorial;
    public int bestEnemyHit;
    public float bestPlaytime;
    public int gameClear;
}
