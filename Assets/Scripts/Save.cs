using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.UI;
using System.Collections.Generic;


[System.Serializable]
public class PlayerValues
{
    public int wood = 0;
    public int metal = 0;
    public int rock = 0;
    public int xP = 0;
    public int currentLevel = 0;
    public float secondsPlayed = 0;
    public int minLuck = 1;
    public float luck = 0;
    public bool tutorialDone = false;
        
}

public class Save : MonoBehaviour
{
    
    public PlayerValues playerValues;
  

    public float timeOfLastLevelLoad;

    

    void Awake()
    {
        timeOfLastLevelLoad = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        SavePlayerValues();
        playerValues.secondsPlayed += Time.deltaTime;
        if (Time.timeSinceLevelLoad - timeOfLastLevelLoad > 2)
        {
            
            DoSomethingUwU();
            timeOfLastLevelLoad = Time.timeSinceLevelLoad;
            ShowValues();
        }
         
    }
    public void LoadPlayerValues()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerValues));
        string text = PlayerPrefs.GetString("player values");
        if (text.Length == 0)
        {
            playerValues = new PlayerValues();
        }
        else
        {
            using (var reader = new StringReader(text))
            {
                playerValues = serializer.Deserialize(reader) as PlayerValues;
            }
        }
    }
    public void SavePlayerValues()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerValues));
        using (StringWriter sw = new StringWriter())
        {
            serializer.Serialize(sw, playerValues);
            PlayerPrefs.SetString("player values", sw.ToString());
        }
    }

    public void ResetPlayerValues()
    {
        PlayerPrefs.SetString("player values", "");
    }

    public void DoSomethingUwU()
    {
        // saveValues.playerValues.money++;
    }
    public void ShowValues()
    {
        Debug.Log("Wood = " + playerValues.wood + " metal = " + " " + playerValues.metal + " xp = " + playerValues.xP + " ");
    }
}