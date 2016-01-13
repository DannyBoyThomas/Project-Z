using UnityEngine;
using System.Collections;

public class Stand : Action
{
    public Stand(ActionHandler aH, GameObject target = null)
        : base(aH, target)
    {
        setPriority(1);
        this.target = target;
    }
    public override void initialise()
    {
        base.initialise();

        if (target != null)
        {
            targetPos = target.transform.position;
        }
        hangTime = Random.Range(3, 25);
    }
    float time = 0;
    float hangTime;
    public override void perform()
    {
        base.perform();
      //  Debug.Log("Attack: " + targetPos);


        time += Time.deltaTime;
        //printTime();
        if (time > hangTime)
        {
            end();
        }
        
        

    }
    void printTime()
    {
        Debug.Log(time + " / " + hangTime);
    }
    public override void getAnimationValues(Animator anim)
    {
        anim.SetFloat("speed", 0f);
        anim.SetBool("run", false);
    }
}
