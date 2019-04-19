using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean teleportAction;
    public GameObject laserPrefab; 

    public Transform cameraRigTransform;
    public GameObject teleportReticlePrefab;
    public Transform headTransform;
    public Vector3 teleportReticleOffset;
    public LayerMask teleportMask;
    public int maxHitDistance;

    private GameObject reticle;
    private Transform teleportReticleTransform;
    private bool shouldShow, shouldSpawn, readyToPress;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;
    private GameObject spawnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;

        reticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldShow)
        {
            RaycastHit hit;
            
            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, maxHitDistance, teleportMask)) // hit valid target
            {
                hitPoint = hit.point;
                
                Renderer rend = laser.GetComponent<Renderer>();
                rend.material.shader = Shader.Find("Unlit/Color");
                rend.material.SetColor("_Color", Color.green);

                ShowLaser(hit.distance);

                reticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                shouldSpawn = true;
            }
            else if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, maxHitDistance)) // hit invalid target 
            {
                hitPoint = hit.point;

                Renderer rend = laser.GetComponent<Renderer>();
                rend.material.shader = Shader.Find("Unlit/Color");
                rend.material.SetColor("_Color", Color.red);

                ShowLaser(hit.distance);

                reticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                shouldSpawn = false;
            }
            else // hit air
            {
                hitPoint = controllerPose.transform.position + transform.forward * 10;

                Renderer rend = laser.GetComponent<Renderer>();
                rend.material.shader = Shader.Find("Unlit/Color");
                rend.material.SetColor("_Color", Color.red);

                ShowLaser(maxHitDistance);

                reticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                shouldSpawn = false;
            }
        }
        else
        {
            laser.SetActive(false);
            reticle.SetActive(false);
        }
    }

    public void LoadSpawn(GameObject spawnPrefab)
    {
        this.spawnPrefab = spawnPrefab;
        shouldShow = true;
        readyToPress = false;
    }

    private void ShowLaser(float distance)
    {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
                                                laserTransform.localScale.y,
                                                distance);
    }

    public void Spawn()
    {
        if (shouldSpawn)
        {
            Instantiate(spawnPrefab, hitPoint, Quaternion.identity);
        }

        Deactivate();
    }

    public void Deactivate()
    { 
        shouldShow = false;
        shouldSpawn = false;
        reticle.SetActive(false);
    }
}
