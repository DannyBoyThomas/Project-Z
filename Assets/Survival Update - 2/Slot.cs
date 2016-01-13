using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slot:MonoBehaviour {

    public Item item = null;

    public void Start()
    {
        Image img = transform.Find("ItemParent").GetComponent<Image>();
        if (item != null)
        {
            img.sprite = item.sprite;
        }
    }

    public void Update()
    {
        
    }
    public void OnMouseClick()
    {
       
    }
}
