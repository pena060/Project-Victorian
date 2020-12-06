using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshair_status : MonoBehaviour
{
    public GameObject loading;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1f){
            gameObject.SetActive(true);
        }
        else if(Time.timeScale == 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
