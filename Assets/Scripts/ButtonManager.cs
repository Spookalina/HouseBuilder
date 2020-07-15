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
        gameManager.GetComponent<WeatherState>().WeatherWhen();
    }

    public void OtherScene()
    {
        gameManager.GetComponent<GameManager>().RecolectionTab();
    }

    public void ChangeScene()
    {
        gameManager.GetComponent<GameManager>().HouseTab();
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
