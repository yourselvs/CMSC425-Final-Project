using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerRocket : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Single fireAction;
    public SteamVR_Action_Vibration hapticAction;
    public Rigidbody cameraRigRb;
    public GameObject firePrefab;
    public float speed;
    
    private float axisValue;
    private GrappleHook grappleHook;
    private GameObject fire;
    private Transform fireTransform;

    public bool RocketActive { get; set; }

    void Start()
    {
        grappleHook = gameObject.GetComponent<GrappleHook>();
        fire = Instantiate(firePrefab);
        fireTransform = fire.transform;
        fireTransform.SetParent(controllerPose.transform);
        fireTransform.localRotation = Quaternion.Euler(90,0,0);
        fire.SetActive(false);
    }

    void Update()
    {
        System.Single axis = fireAction.GetAxis(handType);
        if (!grappleHook.HookActive && axis > 0.2)
        {
            RocketActive = true;
        }
        else if (RocketActive)
        {
            RocketActive = false;
        }
    }

    void FixedUpdate()
    {
        System.Single axis = fireAction.GetAxis(handType);
        if (RocketActive)
        {
            cameraRigRb.AddForce(transform.forward * -1 * speed * axis);
            
            hapticAction.Execute(0, .01f, 150 - 50 * axis, (float)axis / 3, handType);

            float axisModifier = .1f + axis / 10;

            fire.SetActive(true);
            fireTransform.localPosition = new Vector3(0, 0, .1f + axisModifier);
            fireTransform.localScale = new Vector3(axisModifier, axisModifier, axisModifier);
        }
        else
        {
            fire.SetActive(false);
        }
    }
}
