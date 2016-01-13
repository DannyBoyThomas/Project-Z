using UnityEngine;
using System.Collections;

public class Action {


    public ActionHandler.type actionType = ActionHandler.type.Inactive;
    public GameObject target = null;
    public Entity Entity;
    public Vector3 targetPos;
    public ActionHandler AH;
    int priority=0;

    public Action() { }
    public Action(ActionHandler aH, GameObject target = null)
    {
        
        this.target = target;
        AH = aH;
        Entity = aH.Entity;

    }
    public void setTarget(GameObject g)
    {
        if (g != null)
        {
            target = g;
        }
    }
    
    public virtual void initialise()
    {
        actionType = ActionHandler.type.Active;
    }

    public virtual void perform()
    {

    }
    public int getPriority()
    {
        return priority;
    }
    public void setPriority(int i)
    {
        priority = i;
    }
    public void end()
    {
        actionType = ActionHandler.type.Disabled;
        AH.sortActions();
    }
   public void moveTo(float speed = 1f)
    {
        
        
       

       
        if (targetPos != null)
        {
            if (AH.zom.pathFind)
            {
                
                NavMeshAgent agent = Entity.transform.GetComponent<NavMeshAgent>();
                if (Vector3.Distance(agent.destination, targetPos) > 2)
                {
                    agent.SetDestination(targetPos);
                }
                agent.speed = speed;
                //Entity.gameObject.GetComponent<Rigidbody>().velocity = agent.desiredVelocity;
                agent.velocity = agent.desiredVelocity;
            }
            else
            {
               
                Vector3 loc = new Vector3(targetPos.x, Entity.transform.position.y, targetPos.z);
                Vector3 dif = (loc - Entity.transform.position).normalized;
                Entity.transform.position += dif * Time.deltaTime * speed;

                float distance = Vector3.Distance(Entity.transform.position, loc);
                Vector3 currentLookPos = Entity.transform.position + (distance * Entity.transform.forward);
                var q = Quaternion.LookRotation(loc - Entity.transform.position);
                Entity.transform.rotation = Quaternion.RotateTowards(Entity.transform.rotation, q, speed * Time.deltaTime * 100);

                Vector3 look = new Vector3(targetPos.x, Entity.transform.position.y,targetPos.z);
                Entity.transform.LookAt(look);
            }
        }
    }
    public virtual void getAnimationValues(Animator anim)
    {
        
    }
}
