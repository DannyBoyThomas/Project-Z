using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Items {

    public static List<Item> items;
    public static Item Tomahawk, Grenade;
    public static void Initialise()
    {
        items = new List<Item>();
        Tomahawk = new Item("Tomahawk");
        Grenade = new Item("Grenade");
    }
}
