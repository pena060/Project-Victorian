using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string newGameScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
	{
        SceneManager.LoadScene(newGameScene);
	}

    public void QuitGame()
	{
        #if UNITY_EDITOR //quits if unity 3d editor is playing the game
                                    UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit(); //quits if application is running game
        #endif
    }
}
