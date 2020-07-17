using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

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
    // Start is called before the first frame update
    void Start()
    {
        save = this.gameObject.GetComponent<Save>();
        save.LoadPlayerValues();

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
        yield return new WaitForSeconds(60f);
        HouseTab();
    }

    // Update is called once per frame
    void Update()
    {
        if(save.playerValues.tutorialDone == true)
        {
            PlayerController();
            DoScenethings();
            if (currentScene == 0 && nextSceneBool == true)
            {
                nextSceneBool = false;
                StartCoroutine(WaitForNextScene());
                Debug.Log("updated");
            }
        }
        else
        {
            
        }
    }
    public void StartGame()
    {
        if(save.playerValues.tutorialDone == false)
        {
            Tutorial();
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

    }
    public void HouseTab()
    {
        StartCoroutine(LoadScene(2));
        currentScene = 1;
        nextSceneBool = false;
    }
    public void Tutorial()
    {
        StartCoroutine(LoadScene(3));
        currentScene = 2;
    }
    IEnumerator LoadScene(int scene)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene);
        yield return new WaitForSeconds(0.01f);
        transitionAnim = GameObject.Find("Panel").GetComponent<Animator>();


    }
    IEnumerator LoadTutorial(int scene)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene);
        yield return new WaitForSeconds(0.01f);
        transitionAnim = GameObject.Find("Panel").GetComponent<Animator>();
        yield return new WaitForSeconds(4f);
        GameObject temp = GameObject.Find("TutorialMadera");
        while(temp != null)
        {
            Time.timeScale = 0f;
        }
        Time.timeScale = 1f;
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
    public void ReFollowPlayer()
    {
    }
    public void Raycasting()
    {
    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(positionOfTouch), Vector2.zero);

        if(hit.collider != null)
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
            
    
    }
}
    