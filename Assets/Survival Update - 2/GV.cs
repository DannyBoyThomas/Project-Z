using UnityEngine;
using System.Collections;

public class GV :MonoBehaviour {

    public static GameObject canvas;
    public GameObject canvas0;
    public static GameObject invSlot;
    public GameObject invSlot0;
    public static GameObject invPanel;
    public void Start()
    {
        canvas = canvas0;
        
        invSlot = invSlot0;
        invPanel = GameObject.Find("InventoryPanelMoveable");
        canvas.SetActive(false);
    }

}
