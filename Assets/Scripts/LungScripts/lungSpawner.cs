﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lungSpawner : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject spawnee;
    string currColor;
    public Text nodeColor;
    Vector3 spawnLoc;
    GameObject canister1;
    GameObject canister2;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        currColor = nodeColor.text;
        canister1 = GameObject.Find("Air_Tank_1");
        canister2 = GameObject.Find("Air_Tank_2");
        canister1.SetActive(true);
        canister2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fix()
    {
        if (nodeColor.text == "red")
        {
            canister1.SetActive(false);
            canister2.SetActive(false);
        }
        else if (nodeColor.text == "yellow")
        {
            canister1.SetActive(true);
            canister2.SetActive(false);
        }
        else if (nodeColor.text == "green")
        {
            canister1.SetActive(true);
            canister2.SetActive(true);
        }

        if (nodeColor.text == "green")
        {
            //currColor = nodeColor.text;
            if (GameObject.Find("Air_Tank_3(Clone)") != null)
            {
                target = GameObject.Find("Air_Tank_3(Clone)");
                Destroy(target);
            }
        }

    }

    public void Spawn()
    {
        spawnLoc = new Vector3(spawnPos.position.x + Random.Range(0.0f, 5.0f), (float)spawnPos.position.y, spawnPos.position.z + Random.Range(0.0f, 5.0f));

        if (nodeColor.text != currColor)
        {
            if (nodeColor.text != "green")
            {
                //currColor = nodeColor.text;
                GameObject temp = Instantiate(spawnee, spawnLoc, spawnPos.rotation);
                temp.GetComponent<Rigidbody>().useGravity = true;
            }
        }

    }
}
