using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum Weather { Tsunami, Earthquake, Tornado, HighTemp, Rainy, Thunder }
public class WeatherState : MonoBehaviour
{
    public Weather[] weathers;
    public Save save;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    public GameObject cameraGO;
    private float shakeTimer;
    private float startingIntensity;
    private float shakeTimerTotal;
    public List<GameObject> allActiveObjects = new List<GameObject>();
    public GameObject tileContainer;
    public GameObject backgroundTiles;
    // Start is called before the first frame update
    public void Start()
    {
        save = this.gameObject.GetComponent<Save>();
        if (save.playerValues.currentLevel == 1)
        {
            weathers = new Weather[1];
            weathers[0] = Weather.Rainy;
        }
        else if (save.playerValues.currentLevel == 2)
        {
            weathers = new Weather[1];
            weathers[0] = Weather.Earthquake;
        }
        else if (save.playerValues.currentLevel == 3)
        {
            weathers = new Weather[1];
            weathers[0] = Weather.Thunder;
        }
        else if (save.playerValues.currentLevel == 4)
        {
            weathers = new Weather[1];
            weathers[0] = Weather.Tornado;
        }
        else if (save.playerValues.currentLevel == 5)
        {
            weathers = new Weather[1];
            weathers[0] = Weather.HighTemp;
        }
        else if (save.playerValues.currentLevel == 6)
        {
            weathers = new Weather[1];
            weathers[0] = Weather.Tsunami;
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

    public void WeatherWhen()
    {
        save = this.gameObject.GetComponent<Save>();
        save.playerValues.currentLevel += 1;
        if (save.playerValues.currentLevel == 1)
        {
            weathers = new Weather[1];
            weathers[0] = Weather.Rainy;
            StartGame();
        }
        else if (save.playerValues.currentLevel == 2)
        {
            weathers = new Weather[1];
            weathers[0] = Weather.Earthquake;
            StartGame();
        }
        else if (save.playerValues.currentLevel == 3)
        {
            weathers = new Weather[1];
            weathers[0] = Weather.Thunder;
        }
        else if (save.playerValues.currentLevel == 4)
        {
            weathers = new Weather[1];
            weathers[0] = Weather.Tornado;
        }
        else if (save.playerValues.currentLevel == 5)
        {
            weathers = new Weather[1];
            weathers[0] = Weather.HighTemp;
        }
        else if (save.playerValues.currentLevel == 6)
        {
            weathers = new Weather[1];
            weathers[0] = Weather.Tsunami;
        }
        else if (save.playerValues.currentLevel > 6 && save.playerValues.currentLevel < 15)
        {
            weathers = new Weather[2];
            for (int i = 0; i < weathers.Length; i++)
            {
                weathers[i] = (Weather)Random.Range(0, System.Enum.GetValues(typeof(Weather)).Length);
                StartGame();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(cameraGO != null)
        { 
            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
            
            }
        }
        
        
    }

    public void StartGame()
    {
        for (int i = 0; i < weathers.Length; i++)
        {
            if (weathers[i] == Weather.Rainy)
            {
                StartCoroutine(RainyDay());
            }

            if(weathers[i] == Weather.Earthquake)
            {
                StartCoroutine(EarthquakeDay());
            }
        }
    }

    private void Awake()
    {
        
    }

    public void CameraShaker(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimer = time;
        shakeTimerTotal = time;
    }
    IEnumerator RainyDay()
    {
        GameObject[] gOTop;
        int tempTime = 0;
        gOTop = GameObject.FindGameObjectsWithTag("Roof");
        while(tempTime <= 15)
        {
            gOTop = GameObject.FindGameObjectsWithTag("Roof");
            yield return new WaitForSeconds(1f);
            tempTime += 1;
            gOTop[Random.Range(0,gOTop.Length)].GetComponent<Tile>().health -= Random.Range(0.2f,0.3f);
        }
    }

    IEnumerator EarthquakeDay()
    {
        tileContainer = GameObject.Find("HouseTiles");
        cameraGO = GameObject.Find("Camera");
        cinemachineVirtualCamera = cameraGO.GetComponent<CinemachineVirtualCamera>();
        GameObject[] gOTop;
        int tempTime = 0;
        gOTop = GameObject.FindGameObjectsWithTag("Bottom");
        backgroundTiles = GameObject.Find("FondoCasa");
        CameraShaker(5,15f);
        int tempLength;
        tempLength = gOTop.Length;
        while (tempTime <= 15)
        {
            gOTop = GameObject.FindGameObjectsWithTag("Bottom");
            
            yield return new WaitForSeconds(1f);
            tempTime += 1;
            gOTop[Random.Range(0, gOTop.Length)].GetComponent<Tile>().health -= Random.Range(0.2f, 0.3f);
            if(tempLength > gOTop.Length)
            {
                for (int i = 0; i < tileContainer.transform.childCount ; i++)
                {
                    if (tileContainer.transform.GetChild(i).gameObject.activeSelf == true && tileContainer.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>() != null)
                    {
                        GameObject tempGO = tileContainer.transform.GetChild(i).gameObject;
                        allActiveObjects.Add(tempGO);
                    }
                }

                for (int i = 0; i < allActiveObjects.Count; i++)
                {
                    allActiveObjects[i].GetComponent<Rigidbody2D>().gravityScale = 1;
                    allActiveObjects[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    

                }
                backgroundTiles.SetActive(false);
                yield break;
            }

        }
    }
}
