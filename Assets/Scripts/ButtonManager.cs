using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    public void WeatherStart()
    {
        gameManager.GetComponent<WeatherState>().WeatherWhen();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
