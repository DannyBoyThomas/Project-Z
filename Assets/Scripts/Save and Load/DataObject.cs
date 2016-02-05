using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class DataObject
{
    public int ID;
    public string ResourceName;

    public ObjectData[] savedData;

    public void Save()
    {
        SaveLoad.Save(SaveLoad.WorldPath + "Data" + ID, this);
    }

    public void AddSavedData(string name, object value)
    {
        List<ObjectData> data = savedData.ToList<ObjectData>();
        bool exists = false;

        foreach (ObjectData objectData in savedData)
        {
            if (objectData.fieldName.ToUpper() == name.ToUpper())
            {
                objectData.savedData = JsonUtility.ToJson(value);
                exists = true;
            }
        }

        if(!exists)
            data.Add(new ObjectData { fieldName = name, savedData = JsonUtility.ToJson(value) });

        savedData = data.ToArray();
    }

    public string GetSavedData(string name)
    {
        foreach (ObjectData data in savedData)
        {
            if (data.fieldName.ToUpper() == name.ToUpper())
            {
                return data.savedData;
            }
        }

        return string.Empty;
    }

}
[System.Serializable]
public class ObjectData
{
    public string fieldName;
    public string savedData;
}