using UnityEngine;
using System.Collections;

public class Item  
{
    public string name = "Item";
    public int amount =1;
    public bool canStack = true;
    public Texture icon;
    public Sprite sprite;
    public GameObject model;
    public Item(string name)
    {
        this.name = name;
        icon = (Texture)Resources.Load("Icons/" + name);
        sprite = Resources.Load<Sprite>("Icons/" + name);
    }

    public virtual void useItem()
    {
        
    }
    public virtual void onPickUp(Entity entity)
    {

    }
    public virtual void playAnimation()
    {
        Animation ani = getAnimation();
        if (ani != null)
        {
            //play animation
        }
    }
    public virtual Animation getAnimation()
    {
        return null;
    }
	
}
