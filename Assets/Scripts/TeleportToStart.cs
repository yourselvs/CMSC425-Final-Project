using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToStart : MonoBehaviour
{
    public Rigidbody cameraRigRb;
    public GrappleHook lHook, rHook;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cameraRigRb.MovePosition(Vector3.zero);
            cameraRigRb.MoveRotation(Quaternion.identity);
            cameraRigRb.velocity = Vector3.zero;
            lHook.DisableHook();
            rHook.DisableHook();
        }
    }
}
