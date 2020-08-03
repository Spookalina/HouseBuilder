using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TileType{ Wood,Stone}
public class Tile : MonoBehaviour
{
    public float health = 1;
    public GameObject barPrefab;
    public GameObject barPrefabFilled;
    public Vector3 localScale;
    public TileType tileType = TileType.Wood;

    // Start is called before the first frame update
    void Start()
    { 
        localScale = barPrefabFilled.transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            health -= 0.1f;
        }
        localScale.x = health/2;
        barPrefabFilled.transform.localScale = localScale;
        if(health <= 0)
        {
            this.gameObject.SetActive(false);
        }
        if (health >= 1)
        {
            barPrefab.SetActive(false);
            barPrefabFilled.SetActive(false);
        }
        else
        {
            barPrefab.SetActive(true);
            barPrefabFilled.SetActive(true);
        }
    }
}
