using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public Spawner spawner;
    public GameObject explosion;
    public GameObject pickup;

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            ExplodeAndDestroy();
            spawner.RemoveEnemy();
        }
    }

    void ExplodeAndDestroy()
    {
        // do explosions here
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(pickup, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
