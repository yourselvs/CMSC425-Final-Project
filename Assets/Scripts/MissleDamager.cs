using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleDamager : MonoBehaviour
{
    public float damage;
    public float speed;
    public float turnSpeed;
    public float radius;
    public GameObject explosion;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {

        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    void Target(GameObject t)
    {
        target = t;

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Tower" || collider.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(),
                collider.gameObject.GetComponent<Collider>());
        }
        if (!collider.CompareTag("Tower") && !collider.CompareTag("Player"))
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider obj in hits)
            {
                if (obj.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log(obj);
                    obj.GetComponent<EnemyHealth>().TakeDamage(damage);
                }
                DestroyMissle();
            }
        }
    }

    private void DestroyMissle()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            DestroyMissle();
        }

        else
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            //if there is a collider 2.0f ahead of the bullet
            if (Physics.Raycast(transform.position, fwd, out hit, 0.5f) &&
                hit.collider.CompareTag("Enemy"))
            {               
                Collider[] hits = Physics.OverlapSphere(transform.position, 3);
                foreach (Collider obj in hits)
                {
                    if (obj.gameObject.CompareTag("Enemy")) {
                        obj.GetComponent<EnemyHealth>().TakeDamage(damage);                      
                    }
                    DestroyMissle();
                }
            }


            else
            {
                //move towards target
                float step = speed * Time.deltaTime;
                Vector3 targetCenter = target.transform.position + new Vector3(0.0f, 1.25f, 0.0f);
                transform.position = Vector3.MoveTowards(transform.position, targetCenter, step);

                //turn towards target
                //adds 1.25 y to account for the drones being 1.25 above their transform
                Vector3 targetDirection = target.transform.position - transform.position + new Vector3(0.0f, 1.25f, 0.0f);
                float stepTurn = turnSpeed * Time.deltaTime;
                Vector3 turnTowards = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
                transform.rotation = Quaternion.LookRotation(turnTowards);
                Debug.DrawRay(transform.position, targetDirection);
                //Debug.DrawLine(transform.position, target.transform.position);
            }
        }
    }
}
