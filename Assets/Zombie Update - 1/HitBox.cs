using UnityEngine;
using System.Collections;

public class HitBox : MonoBehaviour {

    public bool hitHead = false;
	void Start () {

       
	}
	
	// Update is called once per frame
	void Update () {
        if (hitHead)
        {
            HitHead(null);
            hitHead = false;

        }
	}
    public void HitHead(Entity attacker, int damage = 50)
    {
        Entity entity = transform.parent.parent.GetComponent<Entity>();
        
        entity.dealDamage(attacker, damage);
    }
}
