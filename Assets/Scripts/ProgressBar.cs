﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    // Used online resources to get this code.
    // Source : 
    // https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/#timer

    public bool progressing = false;
    public float timeRemaining = 120;

    //public Text timeText;
    public Slider progressSlider;

    public Text NavigationColor;
    public Text ReactorColor;

    GameManager gm;

    void Start()
    {
        progressing = true;
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        // Check if either NavigationColor or ReactorColor are red.
        if ( NavigationColor.text == "red" || ReactorColor.text == "red" ){
            // If either of them are red, set progressing to false.
            progressing = false;
        }
        else {
            // Set progressing to true.
            progressing = true;
        }
        
        // Check if the progressing is true.
        if ( progressing ){
            // Check if the time remaining isn't zero.
            if ( timeRemaining > 0 ){
                // Subtract the time by deltatime.
                timeRemaining -= Time.deltaTime;
                // Display the time remaining.
                displayTime(timeRemaining);
            }
            else {
                // Otherwise, the progress has finished.
                Debug.Log("Game Over! You survived!");
                // Set the time remaining to zero.
                timeRemaining = 0;
                // Set progressing to false.
                progressing = false;
                gm.Victory();
            }
        }
    }

    void displayTime(float time) {

        //float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        progressSlider.value = seconds;

        //timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
