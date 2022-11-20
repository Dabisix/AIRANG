using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SavedData
{
    public Dictionary<int, int> page_checkPoint = new Dictionary<int, int>();

    public bool language = true;

    public int narration = 1; // 0 : none, 1 : default, 2 : recorded
}

public class FileManager : MonoBehaviour
{
    private const string SAVE_FILE_NAME = "saved";

    private static FileManager instance = null;

    // data to access
    private SavedData savedData = null;

    // singleton pattern implemented
    public static FileManager getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if(this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public SavedData loadData()
    {
        string filePath = Application.persistentDataPath + SAVE_FILE_NAME;

        if (savedData == null)
        {
            // validate file exists
            if(File.Exists(filePath))
            {
                // load from file
                string fileStream = File.ReadAllText(filePath);
                savedData = JsonUtility.FromJson<SavedData>(fileStream);
            } else
            {
                savedData = new SavedData();
                saveData(savedData);
            }
        }

        return savedData;
    }

    public void saveData(SavedData savedData)
    {
        string filePath = Application.persistentDataPath + SAVE_FILE_NAME;

        File.WriteAllText(filePath, JsonUtility.ToJson(savedData));
    }
}
