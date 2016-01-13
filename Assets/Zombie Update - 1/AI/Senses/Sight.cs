using UnityEngine;
using System.Collections;

public class Sight :Sense
{
    public float motionDetectionSpeed = 1.8f;
    public LayerMask layer;
     public Sight(SenseHandler s) : base(s)
    {
        range = 15;
        layer = SH.zom.sightLayerMask;
    }
    
    float coneAngle = 150;
    float time = 0;
    public override void use()
    {
        if (SH.isBlind)
        {
            return;
        }
        look();
        //time += Time.deltaTime;
        //if (time >= 0)
        //{
        //    time = 0;
           
        //    Collider[] coll = Physics.OverlapSphere(Entity.transform.position, range);


        //    foreach (Collider col in coll)
        //    {
        //        RaycastHit hit;

        //        Vector3 direction = col.transform.position - Entity.transform.position;
        //        float angle = Vector3.Angle(direction, Entity.transform.forward);

        //        if (angle < coneAngle * 0.5f)
        //        {
        //            if (Physics.Linecast(Entity.transform.position, col.transform.position, out hit,layer))
        //            {
        //                if (hit.collider == col)
        //                {
        //                    if (hit.collider.tag == "Player" || (hit.collider.gameObject.GetComponent<Rigidbody>() != null && hit.collider.gameObject.GetComponent<Rigidbody>().velocity.magnitude>motionDetectionSpeed && hit.collider.tag != "Zombie"))
        //                    {
        //                        float distance = Vector3.Distance(Entity.transform.position, hit.collider.transform.position);
        //                        targetPos = hit.collider.transform.position;
        //                        target = hit.collider.gameObject;
        //                        if (target != null)
        //                        {
        //                            //Debug.Log("Target: "+target.transform.position);
        //                            SH.newStimulus(calculatePriority(distance), SenseHandler.Detection.Sight, getBestAction(), target);
        //                            return;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
      
    }
    public void look()
    {
        float stationaryRange = 15;
        float movingRange = 40;

        Collider[] cols = Physics.OverlapSphere(Entity.transform.position, movingRange);

        foreach (Collider col in cols)
        {
            RaycastHit hit;

                Vector3 direction = col.transform.position - Entity.transform.position;
                float angle = Vector3.Angle(direction, Entity.transform.forward);

                if (angle < coneAngle * 0.5f)
                {
                    if (Physics.Linecast(Entity.transform.position, col.transform.position, out hit,layer)) //clear sight of enemy
                    {
                        if (col.tag != "Zombie")
                        {
                            float distance = Vector3.Distance(Entity.transform.position, hit.collider.transform.position);
                            targetPos = hit.collider.transform.position;
                            target = hit.collider.gameObject;
                            if (col.tag == "Player")
                            {
                                if (distance <= stationaryRange)
                                {
                                    if (target != null)
                                    {
                                        //Debug.Log("Target: "+target.transform.position);
                                        SH.newStimulus(calculatePriority(distance), SenseHandler.Detection.Sight, getBestAction(), target);
                                        return;
                                    }
                                }
                            }
                            
                            if (hit.collider.gameObject.GetComponent<Rigidbody>() != null && hit.collider.gameObject.GetComponent<Rigidbody>().velocity.magnitude > motionDetectionSpeed)
                            {
                                if (target != null)
                                {
                                    //Debug.Log("Target: "+target.transform.position);
                                    SH.newStimulus(calculatePriority(distance), SenseHandler.Detection.Sight, getBestAction(), target);
                                    return;
                                }
                            }
                        }
                    }
                }
        }
    }
        
       
       
    
    public override int calculatePriority(float f)
    {
        return (Mathf.RoundToInt(1000/f)); ///1000/20 = 50, 1000/5 = 200  ,, 100/5 = 20
        
    }
    public override Action getBestAction()
    {
        return new Attack(SH.zom.AH,target);
    }
}
