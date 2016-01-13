using UnityEngine;
using System.Collections;

public class Walk : Action {

    
    float range = 30;
    float speed = 2f;
    float hangTime;
    bool walking;
    

    public Walk(ActionHandler aH, GameObject target = null)
        : base(aH, target)
    {
        setPriority(1);
        hangTime = Random.Range(3, 30);
    }
    public override void initialise()
    {
        base.initialise();
        
        float x = Random.Range(0, range) - (range / 2f);
        float z = Random.Range(0, range) - (range / 2f);
        Vector3 offset = new Vector3(x,0,z);
        targetPos = Entity.transform.position + offset;
    }
    float time = 0;
    public override void perform()
    {
        base.perform();

        
            
           
           // Debug.Log("walking");
            if (Vector3.Distance(targetPos, Entity.transform.position) < 1)
            {
                time += Time.deltaTime;
                walking = false;
                if (time > hangTime)
                {
                    end();
                }
            }
            else
            {
               // Debug.Log("moving");
                walking = true;
                moveTo(AH.zom.walkSpeed);

            }
        

    }
    public override void getAnimationValues(Animator anim)
    {
        anim.SetFloat("speed", walking?AH.zom.walkSpeed:0);
        anim.SetBool("run",false);
    }
   
}
