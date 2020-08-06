using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

public enum Weather { Earthquake, Rainy}
public enum Weather2 { Tsunami, Earthquake, Tornado, HighTemp, Rainy, Thunder }
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
    public GameObject rainGO;
    public GameObject winGO;
    public GameObject looseGO;
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
            for (int i = 0; i < weathers.Length; i++)
            {
                weathers[i] = (Weather)Random.Range(0, System.Enum.GetValues(typeof(Weather)).Length);
            }
        }
       /* else if (save.playerValues.currentLevel == 3)
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
        }*/

    }

    public void WeatherWhen()
    {
        save = this.gameObject.GetComponent<Save>();
        // save.playerValues.currentLevel += 1;
        for (int i = 0; i < 14; i++)
        {
            GameObject temp = GameObject.Find("WoodPreview2 (" + i + ")");
            save.playerValues.saveTile[i] = temp.GetComponent<Tile>().tileType;
        }
        if (save.playerValues.currentLevel == 1)
        {
            weathers = new Weather[1];
            weathers[0] = Weather.Rainy;
            StartGame();
        }
        else if (save.playerValues.currentLevel >= 2)
        {
            weathers = new Weather[1];
            for (int i = 0; i < weathers.Length; i++)
            {
                weathers[i] = (Weather)Random.Range(0, System.Enum.GetValues(typeof(Weather)).Length);
                StartGame();
            }
        }
        /*else if (save.playerValues.currentLevel == 3)
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
        }*/
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
        GameManager.isUpgradeTime = false;
        this.gameObject.GetComponent<GameManager>().canTap = true;
        for (int i = 0; i < weathers.Length; i++)
        {
            if(save.playerValues.tuorialDone2 == false)
            {
                if (weathers[i] == Weather.Rainy)
                {
                    StartCoroutine(RainyDayTutorial());
                }
            }

            else
            {
                if (weathers[i] == Weather.Rainy)
                {
                    StartCoroutine(RainyDay());
                }

                if (weathers[i] == Weather.Earthquake)
                {
                    StartCoroutine(EarthquakeDay());
                }
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
        GameObject tempboton = GameObject.Find("CanvasGeneral").transform.GetChild(2).gameObject;
        GameObject tempday = GameObject.Find("CanvasGeneral").transform.GetChild(5).gameObject;
        GameObject tempMaskUI = GameObject.Find("TimeBarCanvas").transform.GetChild(0).gameObject;
        GameObject tempUIClock = GameObject.Find("TimeBarCanvas").transform.GetChild(1).gameObject;
        tempboton.SetActive(false);
        tempday.SetActive(false);
        rainGO = GameObject.Find("Rain").transform.GetChild(0).gameObject;
        rainGO.SetActive(true);
        GameObject[] gOTop;
        int tempTime = 30;
        gOTop = GameObject.FindGameObjectsWithTag("Roof");
        int tempLength;
        tempLength = gOTop.Length;
        tempMaskUI.SetActive(true);
        tempUIClock.SetActive(true);
        Image tempImage = tempMaskUI.GetComponent<Image>();
        while (tempTime > 0)
        {
            tempImage.fillAmount = (float)tempTime/30;
            gOTop = GameObject.FindGameObjectsWithTag("Roof");
            yield return new WaitForSeconds(1f);
            tempTime -= 1;
            gOTop[Random.Range(0,gOTop.Length)].GetComponent<Tile>().health -= Random.Range(0.2f,0.3f);
            if (tempLength > gOTop.Length)
            {
                GameObject lose = Instantiate(looseGO) as GameObject;

                lose.transform.SetParent(GameObject.Find("CanvasGeneral").transform, false);
                lose.transform.GetChild(0).GetComponent<TMP_Text>().text = "Dia " + save.playerValues.currentLevel;
                yield break;
            }
        }

        rainGO.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        GameObject win = Instantiate(winGO) as GameObject;
        win.transform.SetParent(GameObject.Find("CanvasGeneral").transform, false);
        win.transform.GetChild(0).GetComponent<TMP_Text>().text = "Dia " + save.playerValues.currentLevel;
        save.playerValues.currentLevel++;
    }

    IEnumerator RainyDayTutorial()
    {
        GameObject tempboton = GameObject.Find("CanvasGeneral").transform.GetChild(2).gameObject;
        GameObject tempday = GameObject.Find("CanvasGeneral").transform.GetChild(5).gameObject;
        GameObject tempMaskUI = GameObject.Find("TimeBarCanvas").transform.GetChild(0).gameObject;
        GameObject tempUIClock = GameObject.Find("TimeBarCanvas").transform.GetChild(1).gameObject;
        tempboton.SetActive(false);
        tempday.SetActive(false);
        rainGO = GameObject.Find("Rain").transform.GetChild(0).gameObject;
        rainGO.SetActive(true);
        GameObject[] gOTop;
        int tempTime = 0;
        gOTop = GameObject.FindGameObjectsWithTag("Roof");
        GameObject tempGOBlack = GameObject.Find("Black").transform.GetChild(0).gameObject;
        GameObject tempGOText = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        
        while (tempTime <= 1)
        {
            
            gOTop = GameObject.FindGameObjectsWithTag("Roof");
            yield return new WaitForSeconds(1f);
            tempTime += 1;
            gOTop[1].GetComponent<Tile>().health -= Random.Range(0.2f, 0.3f);
            tempGOBlack.SetActive(true);
            tempGOText.SetActive(true);

        }
        while(gOTop[1].GetComponent<Tile>().health <= 1)
        {
            yield return new WaitForSeconds(0.1f);
        }

        tempTime = 30;
        tempGOText.GetComponent<TMP_Text>().text = "Muy Bien! \nsigue protegiendo bien tu casa";
        yield return new WaitForSeconds(3f);
        tempGOBlack.SetActive(false);
        tempGOText.SetActive(false);
        int tempLength;
        tempLength = gOTop.Length;
        tempMaskUI.SetActive(true);
        tempUIClock.SetActive(true);
        Image tempImage = tempMaskUI.GetComponent<Image>();
        while (tempTime > 0)
        {
            tempImage.fillAmount = (float)tempTime / 30;
            gOTop = GameObject.FindGameObjectsWithTag("Roof");
            yield return new WaitForSeconds(1f);
            tempTime -= 1;
            gOTop[Random.Range(0, gOTop.Length)].GetComponent<Tile>().health -= Random.Range(0.2f, 0.3f);
            if (tempLength > gOTop.Length)
            {
                GameObject lose = Instantiate(looseGO) as GameObject;
                
                lose.transform.SetParent(GameObject.Find("CanvasGeneral").transform, false);
                lose.transform.GetChild(0).GetComponent<TMP_Text>().text = "Dia " + save.playerValues.currentLevel;
                yield break;
            }
        }
        
        rainGO.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        GameObject win = Instantiate(winGO) as GameObject;
        win.transform.SetParent(GameObject.Find("CanvasGeneral").transform, false);
        win.transform.GetChild(0).GetComponent<TMP_Text>().text = "Dia " + save.playerValues.currentLevel; 
        save.playerValues.currentLevel++;
        save.playerValues.tuorialDone2 = true;
    }

    IEnumerator EarthquakeDay()
    {
        GameObject tempboton = GameObject.Find("CanvasGeneral").transform.GetChild(2).gameObject;
        GameObject tempday = GameObject.Find("CanvasGeneral").transform.GetChild(5).gameObject;
        GameObject tempMaskUI = GameObject.Find("TimeBarCanvas").transform.GetChild(0).gameObject;
        GameObject tempUIClock = GameObject.Find("TimeBarCanvas").transform.GetChild(1).gameObject; 
        tempboton.SetActive(false);
        tempday.SetActive(false);
        tileContainer = GameObject.Find("HouseTiles");
        cameraGO = GameObject.Find("Camera");
        cinemachineVirtualCamera = cameraGO.GetComponent<CinemachineVirtualCamera>();
        GameObject[] gOTop;
        int tempTime = 30;
        gOTop = GameObject.FindGameObjectsWithTag("Bottom");
        backgroundTiles = GameObject.Find("FondoCasa");
        CameraShaker(3,30f);
        int tempLength;
        tempLength = gOTop.Length;
        tempMaskUI.SetActive(true);
        tempUIClock.SetActive(true);
        Image tempImage = tempMaskUI.GetComponent<Image>();
        while (tempTime > 0)
        {
            gOTop = GameObject.FindGameObjectsWithTag("Bottom");
            tempImage.fillAmount = (float)tempTime / 30;
            yield return new WaitForSeconds(1f);
            tempTime -= 1;
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
                yield return new WaitForSeconds(1f);
                GameObject lose = Instantiate(looseGO) as GameObject;

                lose.transform.SetParent(GameObject.Find("CanvasGeneral").transform, false);
                lose.transform.GetChild(0).GetComponent<TMP_Text>().text = "Dia " + save.playerValues.currentLevel;
                yield break;
            }

        }
        yield return new WaitForSeconds(0.5f);
        GameObject win = Instantiate(winGO) as GameObject;
        win.transform.SetParent(GameObject.Find("CanvasGeneral").transform, false);
        
        win.transform.GetChild(0).GetComponent<TMP_Text>().text = "Dia " + save.playerValues.currentLevel;
        save.playerValues.currentLevel++;
    }
}
