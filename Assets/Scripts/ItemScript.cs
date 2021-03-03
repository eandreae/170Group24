﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public bool pickedUp;
    public bool active;
    public bool thrown;
    private Transform playerRoot;
    //private GameObject[] playerObjs;
    public string type;
    private float height;
    private Rigidbody rigidbody;
    public GameObject glowEffect;
    GameObject playerObj;
    public Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        pickedUp = false;
        active = false;
        thrown = false;
        //playerRoot = GameObject.FindWithTag("PlayerRoot").GetComponent<Transform>();
        this.rigidbody = this.GetComponent<Rigidbody>();
        //this.rigidbody.isKinematic = false;
        //this.rigidbody.isKinematic = true;
        this.height = this.transform.position.y;
        playerObj = GameObject.FindGameObjectWithTag("Player");

        //playerObjs = GameObject.FindGameObjectsWithTag("Player");

        //if (this.glowEffect)
        //    this.glowEffect.SetActive(false);

    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if(!pickedUp){ // spin; apply gravity
            //this.rigidbody.isKinematic = false;
            
            if (this.type == "Banana" || this.type == "Coin"){
                //transform.Rotate(0, 0, 90 * Time.deltaTime);
            }

        }
        else {
            //if(this.glowEffect)
            //    this.glowEffect.SetActive(false);

            this.rigidbody.isKinematic = true; // if picked up, item become kinematic

            if(type == "Coin"){
                transform.localRotation = Quaternion.Euler(0, 90, -90); // keep rotation at a constant value
            }
            
            else if(type == "Banana"){
                transform.localRotation = Quaternion.Euler(-90, -90, 0); // keep rotation at a constant value
            } else if(type == "Canister")
            {
                AlterSpeed(6f);
            }
            
            //Debug.Log(playerRoot.position);
            transform.localPosition = new Vector3(0f, 1.2f, 0.5f); // sets position relative to the player transform
        }
    }

    private void OnTriggerEnter(Collider other) {
    	if (other.tag == "Player") {
    		//other.GetComponent<Player>().points++;
        	//Add 1 to points.
        	//Destroy(gameObject); //Destroys coin, when touched.
            this.playerRoot = other.gameObject.GetComponent<Transform>(); // save the player transform (use this in case of multiple playerobjects)
            this.rigidbody.isKinematic = false;
            //if (this.glowEffect && !this.pickedUp && !playerRoot.GetComponent<Player>().holding){
            //    this.glowEffect.SetActive(true);
            //}
        }
        else if (this.active) {
            if (this.type == "Banana" && other.tag == "Gorilla" && !this.pickedUp){
                Object.Destroy(this.gameObject, 0.5f); // destroy object after contact with gorilla
            }
            // else if (this.type == "Food" && other.tag == "Stomach"){
            //     Object.Destroy(this.gameObject); // destroy when touching the stomach?
            // }
            // else if (this.type == "Neuron" && other.tag == "Brain"){
            //     Object.Destroy(this.gameObject); // destroy when touching the brain
            // }
        }
    }

    private void OnCollisionEnter(Collision other) {

    }

    public void AlterSpeed(float newSpeed)
    {
        playerScript.ChangeSpeed(newSpeed);
    }

    private void OnTriggerExit(Collider other) {
    	if (other.tag == "Player" && !this.thrown){
        //    this.glowEffect.SetActive(false);
            this.rigidbody.isKinematic = true;
        }
    }
    // private void OnTriggerEnter(Collider other) {
    // 	if(other.name == "Capsule" || other.name == "Player") {
    // 		other.GetComponent<Player>().points++;
    //     	//Add 1 to points.
    //     	Destroy(gameObject); //Destroys coin, when touched.
    //     }
    // }

}
