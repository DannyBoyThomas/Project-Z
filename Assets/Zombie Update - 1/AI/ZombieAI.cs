using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour {

   public  ActionHandler AH;
   public SenseHandler SH;
   public bool isDeaf = false;
   public bool isBlind = false;
   public float walkSpeed = 1f;
   public float searchSpeed = 2f;
   public float attackSpeed = 5f;
   public float damageDealt = 10;
   public bool pathFind = true;
   public bool makeNoiseOnAttack = true;
    public Animator animator;
    public NavMeshAgent agent;
    public LayerMask sightLayerMask;
	void Start () 
    {
        Entity entity = gameObject.GetComponent<Entity>();
	    AH = new ActionHandler(entity);
        SH = new SenseHandler(entity,isDeaf,isBlind);
	}
	
	
	void Update () 
    {
        AH.perform();
        SH.perform();

        AH.getAnimationValues(animator);
        //animator.SetFloat("speed", agent.desiredVelocity.magnitude);
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * 3));
        Gizmos.DrawWireSphere(transform.position, 10);
    }
}
