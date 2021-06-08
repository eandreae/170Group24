﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ItemScript1P : MonoBehaviour
{
    public GameObject highlight;
    public bool pickedUp;
    public bool active;
    public bool thrown;
    public Transform playerRoot;
    //private GameObject[] playerObjs;
    public string type;
    private float height;
    //private Rigidbody rigidbody;
    //public GameObject glowEffect;
    public Waypoint wp;
    NetworkManager nm;

    public GameObject teleporterIndicator;
    public GameObject batteryIndicator;
    // Start is called before the first frame update
    void Start()
    {
        pickedUp = false;
        active = false;
        thrown = false;
        //playerRoot = GameObject.FindWithTag("PlayerRoot").GetComponent<Transform>();
        //this.rigidbody = this.GetComponent<Rigidbody>();
        //this.rigidbody.isKinematic = false;
        //this.rigidbody.isKinematic = true;
        this.height = this.transform.position.y;
        nm = GameObject.FindObjectOfType<NetworkManager>();
        
        if(nm)
            Object.Destroy(this.gameObject);

        if(teleporterIndicator)
            teleporterIndicator.SetActive(false);

        if(batteryIndicator)
            batteryIndicator.SetActive(false);

    }

    // Update is called once per frame
    public void Update()
    {
        if(!pickedUp){ // code to execute if object is not picked up
            //this.rigidbody.isKinematic = false;

            //if (this.type == "Banana" || this.type == "Coin"){
            //transform.Rotate(0, 0, 90 * Time.deltaTime);
            //}
            this.playerRoot = null;
            if (type == "Battery")
            {
                this.gameObject.GetComponent<Animator>().enabled = true;
            }
        }
        else {  // code to execute if object is picked up
            //if(this.glowEffect)
            //    this.glowEffect.SetActive(false);
            this.active = false;
            this.GetComponent<Rigidbody>().isKinematic = true; // if picked up, item become kinematic
            //Debug.Log(playerRoot.position);

            transform.position = playerRoot.position + 1.2f * (playerRoot.forward) + new Vector3(0, 2f, 0); // sets position relative to the player transform

            if (type == "NeuronRed"){
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 90, -90); // keep rotation at a constant value
                wp.WhichWaypoint(0);
            }
            else if (type == "NeuronGreen")
            {
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 90, -90); // keep rotation at a constant value
                wp.WhichWaypoint(1);
            }
            else if (type == "NeuronBlue")
            {
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 90, -90); // keep rotation at a constant value
                wp.WhichWaypoint(2);
            }
            else if(type == "Banana"){
                transform.rotation = playerRoot.rotation *  Quaternion.Euler(-90, -90, 0); // keep rotation at a constant value
                wp.WhichWaypoint(3);
            } 
            else if(type == "Canister1"){
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 0, 90); // keep rotation at a constant value
                AlterSpeed(6f);
                wp.WhichWaypoint(4);
            }
            else if(type == "Canister2"){
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 0, 90); // keep rotation at a constant value
                AlterSpeed(6f);
                wp.WhichWaypoint(5);
            }
            else if (type == "Sandwich"){
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 90, 0); // keep rotation at a constant value
                wp.WhichWaypoint(6);
            }
            else if (type == "Kebab"){
                transform.position -= new Vector3(0, 2f, 0); // sets position relative to the player transform
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 90, 0); // keep rotation at a constant value
                wp.WhichWaypoint(6);
            }
            else if (type == "Nuke"){
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 90, 0); // keep rotation at a constant value
                wp.DisableArrow();
            }
            else if (type == "Battery")
            {
                transform.rotation = playerRoot.rotation * Quaternion.Euler(0, 90, 0); // keep rotation at a constant value
                this.gameObject.GetComponent<Animator>().enabled = false;
                AlterSpeed(4f);
                wp.WhichWaypoint(8);
                teleporterIndicator.SetActive(true);
                batteryIndicator.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
    	if (other.tag == "Player") {
    		//other.GetComponent<Player>().points++;
        	//Add 1 to points.
        	//Destroy(gameObject); //Destroys coin, when touched.
            //this.playerRoot = other.gameObject.GetComponent<Transform>(); // save the player transform (use this in case of multiple playerobjects)
            this.GetComponent<Rigidbody>().isKinematic = false;
            //if (this.glowEffect && !this.pickedUp && !playerRoot.GetComponent<Player>().holding){
            //    this.glowEffect.SetActive(true);
            //}
        }
        else if (this.active) {
            if (this.type == "Banana" && other.tag == "Gorilla" && !this.pickedUp){
                Object.Destroy(this.gameObject, 0.25f); // destroy object after contact with gorilla
            }
            else if (this.type == "Nuke" && other.tag == "Gorilla" && !this.pickedUp){
                Object.Destroy(this.gameObject, 0.52f); // destroy object after contact with gorilla
            }
        }
    }

    public void AlterSpeed(float newSpeed)
    { 
        playerRoot.GetComponent<Player1P>().ChangeSpeed(newSpeed);
    }

    private void OnTriggerExit(Collider other) {
    	if (other.tag == "Player"){
            if (!this.thrown){  
                //this.rigidbody.isKinematic = true;
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        
    }

    public void highlightOn() {
        highlight.SetActive(true);
    }

    public void highlightOff() {
        highlight.SetActive(false);
    }

    void OnDestroy() {
        if (pickedUp) {
            playerRoot.GetComponent<Player1P>().wpArrow.SetActive(false);
        }
    }

}
