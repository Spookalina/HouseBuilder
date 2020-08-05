using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (GameManager.isPaused == false)
        {
            gameManager.GetComponent<WeatherState>().WeatherWhen();
            }
    }

    public void OtherScene()
    {
        if (GameManager.isPaused == false)
        {
            gameManager.GetComponent<GameManager>().RecolectionTab();
            }
    }

    public void ChangeScene()
    {
        if (GameManager.isPaused == false)
        {
            gameManager.GetComponent<GameManager>().HouseTab();
            }
    }

    public void FastForward()
    {
        if(GameManager.isPaused == false)
        { 
            gameManager.GetComponent<GameManager>().timerInt = 0;
            gameManager.GetComponent<GameManager>().fastForwardButton.SetActive(false);
        }
    }
    public void ButtonPause()
    {
        gameManager.GetComponent<GameManager>().PauseRecolection();
    }

    public void UpgradeButton(GameObject g)
    {
        //g = gameManager.GetComponent<GameManager>().lastTilePressed;
        if(gameManager.GetComponent<GameManager>().save.playerValues.rock >= 10)
        {
            gameManager.GetComponent<GameManager>().save.playerValues.rock -= 10;
            g.GetComponent<Tile>().tileType = TileType.Stone;
            g.GetComponent<Tile>().MaterialChanger();
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ButtonCooldown()
    {
        this.gameObject.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.GetComponent<Button>().interactable = true;
    }
}
