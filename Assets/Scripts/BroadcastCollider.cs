using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastCollider : MonoBehaviour
{
    protected void OnTriggerExit(Collider other)
    {
        BroadcastMessage("TriggerLeft", other);
    }

    protected void OnTriggerEnter(Collider other)
    {
        BroadcastMessage("TriggerEntered", other);
    }
}
