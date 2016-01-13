using UnityEngine;
using System.Collections;

public class Player : Entity {

    public InventoryPlayer inventory;
    public int credits =0;
    
	void Start () 
    {
        inventory = new InventoryPlayer(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
