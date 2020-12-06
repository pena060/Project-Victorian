using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class footsteps : MonoBehaviour
{
    CharacterController cc;
    private AudioSource audio;
    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
    }

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


        float z = Input.GetAxisRaw("Vertical");//move forward and backwards
        float x = Input.GetAxisRaw("Horizontal");//move side to side



        if(cc.isGrounded == true)
        {
            if (isPaused == true && audio.isPlaying == true)
            {
                audio.Stop();
            }

            if (audio.isPlaying == false)
            {    
                if (z == 1)
                {
                    audio.volume = UnityEngine.Random.Range(0.1f, 0.3f);
                    audio.pitch = UnityEngine.Random.Range(0.7f, 0.9f);
                    audio.Play();

                }
                else if (z == -1)
                {
                    audio.volume = UnityEngine.Random.Range(0.1f, 0.3f);
                    audio.pitch = UnityEngine.Random.Range(0.6f, 0.8f);
                    audio.Play();
                }
                else if (x == 1 || x == -1 || (z > 0 && x < 0) || (z < 0 && x > 0) || (z < 0 && x < 0) || (z > 0 && x > 0))
                {
                    audio.volume = UnityEngine.Random.Range(0.1f, 0.3f);
                    audio.pitch = UnityEngine.Random.Range(0.64f, 0.8f);
                    audio.Play();
                }

            }else if (audio.isPlaying == true)
            {

                if (z == 0 && x == 0)
                {
                    audio.Stop();
                }
            }
          
        }else if(cc.isGrounded == false){

            if (audio.isPlaying == true)
            {
                audio.Stop();
            }
        }

        
        

       
    }
}
