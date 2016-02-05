using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class SaveLoad
{
    public const string BasePath = "C:/Games/ProjectZ/";
    public static string WorldPath { get { return BasePath + "/Worlds/"; } }

    public static void Save(string path, object obj)
    {
        Directory.CreateDirectory("C:/Games/ProjectZ");
        Directory.CreateDirectory("C:/Games/ProjectZ/Worlds");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);
        bf.Serialize(file, JsonUtility.ToJson(obj));
        file.Close();
        file.Dispose();
        Debug.Log("Saved");
    }

    public static void CreateDirectory(string path)
    {
        Directory.CreateDirectory(path);
    }

    public static T Load<T>(string path)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);
        string text = bf.Deserialize(file) as string;
        file.Close();
        file.Dispose();
        return (T)JsonUtility.FromJson<T>(text);
    }
}
