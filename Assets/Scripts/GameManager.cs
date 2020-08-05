using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameManager instance;
    public Touch touch;
    public Vector2 positionOfTouch;
    public Save save;
    public int currentScene = 10;
    public Weather[] weathers;
    public Animator transitionAnim;
    public GameObject player;
    public GameObject floatingText;
    public bool nextSceneBool;
    public GameObject TutorialBlackLayer1;
    public GameObject tmpGo;
    public GameObject fastForwardButton;
    public bool canTap = false;
    public bool isInStart = true;
    public int timerInt;
    public GameObject smokeParticles;
    public GameObject lastTilePressed;
    public static bool isPaused = false;
    public static bool isUpgradeTime = false;
    // Start is called before the first frame update
    void Start()
    {
        save = this.gameObject.GetComponent<Save>();
        save.LoadPlayerValues();
        canTap = true;
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance != null)
        {
           Destroy(this.gameObject);
        }

        
        
    }
    
    IEnumerator WaitForNextScene()
    {
        
        Debug.Log("coroutine");
        timerInt = 60;
        while(timerInt >= 1)
        {
            yield return new WaitForSeconds(1f);
            timerInt--;
            GameObject timer = GameObject.Find("TimeBarCanvas").transform.GetChild(1).gameObject;
            timer.GetComponent<RectTransform>().localPosition += new Vector3 (15.2f, 0);
        }
        
        if(save.playerValues.tuorialDone2 == false)
        {
            HouseTutorial();
        }

        else
        {
            HouseTab();
        }
        
    }

    public void PauseRecolection()
    {
        if(isPaused == false)
        {
            Time.timeScale = 0f;
            isPaused = true;
            canTap = false;
        }
        else{
            Time.timeScale = 1f;
            isPaused = false;
            canTap = true;
        }
        
    }


    // Update is called once per frame
    void Update()
    {

            PlayerController();
            DoScenethings();
            if (currentScene == 0 && nextSceneBool == true)
            {
                nextSceneBool = false;
                StartCoroutine(WaitForNextScene());
                Debug.Log("updated");
            }

            else if(currentScene == 2 || currentScene == 4)
            {
                Upgrader();
            }
            if(isInStart != true)
            {
                GameObject tempUI;
                tempUI = this.transform.GetChild(0).gameObject;
                tempUI.SetActive(true);
                tempUI.transform.GetChild(1).GetComponent<TMP_Text>().text = "" + save.playerValues.wood;
                tempUI.transform.GetChild(4).GetComponent<TMP_Text>().text = "" + save.playerValues.rock;

            }
    }
    public void StartGame()
    {
        if(save.playerValues.tutorialDone == false)
        {
            Tutorial();
            canTap = false;
        }
        else
        {
            RecolectionTab();
        }
    }
    public void RecolectionTab()
    {
        StartCoroutine(LoadScene(1));
        currentScene = 0;
        nextSceneBool = true;
        isInStart = false;

    }
    public void HouseTab()
    {
        StartCoroutine(LoadScene(2));
        currentScene = 1;
        nextSceneBool = false;
        isUpgradeTime = true;
    }
    public void Tutorial()
    {
        StartCoroutine(LoadTutorial(3));
        currentScene = 2;
        isInStart = false;
        

    }

    public void HouseTutorial()
    {
        StartCoroutine(LoadScene(4));
        currentScene = 3;
        //save.playerValues.currentLevel = 1;
    }
    IEnumerator LoadScene(int scene)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene);
        yield return new WaitForSeconds(0.01f);
        transitionAnim = GameObject.Find("Panel").GetComponent<Animator>();
        if(scene == 1)
        {
            //GameObject timer = GameObject.Find("Canvas").transform.GetChild(3).transform.GetChild(0).gameObject;
            //GetComponent<TMP_Text>().text = "" + timerInt;
            fastForwardButton = GameObject.Find("Canvas").transform.GetChild(4).gameObject;
            fastForwardButton.SetActive(true);
        }
        if(scene == 2)
        {
            canTap = false;
            GameObject tempday = GameObject.Find("TextTMPHouse");
            tempday.GetComponent<TMP_Text>().text = "Dia " + save.playerValues.currentLevel;
            for (int i = 0; i < save.playerValues.saveTile.Length; i++)
            {
                GameObject temp = GameObject.Find("WoodPreview2 (" + i + ")");
                temp.GetComponent<Tile>().tileType = save.playerValues.saveTile[i];
            }
        }


    }
    IEnumerator LoadTutorial(int scene)
    {
        
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene);
        yield return new WaitForSeconds(0.01f);
        player = GameObject.Find("Player");
        tmpGo = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        transitionAnim = GameObject.Find("Panel").GetComponent<Animator>();
        
        yield return new WaitForSeconds(4f);
        GameObject temp = GameObject.Find("TutorialMadera");
        TutorialBlackLayer1 = GameObject.Find("Black").transform.GetChild(0).gameObject;
        while (temp != null)
        {
            canTap = true;
            TutorialBlackLayer1.SetActive(true);
            tmpGo.SetActive(true);
            player.GetComponent<PlayerManager>().speed = 0;
            player.GetComponent<Animator>().SetBool("Stop", true);
            yield return new WaitForSeconds(0.1f);
            Debug.Log("Sigue Funcando");
        }
        tmpGo.GetComponent<TMP_Text>().text = "Necesitaras muchos mas \nmateriales para que\ntu casa no se desplome";
        canTap = false;
        yield return new WaitForSeconds(3f);
        TutorialBlackLayer1.SetActive(false);
        tmpGo.SetActive(false);
        player.GetComponent<Animator>().SetBool("Stop", false);
        player.GetComponent<PlayerManager>().speed = 2.5f;
        temp = GameObject.Find("TreeTutorial");
        yield return new WaitForSeconds(4.5f);
        
        while (temp != null)
        {
            TutorialBlackLayer1.SetActive(true);
            tmpGo.GetComponent<TMP_Text>().text = "Este es un arbol\npuedes conseguir mas \nmadera de aqui,\npegale unas veces";
            tmpGo.SetActive(true);
            canTap = true;
            player.GetComponent<PlayerManager>().speed = 0;
            player.GetComponent<Animator>().SetBool("Stop", true);
            yield return new WaitForSeconds(0.1f);
            Debug.Log("Sigue Funcando");
        }

        tmpGo.GetComponent<TMP_Text>().text = "Muy bien! sigue asi\n tendras 60 segundos para \nconseguir mas materiales \ny superar la tempestad";
        yield return new WaitForSeconds(4f);
        save.playerValues.tutorialDone = true;
        RecolectionTab();

    }
    public void PlayerController()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    positionOfTouch = touch.position;
                    Raycasting();
                    break;
            }
        }
    }
    public void DoScenethings()
    {
        switch(currentScene)
        {
            case 1:
                
                break;
        }
    }
    public void Upgrader()
    {

    }
    public void Raycasting()
    {
    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(positionOfTouch), Vector2.zero);

    switch(currentScene)
    {
        case 0:
        if(hit.collider != null && canTap == true)
        {
            if (hit.collider.tag == "Wood")
            {
                GameObject tempGO;
                int temp = Mathf.RoundToInt(Random.Range(save.playerValues.minLuck, save.playerValues.minLuck + 1 + (save.playerValues.minLuck * save.playerValues.luck)));
                save.playerValues.wood += temp;
                tempGO = Instantiate(floatingText, hit.transform.position, Quaternion.identity);
                tempGO.transform.GetChild(0).GetComponent<TextMesh>().text = "+"+temp;
                Destroy(hit.transform.gameObject);
            }
            else if (hit.collider.tag == "Tree")
            {
                GameObject tempGO;
                int temp = Mathf.RoundToInt(Random.Range(save.playerValues.minLuck, save.playerValues.minLuck + 1 + (save.playerValues.minLuck * save.playerValues.luck)));
                save.playerValues.wood += temp;
                tempGO = Instantiate(floatingText, hit.transform.position, Quaternion.identity);
                tempGO.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + temp;
                hit.transform.gameObject.GetComponent<Item>().numberOfUses--;
            }
            else if (hit.collider.tag == "Trash")
            {

            }
            else if (hit.collider.tag == "Rock")
            {
                GameObject tempGO;
                int temp = Mathf.RoundToInt(Random.Range(save.playerValues.minLuck, save.playerValues.minLuck + 1 + (save.playerValues.minLuck * save.playerValues.luck)));
                save.playerValues.rock += temp;
                tempGO = Instantiate(floatingText, hit.transform.position, Quaternion.identity);
                tempGO.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + temp;
                Destroy(hit.transform.gameObject);
            }
            else if (hit.collider.tag == "BigRock")
            {
                GameObject tempGO;
                int temp = Mathf.RoundToInt(Random.Range(save.playerValues.minLuck, save.playerValues.minLuck + 1 + (save.playerValues.minLuck * save.playerValues.luck)));
                save.playerValues.rock += temp;
                tempGO = Instantiate(floatingText, hit.transform.position, Quaternion.identity);
                tempGO.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + temp;
                hit.transform.gameObject.GetComponent<Item>().numberOfUses--;
            }
        }
            break;

        case 1:
        if (hit.collider != null && canTap == true)
        {
            if (hit.collider.tag == "WoodTile" || hit.collider.tag == "Roof" || hit.collider.tag == "Bottom")
            {
                if(save.playerValues.wood >= 1 && hit.transform.gameObject.GetComponent<Tile>().health < 1)
                {
                    hit.transform.gameObject.GetComponent<Tile>().health += 0.1f;
                    save.playerValues.wood -= 1;
                    Instantiate(smokeParticles,hit.transform.position,Quaternion.identity);
                }
                        
            }
                    
        }
        
        else if (hit.collider != null && isUpgradeTime == true)
        {
            if (hit.collider.tag == "WoodTile" || hit.collider.tag == "Roof" || hit.collider.tag == "Bottom" )
            {
                
                if(lastTilePressed == null)
                {
                    lastTilePressed = hit.transform.gameObject;
                    lastTilePressed.transform.GetChild(2).gameObject.SetActive(true);
                }
                else
                {
                    if(lastTilePressed != hit.transform.gameObject)
                    {
                        lastTilePressed.transform.GetChild(2).gameObject.SetActive(false);
                        lastTilePressed = null;
                    }
                    else
                    {
                        lastTilePressed.transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
            
            }

            
        }
        else if(hit.collider != null && lastTilePressed != null && hit.collider.tag == "Button")
        {
            lastTilePressed.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if(hit.collider == null)
            {
                if(lastTilePressed != null)
                {                  
                    lastTilePressed.transform.GetChild(2).gameObject.SetActive(false);
                    lastTilePressed = null;
                }
            }
            break;        
        
        case 2:
                if (hit.collider != null && canTap == true)
                {
                    if (hit.collider.tag == "Wood")
                    {
                        GameObject tempGO;
                        int temp = Mathf.RoundToInt(Random.Range(save.playerValues.minLuck, save.playerValues.minLuck + 1 + (save.playerValues.minLuck * save.playerValues.luck)));
                        save.playerValues.wood += temp;
                        tempGO = Instantiate(floatingText, hit.transform.position, Quaternion.identity);
                        tempGO.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + temp;
                        Destroy(hit.transform.gameObject);
                    }
                    else if (hit.collider.tag == "Tree")
                    {
                        GameObject tempGO;
                        int temp = Mathf.RoundToInt(Random.Range(save.playerValues.minLuck, save.playerValues.minLuck + 1 + (save.playerValues.minLuck * save.playerValues.luck)));
                        save.playerValues.wood += temp;
                        tempGO = Instantiate(floatingText, hit.transform.position, Quaternion.identity);
                        tempGO.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + temp;
                        hit.transform.gameObject.GetComponent<Item>().numberOfUses--;
                    }
                    else if (hit.collider.tag == "Trash")
                    {

                    }
                    else if (hit.collider.tag == "Rock")
                    {
                        GameObject tempGO;
                        int temp = Mathf.RoundToInt(Random.Range(save.playerValues.minLuck, save.playerValues.minLuck + 1 + (save.playerValues.minLuck * save.playerValues.luck)));
                        save.playerValues.rock += temp;
                        tempGO = Instantiate(floatingText, hit.transform.position, Quaternion.identity);
                        tempGO.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + temp;
                        Destroy(hit.transform.gameObject);
                    }
                    else if (hit.collider.tag == "BigRock")
                    {
                        GameObject tempGO;
                        int temp = Mathf.RoundToInt(Random.Range(save.playerValues.minLuck, save.playerValues.minLuck + 1 + (save.playerValues.minLuck * save.playerValues.luck)));
                        save.playerValues.rock += temp;
                        tempGO = Instantiate(floatingText, hit.transform.position, Quaternion.identity);
                        tempGO.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + temp;
                        hit.transform.gameObject.GetComponent<Item>().numberOfUses--;
                    }
                }
                break;

        case 3:
                if (hit.collider != null && canTap == true)
                {
                    if (hit.collider.tag == "WoodTile" || hit.collider.tag == "Roof" || hit.collider.tag == "Bottom")
                    {
                        if (save.playerValues.wood >= 1 && hit.transform.gameObject.GetComponent<Tile>().health < 1)
                        {
                            hit.transform.gameObject.GetComponent<Tile>().health += 0.1f;
                            save.playerValues.wood -= 1;
                            Instantiate(smokeParticles,hit.transform.position,Quaternion.identity);
                            Debug.Log("K");
                        }

                    }

                }
                break;
    }
        
            
    
    }

    
}
    