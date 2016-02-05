using UnityEngine;

[System.Serializable]
public class Inventory
{
    public InventoryItem[] InventoryItems;
    public InventoryItem Equipped;
}

[System.Serializable]
public class InventoryItem
{
    public int ItemID = 0;
    public string unlocalizedName = "";
    public string localizedName = "";

    public int Quantity;
    public int MaxQuantity;

    public float Weight;

    public GameObject gameObject;

    public string GetLocalizedName()
    {
        return LanguageHandler.GetLocalizedName(unlocalizedName);
    }

    public float GetTotalWeight()
    {
        return Weight * Quantity;
    }

    public bool AddItem()
    {
        if (Quantity < MaxQuantity)
        {
            Quantity++;
            return true;
        }

        return false;
    }
}
