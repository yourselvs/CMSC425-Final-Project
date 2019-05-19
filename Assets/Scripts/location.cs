using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class location : MonoBehaviour
{
    public Transform turret;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);
        Vector3 center = transform.position + new Vector3(0.0f, 1.25f, 0.0f);
        Debug.DrawLine(center, turret.transform.position);
    }
}
