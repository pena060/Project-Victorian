using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end_script_menu : MonoBehaviour
{
    public AudioSource menu_music;
    public GameObject menu_Screen;


    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;//show mouse in game
    }
    public void QuitGame()
    {

        menu_music.volume = 0f;
        Debug.Log("Quitting game...");
        Application.Quit();

    }
}
