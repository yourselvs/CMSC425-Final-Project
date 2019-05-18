using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            ExplodeAndDestroy();
    }

    void ExplodeAndDestroy()
    {
        // do explosions here
        Destroy(this.gameObject);
    }
}
