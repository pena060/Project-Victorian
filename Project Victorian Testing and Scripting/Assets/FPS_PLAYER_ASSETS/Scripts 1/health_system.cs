using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class health_system : MonoBehaviour
{
    public static health_system singleton;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public bool isDead = false;
    public AudioSource hurtSound;



    [Header("Damage Screen")]
    public Color damageColor;
    public Image damageImage;
    public Image damageBackground;
    float colorSmoothing = 2f;
    public bool isTakingDamage = false;


    private void Awake()
    {
        singleton = this;
    }


    private void Start()
    {
        
        currentHealth = maxHealth;
    }

    private void Update()
    {

        if (Time.timeScale == 0f)
        {
            hurtSound.Stop();
        }
        if (isTakingDamage && !isDead)
        {
            damageImage.color = damageColor;
            damageBackground.color = damageColor;
        }else if (currentHealth <= 10)
        {
            damageImage.color = damageColor;
            damageBackground.color = damageColor;
        }

        else
        {

            damageImage.color = Color.Lerp(damageImage.color, Color.clear, colorSmoothing * Time.deltaTime);
            damageBackground.color = Color.Lerp(damageImage.color, Color.clear, colorSmoothing * Time.deltaTime);
        }

        isTakingDamage = false;


        if(currentHealth <= 0)
        {
            Die();
        }
     
    }

    public void playerTakeDamage(float damage)
    {

        if (!hurtSound.isPlaying)
        {
            hurtSound.Play();
        }
        if (currentHealth > 0)
        {

            if (damage >= currentHealth)
            {
                        
                Die();
                currentHealth = 0;
            }
            else 
            {
                isTakingDamage = true;
                currentHealth -= damage;
            }           
            
        }
    }

    public void Die()
    {
        isDead = true;
        isTakingDamage = true;
        death_screen death = GetComponent<death_screen>();
        if(death != null)
        {
            death.deathMenu();
        }
       
    }




}
