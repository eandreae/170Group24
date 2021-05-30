﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public static Transform[] waypoints;
    public static Transform focus;

    public void Start(){
        waypoints = new Transform[5];
        waypoints[0] = GameObject.Find("RedNeuronWaypoint").transform;
        waypoints[1] = GameObject.Find("GreenNeuronWaypoint").transform;
        waypoints[2] = GameObject.Find("BlueNeuronWaypoint").transform;
        waypoints[3] = GameObject.Find("BananaWaypoint").transform;
        waypoints[4] = GameObject.Find("Oxy1Waypoint").transform;
        waypoints[5] = GameObject.Find("Oxy2Waypoint").transform;
        waypoints[6] = GameObject.Find("KrillWaypoint").transform;
        waypoints[7] = GameObject.Find("BatteryWaypoint").transform;
        
        //Debug.Log(waypoints);
    }

    public static void WhichWaypoint(int wp)
    {
        focus = waypoints[wp];
    }

    // Update is called once per frame
    void Update()
    {
        if (focus != null){
            transform.LookAt(focus);
        }
    }
}
