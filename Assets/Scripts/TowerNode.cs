using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerNode : MonoBehaviour
{    
    [HideInInspector]
    public bool occupied;

    private Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        occupied = false;
        Transform tform = this.GetComponent<Transform>();
        spawnPosition = tform.position + new Vector3(0, tform.localScale.y / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTower(GameObject towerPrefab)
    {
        Instantiate(towerPrefab, spawnPosition, Quaternion.identity);
        occupied = true;
    }
}
