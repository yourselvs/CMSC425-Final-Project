using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamager : MonoBehaviour
{
    public float damage;
    public float speed;

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
        //else if (collider.CompareTag("Enemy"))
        //{
        //    Debug.Log("this collider is " + gameObject.GetComponent<Collider>()
        //        + "other collider is " + collider.gameObject.GetComponent<Collider>());
        //    collider.GetComponent<EnemyHealth>().TakeDamage(damage);
        //}
        if (!collider.CompareTag("Tower") && !collider.CompareTag("Player"))
        {
            Debug.Log("this collider is " + gameObject.GetComponent<Collider>()
                + "other collider is " + collider.gameObject.GetComponent<Collider>());
           Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }

        else
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            //if there is a collider 2.0f ahead of the bullet
            if (Physics.Raycast(transform.position, fwd, out hit, 2.0f) &&
                hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
            }


            else
            {
                float step = speed * Time.deltaTime;
                Vector3 targetCenter = target.transform.position + new Vector3(0.0f, 1.25f, 0.0f);
                transform.position = Vector3.MoveTowards(transform.position, targetCenter, step);
                //Debug.DrawLine(transform.position, target.transform.position);
            }
        }
    }
}
