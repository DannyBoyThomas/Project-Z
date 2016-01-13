using UnityEngine;
using System.Collections;

public class ImpactSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision collision)
    {
        Collider col = collision.collider;
        if(col != null)
        {
            WorldSound.Play(collision.contacts[0].point);
           
        }
    }
}
