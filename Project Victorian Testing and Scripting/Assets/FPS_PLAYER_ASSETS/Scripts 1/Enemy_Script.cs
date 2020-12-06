using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Script : MonoBehaviour
{
    public float lookRadius = 50f;
    Transform target;
    public NavMeshAgent agent;
    public GameObject Body;

    public GameObject dead_corpse;

    public float health = 50f;

    void Start()
    {
        target = Player_Manager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
        float distance = Vector3.Distance(target.position, transform.position);//distance between enemy and player

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance)
            {
                MaintainDirection();
            }
        }

    }

    void MaintainDirection() {//always look at the player 

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void takeDamage(float amount)
    {


        health -= amount;

        if (health <= 0)
        {
            health = 0;
        }

        if (health == 0)
        {
            Die();
        }

    }

    void Die()
    {

        dead_corpse.SetActive(true);

        if (health == 0)
        {        
            Instantiate(dead_corpse, gameObject.transform.position, gameObject.transform.rotation);
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            Debug.Log("spawn corpse");
        }
        StartCoroutine(PauseDestroyCoroutine());
       
        


    }
    IEnumerator PauseDestroyCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.05f);

        //After we have waited 0.08 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
