using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idle_anim : MonoBehaviour
{


    [Header("Transform refrences")]//refrences to head and camera transform
    public Transform headTransform;
    public Transform cameraTransform;

    [Header("Head bobbing")]//speeds of bobbing affect 
    public float idleBobFrequency = 0.7f; // frequency of head bobbing
    public float idleBobHorizontalAmp = 0.01f;//horizontal amplitude of the bobbing
    public float idleBobVerticalAmp = 0.02f;//vertical amplitude of the bobbing
    [Range(0, 1)] public float idleHeadBobSmoothing = 0f;//smoothing for head bobbing (avoids camera glitches)

    public bool isIdle;//is the player moving
    private float idleTime;//counter keeps track og how long the player has been moving (bobbing continues from same position everytime)
    private Vector3 targetCameraPosition;//stores camera position

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        idleTime += Time.deltaTime;//increase idle time counter

        targetCameraPosition = headTransform.position + CalculateHeadBobOffset(idleTime);//calculate camera position (calls CalculateBobOffset function) 

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, idleHeadBobSmoothing);//interplate camera position and target position

        if ((cameraTransform.position - targetCameraPosition).magnitude <= 0.001)//if camera location and target position are extremely close, set them equal
            cameraTransform.position = targetCameraPosition;
    }

    private Vector3 CalculateHeadBobOffset(float t)//function calculates the offset of the head bobbing
    {
        float horizontalOffset = 0;//horizontal offset
        float verticalOffset = 0;//vertical offset
        Vector3 offset = Vector3.zero;//stores overall offset to return

        if (t > 0)//if player is idle then calculate vertical offset and horizontal offset
        {
            horizontalOffset = Mathf.Cos(t * idleBobFrequency) * idleBobHorizontalAmp;//calculate horizontal offset
            verticalOffset = Mathf.Sin(t * idleBobFrequency * 2) * idleBobVerticalAmp;//calculate vertical offset
        }


        offset = headTransform.right * horizontalOffset + headTransform.up * verticalOffset;//overall offset

        return offset;//return offset to calculate target camera position
    }
}


