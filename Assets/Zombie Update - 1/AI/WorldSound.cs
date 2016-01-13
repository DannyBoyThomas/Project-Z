using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldSound 
{
    public static void Play(Vector3 position, float range = 20)
    {
        float offset = Random.Range(0, range * 0.2f);
        range += offset;

        Collider[] cols = Physics.OverlapSphere(position, range);
        if (cols != null)
        {
            foreach (Collider col in cols)
            {
                if (col.tag == "Zombie")
                {
                    List<Sense> senses = col.GetComponent<ZombieAI>().SH.senses;

                    foreach (Sense s in senses)
                    {
                        if (s.GetType() == typeof(Hear))
                        {
                            s.targetPos = position;
                            //s.use();
                            ((Hear)s).search(position);
                            break;
                        }
                    }
                }
            }
        }
    }
    //public WorldSound(Vector3 position, float range = 20)
    //{
    //    float offset = Random.Range(0, range * 0.2f);
    //    range += offset;

    //    Collider[] cols = Physics.OverlapSphere(position, range);
    //    if (cols != null)
    //    {
    //        foreach (Collider col in cols)
    //        {
    //            if(col.tag == "Zombie")
    //            {
    //                List<Sense> senses = col.GetComponent<ZombieAI>().SH.senses;
                  
    //                foreach (Sense s in senses)
    //                {
    //                    if (s.GetType() == typeof(Hear))
    //                    {
    //                        s.targetPos = position;
    //                        s.use();
    //                        break;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
}
