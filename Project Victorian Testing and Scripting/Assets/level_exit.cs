using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level_exit : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(2);
            }     
       
    }
}
