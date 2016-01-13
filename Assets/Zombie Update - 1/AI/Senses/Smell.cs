using UnityEngine;
using System.Collections;

public class Smell : Sense
{
    public Smell(SenseHandler s) : base(s)
    {
        
    }

    float time = 5;
    public override void use()
    {
        base.use();
       
        time += Time.deltaTime;
        if (time >= 5)
        {
           
            time = 0;
            // have player emit invisible spheres with tag for smell

            // Debug.Log("Smelling");
            Collider[] cols = Physics.OverlapSphere(Entity.transform.position, range);

            GameObject currentTarget = null;
            float closestDist = range + 1;
            foreach (Collider col in cols)
            {

                if (col.tag == "Smell")
                {
                   
                    float dist = Vector3.Distance(Entity.transform.position, col.transform.position);
                    if (dist < closestDist && dist <= range)
                    {
                        closestDist = dist;
                        currentTarget = col.gameObject;
                    }

                }
            }
            if (currentTarget != null)
            {
                
                targetPos = currentTarget.transform.position;
                SH.newStimulus(calculatePriority(closestDist), SenseHandler.Detection.Smell, getBestAction(), currentTarget);

            }
        }
    }
    public override int calculatePriority(float distance)
    {
        return (Mathf.RoundToInt(10 / distance));
    }
    public override Action getBestAction()
    {
        return new Search(SH.zom.AH, targetPos);
    }
    
}
