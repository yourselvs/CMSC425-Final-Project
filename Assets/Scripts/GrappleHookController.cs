using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHookController : MonoBehaviour
{
    public bool Hooked { get; set; }

    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        Hooked = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") ||
            other.CompareTag("Player") ||
            other.CompareTag("Tower"))
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(),
                GetComponent<Collider>());
        }
        else if (other.gameObject.CompareTag("Hookable") ||
            other.gameObject.CompareTag("Base"))
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            Hooked = true;
        }
    }
}
