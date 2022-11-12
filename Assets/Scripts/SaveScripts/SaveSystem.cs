using UnityEngine;
using System.IO; //namespace we want to use when working with files on Operating System
using System.Runtime.Serialization.Formatters.Binary; //allows access to BinaryFormatter

public static class SaveSystem //static class is just a class that can't be instantiated // don't want multiple
{
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "saves"); //saves will be the binary file name
        Debug.Log($"Save Path = {Path.Combine(Application.persistentDataPath, "saves")}");
        //FileStream allows us to read and write from a file, from System.IO class
        using (FileStream stream = new FileStream(path, FileMode.Create)) //stream is the data, path is path with file name, create creates
        {
            PlayerData data = new PlayerData(player);

            formatter.Serialize(stream, data); //creates a "stream" of bytes from the object "data"
            Debug.Log("GAME SAVED!");
        }
        //stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Path.Combine(Application.persistentDataPath, "saves"); //opens binary file saves from the path//you include filename in path
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open)) //opening the "stream" from "path"
            {
                PlayerData data = formatter.Deserialize(stream) as PlayerData; //deserializing the stream of bytes AS a PLAYERDATA class
                return data;
            }
            //stream.Close();
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}