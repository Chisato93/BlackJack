using System;
using UnityEngine;
using System.IO;

[Serializable] class SaveData
{
    public int Gold;
    public int Heart;
}

public class DataManager : MonoBehaviour
{
    #region -SINGLETON
    public static DataManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion

    public void SaveDataFun()
    {
        SaveData data = new SaveData();
        data.Gold = GameManager.instance.Gold;
        data.Heart = GameManager.instance.Heart;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        // UIController.instance.ShowSaveDataText();
    }

    public void LoadDataFun()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            GameManager.instance.Gold = data.Gold;
            GameManager.instance.Heart = data.Heart;
            UIController.instance.SetExchange();
        }

        else
        {
            GameManager.instance.Init();
        }
    }
}
