using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamager : MonoBehaviour
{
    public float damage;
    public Collider bulletHead;
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

    private void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        //if there is a collider .5f ahead of the bullet
        Debug.DrawRay(transform.position, fwd, Color.red);
        if (Physics.Raycast(transform.position, fwd, out hit, 3.0f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyHealth>().takeDamage(damage);
            }
            if (!hit.collider.CompareTag("Tower"))
            {
                Destroy(gameObject);
            }
        } else
        {
            float step = speed * Time.deltaTime;
            Debug.Log("target is " + target);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
    }
}
