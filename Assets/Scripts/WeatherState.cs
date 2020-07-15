using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weather { Tsunami, Earthquake, Tornado, HighTemp, Rainy, Thunder }
public class WeatherState : MonoBehaviour
{
    public Weather[] weathers;
    public Save save;
    // Start is called before the first frame update
    public void Start()
    {
        save = this.gameObject.GetComponent<Save>();
        if(save.playerValues.currentLevel == 1)
        {
            weathers = new Weather[1];
            weathers[1] = Weather.Rainy;
        }
        else if (save.playerValues.currentLevel == 2)
        {
            weathers = new Weather[1];
            weathers[1] = Weather.Earthquake;
        }
        else if (save.playerValues.currentLevel == 3)
        {
            weathers = new Weather[1];
            weathers[1] = Weather.Thunder;
        }
        else if (save.playerValues.currentLevel == 4)
        {
            weathers = new Weather[1];
            weathers[1] = Weather.Tornado;
        }
        else if (save.playerValues.currentLevel == 5)
        {
            weathers = new Weather[1];
            weathers[1] = Weather.HighTemp;
        }
        else if (save.playerValues.currentLevel == 6)
        {
            weathers = new Weather[1];
            weathers[1] = Weather.Tsunami;
        }
        else if (save.playerValues.currentLevel > 6 && save.playerValues.currentLevel < 15)
        {
            weathers = new Weather[2];
            for (int i = 0; i < weathers.Length; i++)
            {
                weathers[i] = (Weather)Random.Range(0, System.Enum.GetValues(typeof(Weather)).Length);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void StartGame()
    {
        for (int i = 0; i < weathers.Length; i++)
        {
            if (weathers[i] == Weather.Rainy)
            {
                StartCoroutine(RainyDay());
            }
        }
    }
    IEnumerator RainyDay()
    {
        GameObject[] gOTop;
        int tempTime = 0;
        gOTop = GameObject.FindGameObjectsWithTag("Roof");
        while(tempTime <= 15)
        {
            yield return new WaitForSeconds(1f);
            tempTime += 1;
            gOTop[Random.Range(0,gOTop.Length)].GetComponent<Tile>().health -= 10;
        }
    }
}
