using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_music : MonoBehaviour
{
    public AudioSource gameMusic;
    // Start is called before the first frame update
    void Start()
    {
        gameMusic.volume = 0f;
        gameMusic.Play();

        StartCoroutine(startMusic());
    }

    IEnumerator startMusic()
    {
        yield return new WaitForSeconds(0.08f);
        gameMusic.volume = 100f;
    }
}
