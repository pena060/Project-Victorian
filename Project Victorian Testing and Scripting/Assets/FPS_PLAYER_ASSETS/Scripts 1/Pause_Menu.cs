using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pause_Menu : MonoBehaviour
{
    public  bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject crosshair;
    public GameObject reloadingText;
    public GameObject damageOverlay;
    public AudioSource game_Music;
   // public AudioSource chaseAudioWere;
   // public AudioSource attackAudioWere;
   

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;//show mouse in game
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        if (Weapon_shooting.singleton.isReloading)
        {
            reloadingText.SetActive(true);
        }


        if (game_Music != null)
        {
            game_Music.volume = 0.1f;
        }
        damageOverlay.SetActive(true);
        pauseMenuUI.SetActive(false);
        crosshair.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;//hide mouse in game
    }

    void Pause()
    {

        if(game_Music != null)
        {
            game_Music.volume = 0f;
        }
        
        damageOverlay.SetActive(false);
        pauseMenuUI.SetActive(true);
        crosshair.SetActive(false);
        reloadingText.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();

    }

}
