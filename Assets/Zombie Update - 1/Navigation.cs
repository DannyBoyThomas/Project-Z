using UnityEngine;
using System.Collections;

public class  Navigation :MonoBehaviour {

    
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    public static bool Move(GameObject ob, Vector3 end, float speed = 3.5f)
    {
        return Move(ob.transform.position, end,ob, speed);
    }
    public static bool Move(Vector3 start, Vector3 end, GameObject ob, float speed =3.5f)
    {
      
        if (ob != null)
        {
            if (ob.GetComponent<NavMeshAgent>() != null)
            {
                Debug.Log("pathFinding");
                NavMeshAgent nav = ob.GetComponent<NavMeshAgent>();
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(start, end, 0, path))
                {

                    nav.speed = speed;
                    nav.Move(Vector3.zero);
                   
                    return true;
                }
                
            }
        }
        return false;
    }
}
