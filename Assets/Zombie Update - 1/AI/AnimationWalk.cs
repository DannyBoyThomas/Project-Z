using UnityEngine;
using System.Collections;

public class AnimationWalk : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnAnimatorMove()
    {
        NavMeshAgent agent = transform.parent.GetComponent<NavMeshAgent>();
        
        agent.updateRotation = true;
        Animator anim = GetComponent<Animator>();
        agent.velocity = anim.deltaPosition / Time.deltaTime;
        if (agent.desiredVelocity != Vector3.zero)
        {
            agent.transform.rotation = Quaternion.LookRotation(agent.desiredVelocity);
        }
    }
}
