using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class despawn_gates : MonoBehaviour
{
    GameObject player;
    public int keys;
    
    private void Awake()
    {
        player = GameObject.Find("FPS PLAYER_MAIN");
      
    }

    // Update is called once per frame
    void Update()
    {
        keys = player.GetComponent<pickup>().keyCount;

        if (keys == 4)
        {
            gameObject.SetActive(false);
        }
    }
}
