using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(idle_anim))]
[RequireComponent(typeof(walking_anim))]

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public bool isSprinting = false;
    public bool isJumping = false;
    Vector3 direction = new Vector3(0, 0, 0);

    public bool guiEnabled = false;

    public float stamina = 5f;//current stamina
    public int staminaFallRate = 2;
    public float staminaIncreaseRate = 5f;
    public float maxStamina = 5f;//max stamina
    public float walkSpeed = 3.85f;//regular movement speed
    public float backwardsWalkSpeed = 2.85f;//backwards walking speed
    public float sideStepSpeed = 3f;//side step speed
    public float runSpeed = 6.2f;//running speed (hold left shift)
    float speed;//speed of the player
    public float gravity = -19.60f;//gravity on the player (for falling and jumping)
    Vector3 velocity;//falling velocity

    public float jumpHeight = 0.73f;//the jumping height

    public Transform groundCheck;//used as point on player (feet) that need to touch the ground
    public float groundDistance = 0.4f;//diatance from the ground
    public LayerMask groundMask;//tha layer thats considered ground
    bool isGrounded;//boolean to check if payer has touched the ground

   private walking_anim cb;//make a head bobbing component
   private idle_anim ib;//make a idle bobbing component
    
    Rect staminaRect;//rectangle for stamina hud
    Texture2D staminaTexture;//texture for stamina hud
    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        //sets up start for stamina rectangle on the hud
        staminaRect = new Rect(Screen.width / 10, Screen.height * 9 / 10, Screen.width / 3, Screen.height / 50);
        staminaTexture = new Texture2D(1, 1);
        staminaTexture.SetPixel(0, 0, Color.yellow);
        staminaTexture.Apply();
        stamina = maxStamina;

        //get components from other scripts
        cb = GetComponent<walking_anim>();
        ib = GetComponent<idle_anim>();

        //start idle breathing animation
        ib.isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {

        //check to see if game is paused
        if (Time.timeScale == 0f)//game is paused
        {
            isPaused = true;
        }
        else if (Time.timeScale == 1f)//game is unpaused
        {
            isPaused = false;
        }


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);//form a sphere on the players feet to check if collision with the ground is happening

        if (isGrounded && velocity.y < 0)//check if the player is on the ground and velocity on the y axis is less than zero
        {
            velocity.y = -2f;//change falling velocity to 2.0
            isJumping = false;
        }

        direction.x = Input.GetAxisRaw("Horizontal");//move side to side
        direction.z = Input.GetAxisRaw("Vertical");//move forward and backwards
        direction = Vector3.ClampMagnitude(direction, speed);
        direction.Normalize();

        Vector3 move = transform.right * direction.x + transform.forward * direction.z;//makes sure movement is always in refrence to the direction of the camera

        if (!isPaused)
        {
            //used for sprinting
            if (isGrounded == true)//check to see if player is grounded
            {
                if (Input.GetMouseButton(1))//while aiming change speed and aim animation 
                {
                    speed = sideStepSpeed;
                    cb.bobFrequency = 3.5f;
                    cb.bobHorizontalAmp = 0.03f;
                    cb.bobVerticalAmp = 0.03f;
                    cb.isWalking = true;//player is moving 

                    if (stamina < maxStamina)//if stamina is less than max stamina, regen
                    {
                        
                        stamina += Time.deltaTime / staminaIncreaseRate;//regen stamina
                        ib.idleBobFrequency = 1f;//exhausted breathing frequency increase
                        ib.idleBobVerticalAmp = 0.03f;//exhausted breathing vertical amplitude increase      
                    }

                }
                else if (Input.GetKey(KeyCode.LeftShift) && direction.z == 1 && !Input.GetMouseButton(1))//when left shift is pressed, run
                {
                    isSprinting = true;
                    guiEnabled = true;

                    stamina -= Time.deltaTime * staminaFallRate;//reduce stamina when running
                    speed = runSpeed;//increase movement speed to run speed
                    cb.bobFrequency = 6f;
                    cb.bobHorizontalAmp = 0.05f;
                    cb.bobVerticalAmp = 0.05f;

                    if (stamina <= 0)
                    {
                        isSprinting = false;
                        stamina = 0;
                        speed = walkSpeed;//set speed to be regular walk speed
                        cb.bobFrequency = 4f;
                        cb.bobHorizontalAmp = 0.04f;
                        cb.bobVerticalAmp = 0.04f;
                    }
                }
                else//walk
                {
                    isSprinting = false;
                    guiEnabled = false;
                    if (stamina > maxStamina)
                    {
                        stamina = maxStamina;
                    }

                    if (speed != walkSpeed)// change bobbing and movement speed to regular walking speed (normal values)
                    {
                        speed = walkSpeed;//set speed to be regular walk speed
                        cb.bobFrequency = 4f;
                        cb.bobHorizontalAmp = 0.04f;
                        cb.bobVerticalAmp = 0.04f;
                    }

                    if (stamina < maxStamina && !Input.GetMouseButton(1))//if stamina is less than max stamina, regen
                    {
                        guiEnabled = true;
                        stamina += Time.deltaTime / staminaIncreaseRate;//regen stamina
                        ib.idleBobFrequency = 1.2f;//exhausted breathing frequency increase
                        ib.idleBobVerticalAmp = 0.044f;//exhausted breathing vertical amplitude increase      
                    }
                    else if (stamina >= maxStamina)//full stamina slow down idle breathing
                    {
                        ib.idleBobFrequency = 0.7f;//normal breathing frequency
                        ib.idleBobVerticalAmp = 0.02f;//normal breathing vertical amplitude
                    }

                }
                if (direction.z == 1 || direction.z == -1 || direction.x == 1 || direction.x == -1 || (direction.z > 0 && direction.x < 0) || (direction.z < 0 && direction.x > 0) || (direction.z < 0 && direction.x < 0) || (direction.z > 0 && direction.x > 0))//if player is moving
                {
                    cb.isWalking = true;//player is moving

                    if (direction.z == -1 && !Input.GetMouseButton(1))//walking backward change bobbing and movement speed (not aiming)
                    {
                        speed = backwardsWalkSpeed;
                        cb.bobFrequency = 3.5f;
                        cb.bobHorizontalAmp = 0.035f;
                        cb.bobVerticalAmp = 0.035f;

                    }
                    else if ((direction.x == 1 || direction.x == -1) && !Input.GetMouseButton(1))//walking left or right change bobbing and movement speed (not aiming)
                    {
                        speed = sideStepSpeed;
                        cb.bobFrequency = 4f;
                        cb.bobHorizontalAmp = 0.03f;
                        cb.bobVerticalAmp = 0.03f;

                    }
                    else if (direction.z == 0.7071068 || direction.x == -0.7071068 || direction.z == -0.7071068 || direction.x == 0.7071068 && !Input.GetMouseButton(1)) //diagonal movement (not aiming)
                    {
                        speed = sideStepSpeed;
                        cb.bobFrequency = 3.5f;
                        cb.bobHorizontalAmp = 0.04f;
                        cb.bobVerticalAmp = 0.04f;
                    }
                }
                else
                    cb.isWalking = false;//player is not moving



            }


            controller.Move(move * speed * Time.deltaTime);//move with movement speed


            if (Input.GetButtonDown("Jump") && isGrounded)//check if player is on the ground
            {
                isJumping = true;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);//set correct velocity for jumping
            }


            velocity.y += gravity * Time.deltaTime;//add gravity to velocity on the y axis

            controller.Move(velocity * Time.deltaTime);//add velocity to player

        }
    }

    private void OnGUI()//display GUI (STAMINA)
    {
        if (guiEnabled == true)
        {
            if (Time.timeScale == 1f)
            {
                float ratio = stamina / maxStamina;
                float rectWidth = ratio * Screen.width / 7;
                staminaRect.width = rectWidth;
                GUI.DrawTexture(staminaRect, staminaTexture);

            }
        }
    }

 
}
