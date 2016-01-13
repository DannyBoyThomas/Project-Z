using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryPlayer : Inventory
{
    public class ItemLink
    {
        int keyNum;
        Item item;
        public ItemLink(int keyNum, Item item)
        {
            if(item != null)
            {
            this.keyNum = keyNum;
            this.item = item;
            }
        }
    }
    public List<ItemLink> itemLinks; //use keyboard numbers to select paired item
    public Inventory throwables;
    public InventoryPlayer(Entity owner, int size = 24) :base(owner,size)
    {
        throwables =new Inventory(owner, 2);
        itemLinks = new List<ItemLink>(5); //load from prev save

        addItem(Items.Tomahawk);
        addItem(Items.Grenade);
  
    }

}
