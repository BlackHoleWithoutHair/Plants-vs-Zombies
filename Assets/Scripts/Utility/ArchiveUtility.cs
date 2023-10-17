using System.IO;
using UnityEngine;

public class ArchiveUtility : Singleton<ArchiveUtility>
{
    private static string fileName = "PVZ ArchiveData";
    private ArchiveUtility() { }
    public void SaveData<T>(T data)
    {
        switch (typeof(T).Name)
        {
            case nameof(GameData):
                string json = JsonUtility.ToJson(data);
                string path = Path.Combine(Application.persistentDataPath, fileName + ".txt");
                File.WriteAllText(path, json);
                Debug.Log("Success to SaveData");
                break;
        }
    }
    public GameData GetData()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + ".txt");
        if (!File.Exists(path))
        {
            GameData data = GetDefaultData();
            SaveData(data);
        }
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<GameData>(json);
    }
    private GameData GetDefaultData()
    {
        GameData data = new GameData();
        data.UserName = null;
        data.NameList = null;
        data.MusicVolume = 0.8f;
        data.SoundVolume = 0.8f;
        data.StageId = 7;
        return data;
    }




#if UNITY_EDITOR
    [UnityEditor.MenuItem("MyCommand/InitializeArchive")]
#endif
    public static void InitializeData()
    {
        Instance.SaveData(Instance.GetDefaultData());
    }
#if UNITY_EDITOR
    [UnityEditor.MenuItem("MyCommand/DeleteArchive")]
#endif
    public static void DeleteDataFile()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + ".txt");
        File.Delete(path);
        Debug.Log("Already Delete Data");
    }
}
