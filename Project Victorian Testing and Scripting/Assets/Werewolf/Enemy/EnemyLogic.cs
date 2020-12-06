using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Player_Manager))]

public class EnemyLogic : MonoBehaviour
{
    //public static EnemyLogic singleton;
    public bool canAttack = true;
    

    [SerializeField] private int health = 100;

    Animator anim;
    public float lookRadius = 10f;
    public Rigidbody rb;

    bool isDead = false;
    public GameObject blood_splatter;

    Transform target;
    public NavMeshAgent agent;


    public AudioSource attackGrowl;
    public AudioSource chaseAudio;
    public AudioSource damageSound;
    public AudioSource hitPlayer;

    public float damageAmount = 35f;

    [SerializeField]
    float attackTime = 2f;

    [SerializeField]
    float chaseDistance = 5f;
    [SerializeField]
    float turnSpeed = 5f;

    void Start()
    {
        target = Player_Manager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
       //singleton = this;

    }

    void Update()
    {

        if (Time.timeScale == 0f)
        {
            hitPlayer.Stop();
        }

        if (Time.timeScale == 0f)
        {
            chaseAudio.Stop();
        }

        if (Time.timeScale == 0f)
        {
            attackGrowl.Stop();
        }

        if (Time.timeScale == 0f)
        {
            damageSound.Stop();
        }

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= lookRadius && !isDead && !health_system.singleton.isDead)
        {
            

            if (distance >= chaseDistance && !isDead)
            {
                if (!chaseAudio.isPlaying && Time.timeScale == 1f)
                {
                    chaseAudio.Play();
                }
               



                chasePlayer();
            }
            else if (canAttack && !health_system.singleton.isDead)
            {
                if (!attackGrowl.isPlaying && Time.timeScale == 1f)
                {
                    attackGrowl.Play();
                }
               

                attackPlayer();
            }
            else if(health_system.singleton.isDead)
            {
                disableEnemy();
            }


        }

        if (health <= 0)
        {
            health = 0;
            Dead();
        }

    }

    public void takeDamage(int damage)
    {
        anim.SetTrigger("isHit");
        chaseAudio.Stop();
        attackGrowl.Stop();
        health -= damage;
        if(lookRadius < 500)
        {
           lookRadius = 500;
        }

        agent.speed = 0;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        agent.updatePosition = false;
        anim.SetTrigger("hit");
        if (!damageSound.isPlaying && Time.timeScale == 1f)
        {
            damageSound.Play();
        }
       
        StartCoroutine(EnemyStunned());

    }

    void chasePlayer()
    {
        
        agent.updateRotation = true;
        agent.updatePosition = true;
        agent.SetDestination(target.position);
        
        attackGrowl.Stop();
        anim.SetBool("isWalking", true);
        anim.SetBool("isAttacking", false);
        

    }

    void attackPlayer()
    {
        
        agent.updateRotation = false;
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * turnSpeed);
        agent.updatePosition = false;
        
        chaseAudio.Stop();
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", true);
        if (!hitPlayer.isPlaying && Time.timeScale == 1f)
        {
            hitPlayer.Play();
        }
       
        StartCoroutine(AttackTime());
    }


    void Dead()
    {
        
        chaseAudio.Stop();
        if (!isDead)
        {
            isDead = true;
            anim.SetBool("isNotDead", false);     
            anim.SetTrigger("death");          
            StartCoroutine(spawnBlood());
            if (chaseAudio.isPlaying)
            {
                chaseAudio.Stop();
            }
            if (hitPlayer.isPlaying)
            {
                hitPlayer.Stop();
            }
            StartCoroutine(stopDamageSound());
            Destroy(gameObject, 15);
            
        }
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void disableEnemy()
    {
        canAttack = false;
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);

    }

    IEnumerator AttackTime()
    {
        canAttack = false;    
        yield return new WaitForSeconds(0.2f);
        health_system.singleton.playerTakeDamage(damageAmount);
        yield return new WaitForSeconds(attackTime);
        attackGrowl.Stop();
        canAttack = true;

    }

    IEnumerator stopDamageSound()
    {
        yield return new WaitForSeconds(10f);
        if (damageSound.isPlaying)
        {
            damageSound.Stop();
        }
    }


    IEnumerator EnemyStunned()
    {
        yield return new WaitForSeconds(0.65f);
        agent.speed = 16;
        agent.updatePosition = true;
        agent.updateRotation = true;
        



    }

    IEnumerator spawnBlood()
    {
        yield return new WaitForSeconds(0.65f);
        blood_splatter.SetActive(true);
    }
}
