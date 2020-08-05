using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasGM : MonoBehaviour
{
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = this.GetComponent<Canvas>();
        canvas.enabled = false;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        if(canvas.worldCamera == null)
        {
            canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 1)
        {
            canvas.enabled = true;
        }
    }


}
