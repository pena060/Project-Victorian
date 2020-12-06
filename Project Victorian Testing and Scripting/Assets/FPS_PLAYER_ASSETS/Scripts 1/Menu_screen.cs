using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_screen : MonoBehaviour
{
    public GameObject Loading_Screen;
    public GameObject menu_Screen;
    public AudioSource menu_music;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();


   public void PlayGame()
    {

        
        Loading_Screen.gameObject.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MAP, LoadSceneMode.Additive));
        LightProbes.TetrahedralizeAsync();

        StartCoroutine(GetScenesLoadProgress());
    }

    public void QuitGame()
    {
        menu_music.volume = 0f;
        Debug.Log("Quitting game...");
        StartCoroutine(endGame());
    }


    IEnumerator endGame()
    {
        yield return new WaitForSeconds(0.0001f);
        Application.Quit();
    }

    float totalSceneProgress;
    public IEnumerator GetScenesLoadProgress()
    {
        for(int i = 1; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;
                Time.timeScale = 0f;
                yield return null;
            }

       
        }

        SceneManager.LoadScene(1);
        StartCoroutine(loadScene());
        Time.timeScale = 1f;

    }

    public IEnumerator loadScene()
    {
        yield return new WaitForSeconds(0.05f);

        menu_music.volume = 0f;
        menu_music.Stop();
        Loading_Screen.SetActive(false);
        menu_Screen.SetActive(false);
    }





}
