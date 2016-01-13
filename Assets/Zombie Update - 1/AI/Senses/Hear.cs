using UnityEngine;
using System.Collections;

public class Hear :Sense
{
    public Hear(SenseHandler s) : base(s)
    {
        
    }
    public override void use()
    {
        if (SH.isDeaf)
        {
            return;
        }
        float xOffset = Random.Range(0, 5);
        float zOffset = Random.Range(0, 5);
        Vector3 offset = new Vector3(xOffset, 0, zOffset);
        targetPos += offset;
        float distance = Vector3.Distance(Entity.transform.position,targetPos);
        
        
        SH.newStimulus(calculatePriority(distance), SenseHandler.Detection.Sound, getBestAction());
       
    }
    public void search(Vector3 pos)
    {
        float xOffset = Random.Range(0, 5);
        float zOffset = Random.Range(0, 5);
        Vector3 offset = new Vector3(xOffset, 0, zOffset);
        targetPos += offset;
        float distance = Vector3.Distance(Entity.transform.position, targetPos);


        SH.newStimulus(calculatePriority(distance), SenseHandler.Detection.Sound, getBestAction());
    }
    public override int calculatePriority(float f)
    {
        return (Mathf.RoundToInt(100 / f));
        
    }
    public override Action getBestAction()
    {
        return new Search(SH.zom.AH,targetPos);
    }
}
