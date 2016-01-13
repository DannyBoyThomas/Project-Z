using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SenseHandler 
{

    public class Stimulus
    {
        public int priority =0;
        public  Detection type = Detection.None;
        public GameObject target = null;
        public Action action;
        public Stimulus(int p, Detection d, GameObject g, Action a)
        {
            priority = p;
            type =d;
            target = g;
            action =a;
        }
    }
    public enum Detection { None =0,Smell =1, Sound = 2, Sight =3};
    public Detection detectionType = Detection.None;

    public ZombieAI zom;
    public List<Sense> senses = new List<Sense>();
    public List<Stimulus> stims = new List<Stimulus>();
    public Entity Entity;
    public bool isDeaf, isBlind;

    public SenseHandler(Entity entity, bool deaf = false, bool blind = false)
    {
        Entity = entity;
        isDeaf = deaf;
        isBlind = blind;
        zom = Entity.GetComponent<ZombieAI>();
        initialise();
    }
  
    void initialise()
    {
        senses.Add(new Smell(this));
        senses.Add(new Hear(this));
        senses.Add(new Sight(this));
    }
    public void perform()
    {
        useSenses();
        orderStims();
        //for (int i = 0; i < stims.Count; i++)
        //{
        //    Debug.Log(i + ", " + stims[i].action + ", Pri: " + stims[i].priority);
        //}

        if (stims.Count > 3)
        {
            for (int i = 3; i < stims.Count; i++)
            {
                stims.RemoveAt(i);
            }
        }
        if (stims.Count > 0)
        {

            Action bestAction = stims[0].action;

            zom.AH.addAction(bestAction);
            stims.RemoveAt(0);

        }
        


    }
   
    void orderStims()
    {
      stims = stims.OrderByDescending(x => x.priority).ToList();
    }

    public void newStimulus(int priority, Detection detect,  Action act, GameObject target = null)
    {
        //if (target != null)
        //{
        //    Debug.Log(target.transform.position);
        //}
        stims.Add(new Stimulus(priority,detect,target,  act));
        
        orderStims();
    }
    
    
    void useSenses()
    {
        foreach (Sense s in senses)
        {
            if (s.GetType() != typeof(Hear))
            {
                s.use();
            }
        }
    }
   
   
}
