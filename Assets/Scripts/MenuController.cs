using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MenuController : MonoBehaviour
{
    public enum MenuState
    {
        Inactive,
        Choosing,
        Building,
        Editing,
        Paused
    }

    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean pauseAction;
    public SteamVR_Action_Boolean thumbpadAction;
    public MenuController otherMenu;
    public LaserPointer laserPointer;
    public GameObject chooseTowerMenu,
        buildTowerMenu,
        editTowerMenu,
        pauseMenu;

    [HideInInspector]
    public MenuState state;

    private float timeScale, fixedDeltaTime;
    private GameObject selectedTower;

    // Start is called before the first frame update
    void Start()
    {
        state = MenuState.Inactive;
        timeScale = Time.timeScale;
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (pauseAction.GetStateDown(handType))
        {
            Deactivate();
            otherMenu.Deactivate();
            pauseMenu.SetActive(true);
            Pause();
            state = MenuState.Paused;
        }

        if (state == MenuState.Inactive &&
            thumbpadAction.GetStateDown(handType) &&
            otherMenu.state != MenuState.Paused)
        {
            selectedTower = GetSelectedTower();

            if (selectedTower)
            {
                editTowerMenu.SetActive(true);
                // TODO: Set the object that's being edited somewhere
                //       probably highlight the selected tower somehow?
                state = MenuState.Editing;
            }
            else
            {
                chooseTowerMenu.SetActive(true);
                state = MenuState.Choosing;
            }
        }

    }

    public void Cancel()
    {
        switch (state)
        {
            case MenuState.Choosing:
                chooseTowerMenu.SetActive(false);
                state = MenuState.Inactive;
                break;
            case MenuState.Building:
                buildTowerMenu.SetActive(false);
                chooseTowerMenu.SetActive(true);
                laserPointer.Deactivate();
                // TODO: refund player currency
                state = MenuState.Choosing;
                break;
            case MenuState.Editing:
                editTowerMenu.SetActive(false);
                state = MenuState.Inactive;
                break;
            case MenuState.Paused:
                pauseMenu.SetActive(false);
                Unpause();
                state = MenuState.Inactive;
                break;
        }
    }

    public void SelectTower(GameObject towerPrefab)
    {
        // TODO: check if tower selection is valid
        //       i.e. player has enough currency to purchase tower
        //       and immediately remove currency from player account
        chooseTowerMenu.SetActive(false);
        buildTowerMenu.SetActive(true);
        state = MenuState.Building;
    }

    public void Deactivate()
    {
        laserPointer.Deactivate();

        DeactivateMenus();
    }

    public void DeactivateMenus()
    {
        chooseTowerMenu.SetActive(false);
        editTowerMenu.SetActive(false);
        buildTowerMenu.SetActive(false);
        pauseMenu.SetActive(false);
        state = MenuState.Inactive;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
    }

    public void Unpause()
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = fixedDeltaTime;
    }

    private GameObject GetSelectedTower()
    {
        // TODO: Detect the tower that is being selected
        //       Would be in the direction controller is pointing by a distance variable
        return null;
    }
}
