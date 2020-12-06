using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{


    public float mouseSensitivity = 80f;//mouse sensitivity

    public Transform playerBody;// playerbody to attach looking with mouse

    float xRotation = 0f;//will be used to set y axis look

    // Start is called before the first frame update
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;//hide mouse in game
    }

    // Update is called once per frame
    void Update()
    {
        //x axis
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;//look side to side
        playerBody.Rotate(Vector3.up * mouseX);//axis in which the player can rotate (x axis)

        //y axis
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;//look up and down
        xRotation -= mouseY;//decrease x axis rotation based on mouseY
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);//prevent a 360 rotation in the y axis
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);//apply rotation


       
    }
}
