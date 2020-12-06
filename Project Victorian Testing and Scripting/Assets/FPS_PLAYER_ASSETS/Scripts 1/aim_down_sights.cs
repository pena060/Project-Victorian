using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(walking_anim))]


public class aim_down_sights : MonoBehaviour
{
    public Vector3 ADS;
    public Vector3 HipFire;
    public GameObject crosshair;
    bool isPaused;
    // Update is called once per frame
    void Update()
    {
        //check to see if game is paused
        if (Time.timeScale == 0f)//game is paused
        {
            isPaused = true;
        }
        else if (Time.timeScale == 1f)//game is unpaused
        {
            isPaused = false;
        }

        if (!isPaused)
        {
            if (Input.GetMouseButton(1))//when right mouse button is held
            {
                crosshair.SetActive(false);
                transform.localPosition = ADS;//aim down sights position


            }
            else//right mouse button not held
            {

                transform.localPosition = Vector3.Slerp(transform.localPosition, HipFire, 2.5f * Time.deltaTime);//hipfire position
                crosshair.SetActive(true);

            }

        }
    }
}
