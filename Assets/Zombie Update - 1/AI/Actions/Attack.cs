using UnityEngine;
using System.Collections;

public class Attack : Action
{

    bool inAttackRange = false;
    public Attack(ActionHandler aH, GameObject target = null)
        : base(aH, target)
    {
        setPriority(4);
        this.target = target;
    }
    public override void initialise()
    {
        base.initialise();

        if (target != null)
        {
            targetPos = target.transform.position;
        }
    }
    public override void perform()
    {
        base.perform();
      //  Debug.Log("Attack: " + targetPos);

        if (AH.zom.makeNoiseOnAttack)
        {
            makeNoise();
        }
            moveTo(AH.zom.attackSpeed);

            if (Vector3.Distance(targetPos, Entity.transform.position) < 1.2f)
            {
                //damage;
               
                if (AH.canAttack)
                {
                    if (target.GetComponent<Entity>() != null)
                    {
                        target.GetComponent<Entity>().dealDamage(Entity);
                    }
                    Animator anim = AH.zom.animator ;
                    anim.SetTrigger("attack");
                    AH.canAttack = false;
                }
                end();
            }
            
        if(targetPos != null)
        {
            RaycastHit hit;
            if(Physics.Linecast(Entity.transform.position,targetPos,out hit))
            {
                if (hit.collider.tag != "Player")
                {
                    AH.addAction(new Search(AH, targetPos));
                    end();
                }
            }
        }
            
        
        

    }
    void makeNoise()
    {
         float i = Random.Range(0, 1);
        if (i < 0.3f)
        {
            float distance = Vector3.Distance(Entity.transform.position,targetPos);
            float range = 10;// Mathf.Clamp(8 - distance, 1, 8);
            WorldSound.Play(Entity.transform.position, range);
        }

    }
    public override void getAnimationValues(Animator anim)
    {
       
            NavMeshAgent agent = AH.zom.GetComponent<NavMeshAgent>();
        bool near = agent.remainingDistance <0.1f;
        float returnSpeed = near ?0: AH.zom.attackSpeed;
        anim.SetFloat("speed", returnSpeed);
        anim.SetBool("run", true);

        

        
    }
   
}
