using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//modeled partially off of the way the turret defense template did targetting and firing.
//Drew from HitscanAttack, Damager, AttackAffector, and ILauncher
public class Targetting : MonoBehaviour
{
    public Collider range;
    public float turnSpeed;
    public int damage;
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
    protected void OnTriggerExit(Collider other)
    {
        var enemy = other.gameObject;
        if(enemy.CompareTag("Enemy"))
        {
            inRange.Remove(enemy);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject;
        if (enemy.CompareTag("Enemy"))
        {
            inRange.Add(enemy);
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
            health.takeDamage(damage * Time.deltaTime);
            attackParticle.transform.position = transform.position;
            attackParticle.transform.LookAt(target.transform.position);
            attackParticle.Play();
            Debug.Log("boom");
        } else if (projectile == projectileType.Projectile)
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
