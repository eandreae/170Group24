﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;

public class GameManager : MonoBehaviour
{
    public GameObject victoryPanel1P;
    public GameObject victoryPanel;
    public GameObject defeatPanel1P;
    public GameObject defeatPanel;
    public Text causeOfDeath1P;
    public Text causeOfDeath;
    public GameObject pausePanel1P;
    public GameObject pausePanel;
    public GameObject returnOptions;

    public AudioSource backgroundMusic;

    public Animator transitionPanel;

    public float returnDelay = 1f;
    public float deathAnimDuration = 1f;

    public bool singleplayer = true;
    public bool gameStarted = false;

    bool won = false;
    bool lost = false;

    Player1P p1p;
    public Player localp;
    public List<Player> alivePlayers;

    public NetworkManager nm;

    void Start()
    {
        nm = FindObjectOfType<NetworkManager>();
        singleplayer = (nm == null);

        gameStarted = false;
        Time.timeScale = 1f;

        if(singleplayer)
            p1p = FindObjectOfType<Player1P>();
        
        else {
            alivePlayers = new List<Player>();
        }
    }

    void Update(){
        // setting up alivePlayer List and local player for multiplayer
        if(!singleplayer){
            if(gameStarted){
                if(alivePlayers.Count == 0){
                    lost = true;
                    Defeat(0); // trigger defeat screen again
                }
                if(!lost && !won){
                    for (int i = alivePlayers.Count-1; i >= 0; i--){
                        var p = alivePlayers[i];
                        if( p.dead ){
                            alivePlayers.Remove(p);
                        }
                    }
                }
            }
            // if game hasnt started, so make sure all players are loaded in
            else{
                if(alivePlayers.Count > 0)
                    gameStarted = true;
            }
        }
    }

    public void Victory()
    {
        if (!lost)
        {
            //Enabling a victory panel
            if(singleplayer){
                victoryPanel1P.SetActive(true);
                 //Turning off the gameplay music
                backgroundMusic.volume = 0f;
                p1p.canMove = false;
                p1p.GetComponent<Animator>().enabled = false;
                //We need to destroy the pause menu panel so the player can't pause once the game is technically over
                Destroy(pausePanel);
                Destroy(pausePanel1P);
                won = true;
                p1p.hasWonTheGame = true;
                Time.timeScale = 0f;
            }
            else{
                victoryPanel.SetActive(true);
                backgroundMusic.volume = 0f;
                localp.canMove = false;
                localp.GetComponent<Animator>().enabled = false;

                //We need to destroy the pause menu panel so the player can't pause once the game is technically over
                Destroy(pausePanel);
                Destroy(pausePanel1P);

                won = true;
                p1p.hasWonTheGame = true;
                Time.timeScale = 0f;
            }

        }
    }

    public void Defeat(int cause)
    {
        if (cause == 0)
            Debug.Log("multiplayer game over");
        if (!won)
        {   
            // //Enabling a defeat panel
            // Invoke("DefeatPanel", deathAnimDuration);
            if(singleplayer){
                defeatPanel1P.SetActive(true);
                backgroundMusic.volume = 0f;
                Destroy(pausePanel);
                Destroy(pausePanel1P);
                lost = true;
                p1p.canMove = false;
                p1p.hasWonTheGame = true;
                p1p.GetComponent<Animator>().Play("PlayerDeath");
                if (cause == 1)
                {
                    causeOfDeath1P.text = "Cause of Death: Loss of Oxygen";
                }
                else if (cause == 2)
                {
                    causeOfDeath1P.text = "Cause of Death: Beaten to Death by Gorilla";
                }
                else if (cause == 3)
                {
                    causeOfDeath1P.text = "Cause of Death: Obliterated by Self-Destruct Sequence";
                }
                else if (cause == 4)
                {
                    causeOfDeath1P.text = "Cause of Death: Entering the...void?";
                }
            }
            else if (!lost){ // multiplayer death
                defeatPanel.SetActive(true);
                backgroundMusic.volume = 0f;
                Destroy(pausePanel);
                Destroy(pausePanel1P);
                //lost = true;
                localp.canMove = false;

                if (cause == 1)
                {
                    causeOfDeath.text = "Cause of Death: Loss of Oxygen";
                }
                else if (cause == 2)
                {
                    causeOfDeath.text = "Cause of Death: Beaten to Death by Gorilla";
                }
                else if (cause == 3)
                {
                    causeOfDeath.text = "Cause of Death: Obliterated by Self-Destruct Sequence";
                }
                else if (cause == 4)
                {
                    causeOfDeath.text = "Cause of Death: Entering the...void?";
                }
            }
            else{ // multiplayer defeat
                defeatPanel.SetActive(true);
                backgroundMusic.volume = 0f;
                Destroy(pausePanel);
                Destroy(pausePanel1P);


                returnOptions.SetActive(true);
                if (cause == 0) // ma
                {
                    causeOfDeath.text = "All Players have Died";
                }
            }

        }
    }

    void DefeatPanel()
    {
        
        defeatPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        transitionPanel.Play("PanelOutro");
        Invoke("FinallyReturnToMenu", returnDelay);
    }

    void FinallyReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
