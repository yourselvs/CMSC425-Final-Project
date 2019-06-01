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
    public bool shouldSpawn;

    private bool shouldShow;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;
    private TowerNode nodeSelected;
    private GameObject spawnPrefab;
    private TowerNode nodeHighlighted;
    private Color previousColor;

    // Start is called before the first frame update
    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldShow)
        {
            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out RaycastHit hit, maxHitDistance, teleportMask)) // hit valid target
            {
                TowerNode node = hit.collider.GetComponent<TowerNode>();
                
                if(node != null)
                {
                    Renderer nodeRend = node.GetComponent<Renderer>();
                    Renderer rend = laser.GetComponent<Renderer>();
                    rend.material.shader = Shader.Find("Unlit/Color");

                    if (nodeHighlighted == null)
                    {
                        nodeHighlighted = node;
                        previousColor = nodeRend.material.GetColor("_Color");
                    }
                    else if (node != nodeHighlighted) // if a new node is being pointed at
                    {
                        Renderer nodeHighRend = nodeHighlighted.GetComponent<Renderer>();
                        nodeHighRend.material.SetColor("_Color", previousColor);
                        // get pointers to that node/color
                        nodeHighlighted = node;
                        previousColor = nodeRend.material.GetColor("_Color");
                    }

                    if (node.occupied)
                    {
                        nodeRend.material.SetColor("_Color", Color.red);
                        rend.material.SetColor("_Color", Color.red);
                        shouldSpawn = false;
                    }
                    else
                    {
                        nodeRend.material.SetColor("_Color", Color.green);
                        rend.material.SetColor("_Color", Color.green);
                        shouldSpawn = true;
                    }

                    hitPoint = hit.point;
                    nodeSelected = hit.collider.GetComponent<TowerNode>();

                    

                    ShowLaser(hit.distance);
                }
            }
            else if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, maxHitDistance)) // hit invalid target 
            {
                if (nodeHighlighted != null)
                {
                    Renderer nodeRend = nodeHighlighted.GetComponent<Renderer>();
                    nodeRend.material.SetColor("_Color", previousColor);
                    nodeHighlighted = null;
                }

                hitPoint = hit.point;
                nodeSelected = null;

                Renderer rend = laser.GetComponent<Renderer>();
                rend.material.shader = Shader.Find("Unlit/Color");
                rend.material.SetColor("_Color", Color.red);

                ShowLaser(hit.distance);
                
                shouldSpawn = false;
            }
            else // hit air
            {
                if(nodeHighlighted != null)
                {
                    Renderer nodeRend = nodeHighlighted.GetComponent<Renderer>();
                    nodeRend.material.SetColor("_Color", previousColor);
                    nodeHighlighted = null;
                }

                hitPoint = controllerPose.transform.position + transform.forward * 10;
                nodeSelected = null;

                Renderer rend = laser.GetComponent<Renderer>();
                rend.material.shader = Shader.Find("Unlit/Color");
                rend.material.SetColor("_Color", Color.red);

                ShowLaser(maxHitDistance);
                
                shouldSpawn = false;
            }
        }
        else
        {
            laser.SetActive(false);
        }
    }

    public void LoadSpawn(GameObject spawnPrefab)
    {
        this.spawnPrefab = spawnPrefab;
        shouldShow = true;
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
            nodeSelected?.SpawnTower(spawnPrefab);
        }

        Deactivate();
    }

    public void Deactivate()
    {
        if (nodeHighlighted != null)
        {
            Renderer nodeRend = nodeHighlighted.GetComponent<Renderer>();
            nodeRend.material.SetColor("_Color", previousColor);
            nodeHighlighted = null;
        }

        shouldShow = false;
        shouldSpawn = false;
    }
}

// TODO: make laser red when pointing at occupied nodes
// TODO: give haptic feedback when good/bad placement