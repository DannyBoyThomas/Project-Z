using UnityEngine;
using System.Collections;

public class ZombieAnimator : MonoBehaviour {

    Action action;
    Animator anim;
    ZombieAI zom;
    Vector3 vel;
    Entity entity;
    
    float speed;
	void Start () {
        zom = GetComponent<ZombieAI>();
        anim = GetComponentInChildren<Animator>();
        entity = GetComponent<Entity>();
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        vel = entity.GetComponent<Rigidbody>().velocity;
        speed = vel.magnitude;
        action = zom.AH.actions[0];

        
	}
}
