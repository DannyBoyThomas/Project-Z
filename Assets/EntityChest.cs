using UnityEngine;
using System.Collections;
using System;

public class EntityChest : TileEntity, ISaveable
{
    public Inventory inventory;

    public override void Load()
    {
        base.Load();
        string rawInventory = dataObject.GetSavedData("inventory");
        inventory = JsonUtility.FromJson<Inventory>(rawInventory);
    }

    public override void Save()
    {
        base.Save();
        dataObject.ResourceName = "EntityChest";
        dataObject.AddSavedData("inventory", inventory);
    }
}
