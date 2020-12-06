using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon_shooting : MonoBehaviour
{
    
    public int damage = 50;
    public float range = 50f;
    public static Weapon_shooting singleton;
    public Camera playerEyes;
    public ParticleSystem muzzleFlash;
    public ParticleSystem shootingSpark;
    public ParticleSystem shootingSmoke;
    public ParticleSystem backgroundFlash;
    public GameObject impactEffect;
    public GameObject hammer1;
    public GameObject hammer2;
    public GameObject Reloadingtext;
    public float impactForce = 80f;
    public float fireRate = 1f;
    public int maxAmmo = 1;
    private int currentAmmo;
    public float reloadTime = 5;
    public bool isReloading = false;
    private float timeToFire = 0f;
    public AudioSource fireSound;
    public LineRenderer bulletTrail;

    public Transform shootPoint;

    private bool isPaused = false;

    public Weapon_Recoil recoil;


    //public Animator anim;

    //damage effect
    public GameObject bloodEffect;

    //bulletholes
    public GameObject bulletHole;

    // Start is called before the first frame update
    void Start()
    {
        recoil = GetComponent<Weapon_Recoil>();
        if(currentAmmo == 0)
        {
            currentAmmo = maxAmmo;
        }
        singleton = this;
    }

    void OnEnable()
    {
        isReloading = false;

    }

    // Update is called once per frame
    void Update()
    {


        //check to see if game is paused
        if (Time.timeScale == 0f)//game is paused
        {
            isPaused = true;
        }
        else if(Time.timeScale == 1f)//game is unpaused
        { 
            isPaused = false; 
        }


        if (isReloading)
        {
            return;
        }


        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }


        //press fire button to shoot (only works if the game is unpaused and if there is no delay set normal = 0)
        if (Input.GetButton("Fire1") && Time.time >= timeToFire && isPaused == false)
        {
            timeToFire = Time.time + 1f / fireRate;
            Shoot();
            recoil.Fire();
        }
       
    }


    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        Reloadingtext.SetActive(true);
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        hammer1.transform.eulerAngles = new Vector3(hammer1.transform.eulerAngles.x + -63.013f, hammer1.transform.eulerAngles.y, hammer1.transform.eulerAngles.z);
        hammer2.transform.eulerAngles = new Vector3(hammer1.transform.eulerAngles.x + -63.013f, hammer1.transform.eulerAngles.y, hammer1.transform.eulerAngles.z);
        isReloading = false;
        Reloadingtext.SetActive(false);
    }

    void Shoot()//function that handles shooting, shooting effects, and damage dealt
    {
        
        backgroundFlash.Play();//muzzle flash for weapon cam
        muzzleFlash.Play();//show muzzle flash
        shootingSpark.Play();//show shooting spark
        shootingSmoke.Play();//show shooting smoke
        fireSound.Play();//play shooting sound
        hammer1.transform.eulerAngles = new Vector3(hammer1.transform.eulerAngles.x + 63.013f, hammer1.transform.eulerAngles.y, hammer1.transform.eulerAngles.z);
        hammer2.transform.eulerAngles = new Vector3(hammer1.transform.eulerAngles.x + 63.013f, hammer1.transform.eulerAngles.y, hammer1.transform.eulerAngles.z);     
        RaycastHit hit;//registers a hit in view

        currentAmmo--;

       if(Physics.Raycast(playerEyes.transform.position, playerEyes.transform.forward, out hit, range))
        {         
            if(hit.collider.tag == "Enemy")
            {
                Debug.Log("enemy hit");
                hit.transform.SendMessageUpwards("takeDamage", damage);                 
                Instantiate(bloodEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                //Instantiate(bloodWound, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
               
                
            }
            else if (hit.collider.tag == "Head")
            {
                Debug.Log("Enemy Head hit");
                hit.transform.SendMessageUpwards("takeDamage", 200);
                Instantiate(bloodEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
               
            }
            else if (hit.collider.tag == "ground")
            {
                Debug.Log("ground hit");
                Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            }
            


            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            if(Physics.Raycast(shootPoint.position, playerEyes.transform.forward, out hit, range))
                spawnTrail(hit.point);

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }

        
    }

    void spawnTrail(Vector3 hit)
    {
        GameObject bulletTrailEffect = Instantiate(bulletTrail.gameObject, shootPoint.position, Quaternion.identity);
        LineRenderer lineRenderer = bulletTrailEffect.GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0, shootPoint.position);
        lineRenderer.SetPosition(1, hit);
        Destroy(bulletTrailEffect, 1f);

    }
}


