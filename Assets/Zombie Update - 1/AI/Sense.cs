using UnityEngine;
using System.Collections;

public class Sense 
{
    public int priority = 0;
    public int range = 40;
    public Entity Entity;
    public SenseHandler SH;
   public  GameObject target;
    public Vector3 targetPos;
   
    public Sense(SenseHandler s)
    {
        SH =s;
        Entity = s.Entity;
        
    }
    public int getPriority()
    {
        return priority;
    }
    public virtual void use()
    {
        
    }
    public virtual int calculatePriority(float f)
    {

        return 0;
    }
    public virtual Action getBestAction()
    {
        return new Walk(SH.zom.AH);
    }

	
}
