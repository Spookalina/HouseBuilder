using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] thingsToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        int temp = Random.Range(0, thingsToSpawn.Length);
        GameObject tempGO = Instantiate(thingsToSpawn[temp],transform.position, Quaternion.identity );
        tempGO.transform.parent = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
