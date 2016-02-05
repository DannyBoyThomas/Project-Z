using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Linq;

[System.Serializable]
public class InventoryManager : MonoBehaviour
{
    public Inventory playerInventory = new Inventory();
    public GameObject WeaponHolder;

    public InventoryItem[] Hotbar = new InventoryItem[4];

    public void SwitchItem(int item)
    {
        if (WeaponHolder.transform.childCount > 0)
        {
            while (WeaponHolder.transform.childCount > 0)
            {
                Destroy(WeaponHolder.transform.GetChild(0));
            }
        }

        GameObject spawned = GameObject.Instantiate(Hotbar[item].gameObject) as GameObject;
        spawned.transform.parent = WeaponHolder.transform;
        spawned.transform.localPosition = Vector3.zero;
        spawned.transform.localRotation = Quaternion.identity;
        playerInventory.Equipped = Hotbar[item];

        Debug.Log("Switched : " + item);
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SwitchItem(3);

    }

    public bool UseRadialMenu = false;

    [ContextMenu("Save")]
    public void Save()
    {
        string location = "C:/Games/ProjectZ/Player.data";
        SaveLoad.Save(location, playerInventory);
        Debug.Log("Saved");
    }

    [ContextMenu("Load")]
    public void Load()
    {
        string location = "C:/Games/ProjectZ/Player.data";
        var obj = SaveLoad.Load<Inventory>(location);
        playerInventory = obj;
    }


}

