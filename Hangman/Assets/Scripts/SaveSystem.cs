using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
   
    public static void SaveStats ( StatsData stats )
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/stats.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        StatsData data = stats;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void InitSave( Stats stats )
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/stats.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        StatsData data = new StatsData(stats);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static StatsData LoadStats()
    {
        string path = Application.persistentDataPath + "/stats.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            StatsData data = formatter.Deserialize(stream) as StatsData;

            if (data.bgUnlocked == null)
                data.bgUnlocked = new bool[11] { true, false, false, false, false, false, false, false, false, false, false }; // added for feedback version

            if (data.isApplied == null)
                data.isApplied = new bool[11] { true, false, false, false, false, false, false, false, false, false, false }; // added for feedback version
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }
}
