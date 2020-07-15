using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBehave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1f);
        transform.localPosition += new Vector3(0,0.5f,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
