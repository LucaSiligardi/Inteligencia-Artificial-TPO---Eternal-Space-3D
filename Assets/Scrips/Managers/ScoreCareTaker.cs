using System.IO;
using UnityEngine;

public static class ScoreCareTaker
{
    private static string path = Application.persistentDataPath + "/score.json";

    public static void Save(ScoreMemento memento)
    {
        string json = JsonUtility.ToJson(memento, true);
        File.WriteAllText(path, json);
    }

    public static ScoreMemento Load()
    {
        if (!File.Exists(path))
        {
            return new ScoreMemento(0);
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<ScoreMemento>(json);
    }
}