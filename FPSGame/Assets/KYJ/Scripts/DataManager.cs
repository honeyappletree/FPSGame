using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public bool tutorial;
    public int bestEnemyHit;
    public float bestPlaytime;
}

public class DataManager : MonoBehaviour
{
    string path;

    // Start is called before the first frame update
    void Start()
    {
        path = Path.Combine(Application.dataPath, "database.json");
        JsonLoad();
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
            }
        }
    }

    public void JsonSave()
    {
        SaveData saveData = new SaveData();

        saveData.tutorial = TutorialFSM.doneTutorial;
        if(saveData.bestPlaytime > GameManager.instance.playerSec)
        {
            saveData.bestPlaytime = GameManager.instance.playerSec;
        }
        if(saveData.bestEnemyHit < EnemyFSM.dieEnemy)
        {
            saveData.bestEnemyHit = EnemyFSM.dieEnemy;
        }

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
