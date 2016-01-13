using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed = 1.8f;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        //transform.position += transform.forward * speed;
        Rigidbody rig = GetComponent<Rigidbody>();
        rig.velocity = transform.forward*speed;
        
	}
}
