using UnityEngine;
using System.Collections;

public class LootGenerator : MonoBehaviour 
{

    public static void GenerateWorldLoot()
    {
        GameObject[] SpawnPoints = GameObject.FindGameObjectsWithTag("Loot Point");

        object[] spawnableObjects = Resources.LoadAll("Loot", typeof(GameObject));

        if (spawnableObjects.Length == 0)
        {
            Debug.LogWarning("No Loot exists");
            return;
        }

        foreach (GameObject trans in SpawnPoints)
        {
            int loot = Random.Range(0, spawnableObjects.Length);

            GameObject.Instantiate((GameObject)spawnableObjects[loot], trans.transform.position, Quaternion.identity);
        }
    }

    public void Start()
    {
        GenerateWorldLoot();
    }
	
}
