using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetting : MonoBehaviour
{
    public Collider range;

    private GameObject target;
    private List<GameObject> inRange = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange.Count > 0)
        {
            FindClosest();
            Attack();
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
        var enemy = other.gameObject;
        if(enemy.CompareTag("Enemy"))
        {
            inRange.Remove(enemy);
            Debug.Log("inRange removed and is now size " + inRange.Count);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject;
        if (enemy.CompareTag("Enemy"))
        {
            inRange.Add(enemy);
            Debug.Log("inRange added and is now size " + inRange.Count);
        }
    }

    protected void Attack()
    {

    }
}
