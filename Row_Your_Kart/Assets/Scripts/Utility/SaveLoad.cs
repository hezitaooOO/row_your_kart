using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;


public class SaveLoad
{
    public static void SaveHighScore(int score)
    {
        string path = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if(File.Exists(path))
        {
            file = File.OpenWrite(path);
        }
        else
        {
            file = File.Create(path);
        }

        GameData data = new GameData(score);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public static int LoadHighScore()
    {
        string path = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(path))
        {
            file = File.OpenRead(path);
        }
        else
        {
            file = File.Create(path);
            return 0;
        }
        BinaryFormatter bf = new BinaryFormatter();

        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        return data.highScore;

    }


}
