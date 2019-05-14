using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetting : MonoBehaviour
{
    public Collider range;
    public float turnSpeed;
    public Damager damager;
    public enum projectileType
    {
        Hitscan,
        AOE
    }
    public projectileType projectile;

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
            FindFurthestAlong();
            Turn();
            Fire();
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

    protected void FindFurthestAlong()
    {
        float distance = -1;
        foreach(GameObject enemy in inRange)
        {
            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
            float traveled = movement.distanceTraveled;
            if (distance < movement.distanceTraveled)
            {
                distance = traveled;
                target = enemy;
            }
        }
        Debug.Log("enemy is " + target.name);
    }

    protected void Turn()
    {
        Vector3 targetDirection = target.transform.position - transform.position;
        float step = turnSpeed * Time.deltaTime;
        Vector3 turnTowards = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(turnTowards);
    }

    protected void Fire()
    {
        if(target == null)
        {
            return;
        }
        EnemyHealth health = target.GetComponent<EnemyHealth>();
        if(projectile == projectileType.Hitscan)
        {
            health.takeDamage(damager.damage);
        }
        

        
    }
}
