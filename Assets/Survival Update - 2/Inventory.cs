using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Inventory
{

    public Entity owner;
    public Item[] items;
   // public List<Item> items;
    public int size;
    public Inventory(Entity owner, int size)
    {
        this.owner = owner;
        items = new Item[size];
        this.size = size;
       
        
    }

    public Item getItem(int slot)
    {
       
        return items[slot];
    }
    public bool addItem(Item item)
    {
        int slot = findBestSlot(item);
       
        if (slot != -1)
        {
            item.onPickUp(owner);
            Item currentItem = getItem(slot);
            if (currentItem == null)
            {
                items[slot] = item;
            }
            else // slot occupied
            {
                items[slot] = mergeItems(currentItem, item);
            }

            return true;
        }
        return false;
        
    }
    int findFreeSlot()
    {
        
        for (int i = 0; i < items.Length; i++)
        {
            
            if (items[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
    int findBestSlot(Item item)
    {
        if (item == null || !item.canStack)
        {
            
            return findFreeSlot();
        }
        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                Item it = items[i];
                if (it != null)
                {
                    if (it.name == item.name)
                    {
                        return i;
                    }
                }
            }
        }
       
        return findFreeSlot();
       
    }
    Item mergeItems(params Item[] items)
    {
        int amount = 0;
        Item ret = null;
        foreach (Item i in items)
        {
            if (i != null)
            {
                ret = i;
                amount += i.amount;
            }
        }
        if (ret != null)
        {
            ret.amount = amount;
        }
            return ret;
        

    }
}
