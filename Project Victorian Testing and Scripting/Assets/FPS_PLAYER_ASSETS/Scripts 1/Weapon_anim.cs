using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(walking_anim))]
public class Weapon_anim : MonoBehaviour
{
    public Transform weaponParent;

    private Vector3 weaponParentOrigin;
    private float movementCounter;
    private float idleCounter;
    private Vector3 targetBobWeaponPosition;
    private walking_anim playerWalk;//make a head bobbing component
    private Movement movement;

    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        weaponParentOrigin = weaponParent.localPosition;
        movement = GetComponent<Movement>();
        playerWalk = GetComponent<walking_anim>();
  

        isMoving = false;

    }

    // Update is called once per frame
    void Update()
    {
       
        float x = Input.GetAxis("Horizontal");//move side to side
        float z = Input.GetAxis("Vertical");//move forward and backwards
        if (movement.isJumping  == false)
        {
            if (z == 0 && x == 0)
            {
                weaponMovement(idleCounter, 0.008f, 0.008f);
                idleCounter += Time.deltaTime;
                weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetBobWeaponPosition, Time.deltaTime * 2f);
            }
            else if (z == -1 || x == -1 || x == 1)
            {
                weaponMovement(movementCounter, 0.010f, 0.010f);
                movementCounter += Time.deltaTime * 3;
                weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetBobWeaponPosition, Time.deltaTime * 8f);
            }
            else if ((z > 0 && x < 0) || (z < 0 && x > 0) || (z < 0 && x < 0) || (z > 0 && x > 0))
            {
                weaponMovement(movementCounter, 0.017f, 0.017f);
                movementCounter += Time.deltaTime * 6;
                weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetBobWeaponPosition, Time.deltaTime * 8f);
            }
            else if (movement.isSprinting)
            {
                weaponMovement(movementCounter, 0.016f, 0.016f);
                movementCounter += Time.deltaTime * 9;
                weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetBobWeaponPosition, Time.deltaTime * 8f);
            }
            else if (!playerWalk.isWalking) {
                weaponMovement(idleCounter, 0.008f, 0.008f);
                idleCounter += Time.deltaTime;
                weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetBobWeaponPosition, Time.deltaTime * 2f);
            }
            else
            {
                weaponMovement(movementCounter, 0.016f, 0.016f);
                movementCounter += Time.deltaTime * 3;
                weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetBobWeaponPosition, Time.deltaTime * 10f);
            }
        }
        else
        {
            weaponJump(0.07f);
            movementCounter += Time.deltaTime * 2;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetBobWeaponPosition, Time.deltaTime * 10f);
        }
        
    }
  


    void weaponMovement(float p_z, float p_x_intensity, float p_y_intensity)
    {
        targetBobWeaponPosition = weaponParentOrigin + new Vector3(Mathf.Cos(p_z) * p_x_intensity, Mathf.Sin(p_z * 2) * p_y_intensity, 0);
    }

    void weaponJump(float p_y_intensity)
    {
        targetBobWeaponPosition = weaponParentOrigin + new Vector3(0,  p_y_intensity, 0);
    }
}


