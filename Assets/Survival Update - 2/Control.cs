using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

    public Player player;
    public GameObject playerObject;
    bool inventoryOpen = false;
	void Start () 
    {
       
        player = playerObject.GetComponent<Player>();
        preInititalise();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inventoryOpen)
            {
                GuiInventory.remove();
                inventoryOpen = false;
            }
            else
            {
                GuiInventory.draw(player.inventory);
                inventoryOpen = true;
            }
        }
	}
    void preInititalise()
    {
        Items.Initialise();
    }
    void Initialise()
    {

    }
}
