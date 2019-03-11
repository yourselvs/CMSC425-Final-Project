using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAndTeleport : MonoBehaviour
{
    public Rigidbody cameraRigRb;
    public GrappleHook lHook, rHook;
    public GameObject[] tutorialPrefabs;
    public string sceneToLoad;

    private int levelNum;
    private GameObject level;
    
    void Start()
    {
        levelNum = 0;

        if (tutorialPrefabs.Length > 0)
        {
            level = Instantiate(tutorialPrefabs[levelNum++]);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(levelNum >= tutorialPrefabs.Length)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                cameraRigRb.MovePosition(Vector3.zero);
                cameraRigRb.MoveRotation(Quaternion.identity);
                cameraRigRb.velocity = Vector3.zero;
                lHook.DisableHook();
                rHook.DisableHook();
                Destroy(level);
                level = Instantiate(tutorialPrefabs[levelNum++]);
            }
        }
    }
}
