using UnityEngine;
using System.Collections;

public class WorldZombie : MonoBehaviour {

	// Use this for initialization
    public GameObject[] prefabs;
    int zombieCount = 0;
	void Start () 
    {
        zombieCount = GameObject.FindGameObjectsWithTag("Zombie").Length;
	}
    float time = 0;
	// Update is called once per frame
	void Update () 
    {

        time += Time.deltaTime;
        if (time > 2)
        {
            time = 0;
            randomSpawn();
        }
	}

    GameObject getRandomZombie()
    {
        int i = Random.Range(0, prefabs.Length);
        return prefabs[i];
    }
    public int range = 20;
    void randomSpawn()
    {
       

        while (true)
        {
            float x = Random.Range(0, range * 2) - range;
            float z= Random.Range(0, range * 2) - range;
            Vector3 spawnPos = new Vector3(x,0,z);
            GameObject[] obs = GameObject.FindGameObjectsWithTag("Player");
            bool seen = false;
            foreach (GameObject g in obs)
            {
                if (seen)
                {
                    break;
                }
                RaycastHit hit;
                if (Physics.Linecast(g.transform.position, spawnPos, out hit))
                {
                    if(Vector3.Distance(spawnPos,hit.point)<0.3f)
                    {
                        seen = true;
                        break;
                    }
                }
            }
            if (!seen)
            {
                Instantiate(getRandomZombie(),spawnPos, Quaternion.identity);
                return;
            }
        }
    }
}
