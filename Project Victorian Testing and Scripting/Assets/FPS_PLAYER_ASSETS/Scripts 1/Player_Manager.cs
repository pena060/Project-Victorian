using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    #region Singleton

    public static Player_Manager instance;

    void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
        

    }

    #endregion

    public GameObject player;
}
