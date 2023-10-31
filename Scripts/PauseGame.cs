using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour{

    private Player playerControls;
    private InputAction menu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private static bool isPaused;
    
    void Awake(){
        pauseMenu.SetActive(false);
        playerControls = new Player();
    }


   /* public void PauseResume(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isPaused)
            {
                Debug.Log("Resume");
                Resume();
            }
            else
            {
                Debug.Log("Paused");
                Pause();
            }
        }
    }*/

    private void OnEnable()
    {
        menu = playerControls.Menu.Escape;
        menu.Enable();

        menu.performed += Pause;

    }

    private void OnDisable()
    {
        menu.Disable();
    }

    void Pause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenu.SetActive(true);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenu.SetActive(false);
        isPaused = false;
    }
    

    public void QuitGame(){
        Application.Quit();
    }
}
