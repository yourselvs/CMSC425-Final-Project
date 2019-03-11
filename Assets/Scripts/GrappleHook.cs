using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class GrappleHook : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean fireAction;
    public SteamVR_Action_Vibration hapticAction;
    public GameObject hookPrefab;
    public GameObject ropePrefab;
    public Transform cameraRigTransform;
    public Rigidbody cameraRigRb;
    public float playerSpeed;
    public float hookSpeed;

    private ControllerRocket rocket;
    private GameObject hook;
    private Transform hookTransform;
    private GrappleHookController hookController;
    private Rigidbody hookRigidbody;
    private bool shouldFire;
    private bool fireHook;

    private GameObject rope;
    private Transform ropeTransform;

    public bool HookActive { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rocket = gameObject.GetComponent<ControllerRocket>();
        hook = Instantiate(hookPrefab);
        hookTransform = hook.transform;
        hookController = hook.GetComponent<GrappleHookController>();
        hookRigidbody = hook.GetComponent<Rigidbody>();
        hook.SetActive(false);
        HookActive = false;

        rope = Instantiate(ropePrefab);
        ropeTransform = rope.transform;
    }
    
    void Update()
    {
        if (fireAction.GetState(handType)) // button is pressed down
        {
            if (shouldFire && !rocket.RocketActive) // hook has been released since being disabled or hasn't been disabled
            {
                ropeTransform.position = Vector3.Lerp(controllerPose.transform.position, hookTransform.position, .5f);
                ropeTransform.LookAt(hookTransform);
                ropeTransform.localScale = new Vector3(ropeTransform.localScale.x,
                                                        ropeTransform.localScale.y,
                                                        (hookTransform.position - controllerPose.transform.position).magnitude / 2);
                rope.SetActive(true);

                if (!HookActive)
                {
                    fireHook = true;
                    HookActive = true;
                    hook.SetActive(true);
                    hookTransform.SetPositionAndRotation(controllerPose.transform.position, controllerPose.transform.rotation);
                }
            }
        }
        else if (HookActive) // Trigger is not held. Hook has been fired and needs to be disabled
        {
            DisableHook();
        }
        else // Hook is disabled and needs to be reset
        {
            hookTransform.SetPositionAndRotation(controllerPose.transform.position, controllerPose.transform.rotation);
            rope.SetActive(false);
            shouldFire = true;
        }
        
    }

    void FixedUpdate()
    {
        if (fireHook) // Hook has not been fired
        {
            // Fire hook
            Vector3 force = controllerPose.transform.forward;
            force.Normalize();
            hapticAction.Execute(0, .06f, 100, 1, handType);
            hookRigidbody.velocity = (force * hookSpeed);
            fireHook = false;
        }
        else if (hookController.Hooked) // Hook is fired and attached
        {
            Vector3 force = hookTransform.position - cameraRigTransform.position;
            force.Normalize();

            hapticAction.Execute(0, .01f, 100, .05f, handType);
            cameraRigRb.AddForce(force * playerSpeed);
        }
    }

    public void DisableHook()
    {
        // Disable hook
        rope.SetActive(false);
        hook.SetActive(false);
        hookController.Hooked = false;
        hookRigidbody.isKinematic = false;
        HookActive = false;
        shouldFire = false;
    }
}
