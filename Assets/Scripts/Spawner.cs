using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> spawners = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            spawners.Add(child.gameObject);
        }
        
        while(spawners.Count >= Random.Range(3,6))
        {
            DeleteSpawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeleteSpawn()
    {
        int tempInt = Random.Range(0,spawners.Count);
        Destroy(spawners[tempInt]);
        spawners.RemoveAt(tempInt);
    }
}
