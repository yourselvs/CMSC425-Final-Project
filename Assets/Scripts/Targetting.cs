using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//modeled partially off of the way the turret defense template did targetting and firing.
//Drew from HitscanAttack, Damager, AttackAffector, and ILauncher
public class Targetting : MonoBehaviour
{
    public float turnSpeed;
    public enum projectileType
    {
        Hitscan,
        AOE,
        Projectile
    }
    public projectileType projectile;
    public ParticleSystem attackParticle;
    public float reloadTime = 1f;
    public GameObject bullet;
    public GameObject projectilePoint;


    private float myTime = 0.0f;
    public GameObject target;
    private List<GameObject> inRange = new List<GameObject>();
    private GameObject shot;
    private GameObject exp;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myTime += Time.deltaTime;
        if(inRange.Count > 0)
        {
            FindFurthestAlong();
            Turn();
            Fire();
        }
    }
    protected void TriggerLeft(Collider other)
    {
        Debug.Log("left");
        var enemy = other.gameObject;
        if(enemy.CompareTag("Enemy"))
        {
            inRange.Remove(enemy);
        }
    }

    protected void TriggerEntered(Collider other)
    {
        Debug.Log("enter");
        var enemy = other.gameObject;
        if (enemy.CompareTag("Enemy"))
        {
            inRange.Add(enemy);
        }
    }

    protected void FindFurthestAlong()
    {
        
        List<GameObject> toDelete = new List<GameObject>();
        float distance = -1;
        foreach (GameObject enemy in inRange)
        {
            if (enemy != null)
            {
                EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
                float traveled = movement.distanceTraveled;
                if (distance < movement.distanceTraveled)
                {
                    distance = traveled;
                    target = enemy;
                }
            }
            else
            {
                toDelete.Add(enemy);
            }
        }

        foreach (GameObject toDel in toDelete)
        {
            inRange.Remove(toDel);
        }
        
    }

    protected void Turn()
    {
        if (target != null)
        {
            //adds 1.25 y to account for the drones being 1.25 above their transform
            Vector3 targetDirection = target.transform.position - transform.position + new Vector3(0.0f, 1.25f, 0.0f);
            float step = turnSpeed * Time.deltaTime;
            Vector3 turnTowards = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(turnTowards);
            Debug.DrawRay(transform.position, targetDirection);
        }
    }

    protected void Fire()
    {
        if(target == null)
        {
            return;
        }
        EnemyHealth health = target.GetComponent<EnemyHealth>();
        if (projectile == projectileType.Projectile)
        {
            if(myTime > reloadTime)
            {
                myTime = 0.0f;
                shot = Instantiate(bullet, projectilePoint.transform.position,
                    projectilePoint.transform.rotation) as GameObject;
                shot.SendMessage("Target", target);

            }
        }
        
    }
}
