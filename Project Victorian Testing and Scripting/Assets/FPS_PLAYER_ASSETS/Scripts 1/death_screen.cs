using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class death_screen : MonoBehaviour
{
    
    public GameObject deathMenuUI;
    public GameObject crosshair;
    public AudioSource game_Music;
    public GameObject damageBackground;
    public GameObject reloadNotif;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Retry()
    {
        game_Music.volume = 0f;
        SceneManager.LoadScene(1);
        deathMenuUI.SetActive(false);
        crosshair.SetActive(true);
        Time.timeScale = 1f;
    }

    public void deathMenu()
    {
        deathMenuUI.SetActive(true);
        crosshair.SetActive(false);
        damageBackground.SetActive(false);
        reloadNotif.SetActive(false);
        Cursor.lockState = CursorLockMode.None;//show mouse in game  
        Time.timeScale = 0f;
        

    }

    public void MainMenu()
    {
        game_Music.volume = 0f;
        SceneManager.LoadScene("MainMenu");
    }
}
