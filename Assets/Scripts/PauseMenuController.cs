using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PauseMenuController : MonoBehaviour
{
    public enum PausedMenuState
    {
        Inactive,
        Paused
    }

    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean pauseAction;
    public PauseMenuController otherMenu;
    public GameObject pauseMenu;

    [HideInInspector]
    public PausedMenuState state;

    private float timeScale, fixedDeltaTime;

    // Start is called before the first frame update
    void Start()
    {
        state = PausedMenuState.Inactive;
        timeScale = 1.0f;
        fixedDeltaTime = 0.02f * timeScale;
    }

    void Update()
    {
        if (pauseAction.GetStateDown(handType))
        {
            if (state == PausedMenuState.Paused)
            {
                Cancel();
            }
            else
            {
                Deactivate();
                otherMenu.Deactivate();
                pauseMenu.SetActive(true);
                Pause();
                state = PausedMenuState.Paused;
            }
        }
    }

    public void Cancel()
    {
        if(state == PausedMenuState.Paused)
        {
            pauseMenu.SetActive(false);
            Unpause();
            state = PausedMenuState.Inactive;
        }
    }

    public void Deactivate()
    {
        DeactivateMenus();
    }

    public void DeactivateMenus()
    {
        if (pauseMenu != null)
        { 
            pauseMenu.SetActive(false);
        }
        state = PausedMenuState.Inactive;
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
}
