using UnityEngine;
using System.Collections;

[System.Serializable]
public class Entity : MonoBehaviour {

    public string name = "Bob";
    public int health = 100;
    public int maxHealth = 100;
    public int baseDamageDealt = 10;

    public virtual void Start () 
    {
        
	}
	
	
	public virtual void Update () {
	
	}
    
    public virtual void dealDamage(Entity attacker, int damage =-1)
    {
       
        int damageDealt = damage == -1 ? attacker.baseDamageDealt : damage;
        health -= damageDealt;

        if (health <= 0)
        {
            Animator anim = transform.GetComponentInChildren<Animator>();
            anim.SetTrigger("die");
        }
    }
}
