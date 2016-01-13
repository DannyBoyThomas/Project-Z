using UnityEngine;
using System.Collections;

public class Search : Action
{
   
    public Search(ActionHandler aH, Vector3 targ)
        : base(aH, null)
    {
        targetPos = targ;
        setPriority(2);
        hangTime = Random.Range(3, 10);
        hangDistance = Random.Range(0.6f, 2);
    }
    float time = 0;
    float hangTime;
    float hangDistance;
    public override void perform()
    {
        base.perform();
       
       // Debug.Log("Searching");
        if (Vector3.Distance(targetPos, Entity.transform.position) < hangDistance)
        {
            time += Time.deltaTime;
           
            if (time > hangTime)
            {
                end();
            }
        }
        else
        {
            moveTo(AH.zom.searchSpeed);
        }

    }
    public override void getAnimationValues(Animator anim)
    {
        anim.SetFloat("speed", AH.zom.searchSpeed);
        anim.SetBool("run", false);
    }
}
