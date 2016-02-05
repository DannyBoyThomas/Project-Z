using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class World : MonoBehaviour
{
    public string worldName;

    public List<Entity> entites = new List<Entity>();
    public List<TileEntity> tileEntites = new List<TileEntity>();
    
    [System.Serializable]
    public class WorldDataHolder
    {
        public DataObject[] dataObjects;
    }

    public WorldDataHolder dataHolder = new WorldDataHolder();

    [ContextMenu("Save")]
    public void Save()
    {
        List<DataObject> data = new List<DataObject>();

        for (int i = 0; i < tileEntites.Count; i++)
        {
            TileEntity entity = tileEntites[i];
            entity.Save();
            //entity.dataObject.ID = i;
            //entity.dataObject.Save();
            data.Add(entity.dataObject);
        }

         dataHolder.dataObjects = data.ToArray();

        SaveLoad.CreateDirectory(SaveLoad.WorldPath + worldName);
        SaveLoad.Save(SaveLoad.WorldPath + worldName + "/Entities.txt", dataHolder);

    }

    [ContextMenu("Load")]
    public void Load()
    {
        dataHolder = SaveLoad.Load<WorldDataHolder>(SaveLoad.WorldPath + worldName +  "/Entities.txt");


        for (int i = 0; i < dataHolder.dataObjects.Length; i++)
        {
            string resourceName = dataHolder.dataObjects[i].ResourceName;
            GameObject go = Resources.Load<GameObject>("Objects/" + resourceName);
            GameObject spawned = Instantiate(go) as GameObject;
            TileEntity entity = spawned.GetComponent<TileEntity>();
            entity.dataObject = dataHolder.dataObjects[i];
            entity.Load();
        }

    }


}
