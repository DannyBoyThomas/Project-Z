using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiInventory {

    
  //  public GameObject invPanel;
    public static void draw(Inventory inv)
    {
        GV.canvas.SetActive(true);
        GameObject canvas = GV.canvas;
        GameObject slot = GV.invSlot;
        int size = inv.size;
        GameObject invPanel = GV.invPanel;
        Player player = inv.owner.GetComponent<Player>();
        string playerName = player.name;
        GameObject namePanel =  getNamePanel(canvas);
        namePanel.transform.Find("Name").GetComponent<Text>().text = playerName;
        namePanel.transform.Find("Credit").GetComponent<Text>().text = "Credits: " + player.credits;
     
       
        for (int i = 0; i < size; i++)
        {
            Item item = inv.getItem(i);
            GameObject newSlot = (GameObject)GameObject.Instantiate(slot);
            newSlot.GetComponent<Slot>().item = item;
            newSlot.transform.SetParent(invPanel.transform);
        }
        
    }
    public static void remove()
    {
        for (int i = 0; i < GV.invPanel.transform.childCount; i++)
        {
            GameObject g = GV.invPanel.transform.GetChild(i).gameObject;
            GameObject.Destroy(g);
        }
        GV.canvas.SetActive(false);
       
    }
    static GameObject getNamePanel(GameObject inv)
    {
        return inv.transform.Find("InventoryMain").Find("PlayerNamePanel").gameObject;
    }
}
