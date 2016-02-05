using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TileEntity : MonoBehaviour, ISaveable
{
    public DataObject dataObject = new DataObject();

    public virtual void Start()
    {
        FindObjectOfType<World>().tileEntites.Add(this);
    }

    public virtual void Load()
    {
        string rawPos = dataObject.GetSavedData("position");
        string rawRot = dataObject.GetSavedData("rotation");

        Vector3 pos = JsonUtility.FromJson<Vector3>(rawPos);
        Quaternion rot = JsonUtility.FromJson<Quaternion>(rawRot);

        transform.position = pos;
        transform.rotation = rot;
    }

    public virtual void Save()
    {
        dataObject.AddSavedData("position", transform.position);
        dataObject.AddSavedData("rotation", transform.rotation);
    }
}
