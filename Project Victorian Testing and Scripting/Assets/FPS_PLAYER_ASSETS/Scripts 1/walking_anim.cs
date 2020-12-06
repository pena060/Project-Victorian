using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Movement))]
public class walking_anim : MonoBehaviour
{
    [Header("Transform refrences")]//refrences to head and camera transform
    public Transform headTransform;
    public Transform cameraTransform;

    [Header("Head bobbing")]//speeds of bobbing affect 
    public float bobFrequency = 4f; // frequency of head bobbing
    public float bobHorizontalAmp = 0.04f;//horizontal amplitude of the bobbing
    public float bobVerticalAmp = 0.04f;//vertical amplitude of the bobbing
    [Range(0, 1)] public float headBobSmoothing = 0.1f;//smoothing for head bobbing (avoids camera glitches)

    public bool isWalking;//is the player moving
    private float walkingTime;//counter keeps track og how long the player has been moving (bobbing continues from same position everytime)
    private Vector3 targetCameraPosition;//stores camera position

    private Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWalking)//if player is not walking, set walking to false
            walkingTime = 0;
        else//player is walking
            walkingTime += Time.deltaTime;//increase walking time
   
        targetCameraPosition = headTransform.position + CalculateHeadBobOffset(walkingTime);//calculate camera position (calls CalculateBobOffset function) 

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, headBobSmoothing);//interplate camera position and target position

         if ((cameraTransform.position - targetCameraPosition).magnitude <= 0.001)//if camera location and target position are extremely close, set them equal
             cameraTransform.position = targetCameraPosition;

    }

    private Vector3 CalculateHeadBobOffset(float t)//function calculates the offset of the head bobbing
    {
        float horizontalOffset = 0;//horizontal offset
        float verticalOffset = 0;//vertical offset
        Vector3 offset = Vector3.zero;//stores overall offset to return


        if (t > 0)//if player is moving and the time of movement is greater than 0 (moving), then calculate horizontal and vertical offset
        {
            if (!movement.isJumping)
            {
                horizontalOffset = Mathf.Cos(t * bobFrequency) * bobHorizontalAmp;//calculate horizontal offset
            }
            verticalOffset = Mathf.Sin(t * bobFrequency * 2) * bobVerticalAmp;//calculate vertical offset
        }

        offset = headTransform.right * horizontalOffset + headTransform.up * verticalOffset;//overall offset

        return offset;//return offset to calculate target camera position
    }
}
