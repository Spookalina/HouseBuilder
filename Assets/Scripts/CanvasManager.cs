using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    
    public CanvasManager instance = null;

     // Start is called before the first frame update
     void Start()
     {

     }
     void Awake()
     {
         /*if (instance == null)
         {
             instance = this;
             DontDestroyOnLoad(this);
         }
         else if(instance != null)
         {
             Destroy(this.gameObject);
         }*/
     }
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
