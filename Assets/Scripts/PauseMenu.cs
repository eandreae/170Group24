﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class PauseMenu : MonoBehaviour
{
    bool gameIsPaused = false;

    public GameObject gameManager;
    public GameObject pauseMenuPanel1P;
    public GameObject pauseMenuPanel;
    public GameObject settingsPanel;

    //public Animator outroAnim;

    public AudioSource buttonPress;

    public AudioSource backgroundMusic;

    //public Minimap mm;
    public NetworkManager nm;

    //public Player player;

    public Animator transitionPanel;
    public bool canPause = true;


    void Start()
    {
        //mm = FindObjectOfType<Minimap>();
        nm = FindObjectOfType<NetworkManager>();
        //player = FindObjectOfType<Player>();
        //canPauseViaEscape = true;
    }
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("p")) && canPause)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        buttonPress.Play();
        backgroundMusic.pitch = 0.75f;
        setPausePanelActive(true);
        //mm.canActivateMinimap = false;
        //player.moveSpeed = 0f;
        if(!nm)
            Time.timeScale = 0f;
        gameIsPaused = true;
    }


    public void Resume()
    {
        Time.timeScale = 1f;
        buttonPress.Play();
        backgroundMusic.pitch = 1f;
        setPausePanelActive(false);
        //mm.canActivateMinimap = true;
        //player.moveSpeed = 14f;
        gameIsPaused = false;
    }

    public void OpenSettings()
    {
        buttonPress.Play();
        setPausePanelActive(false);
        settingsPanel.SetActive(true);
        canPause = false;
    }

    public void CloseSettings()
    {
        buttonPress.Play();
        setPausePanelActive(true);
        canPause = true;
        settingsPanel.SetActive(false);
    }

    public void GoBackToMenu()
    {
        Time.timeScale = 1f;
        backgroundMusic.pitch = 1f;
        buttonPress.Play();
        setPausePanelActive(false);
        transitionPanel.Play("PanelOutro");
        Invoke("finallyGoBackToMenu", 1f);
    }

    void finallyGoBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        backgroundMusic.pitch = 1f;
        buttonPress.Play();
        setPausePanelActive(false);
        transitionPanel.Play("PanelOutro");
        Invoke("finallyQuit", 1f);
    }

    void finallyQuit()
    {
        Debug.Log("Quit game!");
        Application.Quit();
    }


    private void setPausePanelActive(bool boolean)
    {
        if (gameManager.GetComponent<GameManager>().singleplayer)
            pauseMenuPanel1P.SetActive(boolean);
        else
            pauseMenuPanel.SetActive(boolean);
    }
}
