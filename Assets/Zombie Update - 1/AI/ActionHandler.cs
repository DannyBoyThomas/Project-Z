using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ActionHandler {

    public List<Action> actions = new List<Action>();
    public Entity Entity;
    public int priority = 0;
    public enum type { Active, Inactive, Disabled };
    public ZombieAI zom;
    private Action currentAction ;
    public bool canAttack = true;
    public ActionHandler(Entity owner)
    {
        Entity = owner;
        zom = Entity.gameObject.GetComponent<ZombieAI>();
        addAction(new Walk(this));
       
    }
    public void sortActions()
    {
       actions.RemoveAll((x) => x.actionType == ActionHandler.type.Disabled);
       actions = actions.OrderByDescending(x => x.getPriority()).ToList();
       
        if (actions.Count >3)
        {
            for (int i = 3; i < actions.Count; i++)
            {
                actions.RemoveAt(i);
            }
        }
    }
   
    public void addAction(Action a)
    {
       
        if (actions.Count > 0)
        {
          
            Action current = actions[0];
            int pr = current.getPriority();
            
            if (a.GetType() == typeof(Attack)) //remove all attack actions ( new is better)
            {
               
                    actions.RemoveAll((x) => x.getPriority() == pr); // has same priority as currently playing
                

            }
           
        }
        actions.Add(a);
        sortActions();

       
       
    }
    float time = 0;
    public void perform()
    {
        if (!canAttack)
        {
            time += Time.deltaTime;
        }
        if (time > 1)
        {
            time = 0;
            canAttack = true;
        }
        if (actions != null && actions.Count > 0)
        {
            
            sortActions();

            for (int i = 0; i < actions.Count; i++)
            {
               // Debug.Log(i + ", " + actions[i] + ", "+ actions[i].targetPos  );
                
                    
            }
            Action a = actions[0];
            if (a != null) //has ana ction to perform
            {
                if  (currentAction != a) //set the new currentAction
                {
                    currentAction = a;
                     a.initialise();
                }

                if (a.actionType == ActionHandler.type.Active)
                {
                    a.perform();
                    
                }
            }
        }
        if (actions != null && actions.Count < 3) //make zombie do stuff if it has nothing to do
        {
           // Debug.Log("adding");
            for (int i = actions.Count; i <= 3; i++)
            {
                addAction(randomAction());
            }
        }

    }
    Action randomAction()
    {
        int i =Random.Range(0, 2);
        if (i == 0)
        {
            return new Walk(this);
        }
        return new Stand(this);
    }
    public  void getAnimationValues(Animator anim)
    {
        if (currentAction != null)
        {
            currentAction.getAnimationValues(anim);

        }
        else
        {
            anim.SetBool("run", false);
            anim.SetFloat("speed", 0);
        }
    }
}
