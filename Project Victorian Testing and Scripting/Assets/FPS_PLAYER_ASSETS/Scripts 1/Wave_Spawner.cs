using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_Spawner : MonoBehaviour
{
    public enum SpawnState{SPAWNING, WAITING, COUNTING};
    [System.Serializable]
    public class Wave {
        public string name;
        public Transform enemy;
        public int enemyCount;
        public float rate;
        

    }

    public Wave[] waves;
    private int nextWave = 0;


    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdownTimer;
    private float checkForEnemiesCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawnpoints referenced");
        }

        waveCountdownTimer = timeBetweenWaves;

        
    }

    private void Update()
    {
        if(waveCountdownTimer <= 0)
        {
            waveCountdownTimer = 0;
        }
        if(state == SpawnState.WAITING){//check if enemies are still alive
            if (!isEnemyAlive())//begin a new round
            {
                waveCompletion();
            }
            else
            {
                return;
            }

        }

        if(waveCountdownTimer <= 0)
        {
            if(state != SpawnState.SPAWNING)//start a wave
            {
                StartCoroutine(spawnWave(waves[nextWave]));//start a wave 
            }
        }
        else
        {
            waveCountdownTimer -= Time.deltaTime;//timer minus Time
         }
    }


    void waveCompletion()
    {
        state = SpawnState.COUNTING;
        waveCountdownTimer = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All Waves Complete! looping");
        }
        else
        {
            nextWave++;
          


        }
       
    }
    bool isEnemyAlive()//check if there is still enemies alive in the current wave
    {
        //checkForEnemiesCountdown -= Time.deltaTime;
        //if(checkForEnemiesCountdown <= 0f)
        //{
            //checkForEnemiesCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
       // }
       
        return true;
    }
    IEnumerator spawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;//enemies spawning

        for(int i = 0; i < _wave.enemyCount; i++)//spawn enemies required per wave
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }


        state = SpawnState.WAITING;//wave in progress

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform _spawnP = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //spawn enemy
        Instantiate(_enemy, _spawnP.position, _spawnP.rotation);



    }
}
