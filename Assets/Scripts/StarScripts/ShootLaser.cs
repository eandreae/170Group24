﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//Used this tutorial for laser beam
//https://www.youtube.com/watch?v=pNE3rfMGEAw&ab_channel=Doc
public class ShootLaser : MonoBehaviour
{
    public Material material;
    LaserBeam beam;
    private bool hitTarget = false;
    public UnityEvent LaserCorrect;
    public Text starColor;
    private bool starFixed = true;
    GameObject mirror1;
    GameObject mirror2;
    GameObject mirror3;
    public List<float> angles = new List<float>();
    static float[] angleList =
    {
        143.0f,
        104.0f,
        295.0f,
        73.0f,
        176.0f,
        -94.0f,
        116.5f,
        -82.5f,
        104.0f
    };


    private void Start()
    {
        mirror1 = GameObject.Find("MirrorCart/Rail");
        mirror2 = GameObject.Find("MirrorCart/Rail (1)");
        mirror3 = GameObject.Find("MirrorCart/Rail (2)");

        ChooseRotation(mirror1.transform.GetChild(0).gameObject, mirror2.transform.GetChild(0).gameObject, mirror3.transform.GetChild(0).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(starColor.text != "green")
        {
            Destroy(GameObject.Find("Laser Beam"));
            beam = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material, hitTarget);
            if (beam.targetHit == true)
            {
                LaserCorrect.Invoke();
                //Debug.Log("WEEEE");
            }
        } else if(GameObject.Find("Laser Beam") != null)
        {
            StartCoroutine("DestroyLaser");
        }
    }

    public void ShuffleMirror()
    {
        GameObject child1 = mirror1.transform.GetChild(1).gameObject;
        GameObject child2 = mirror2.transform.GetChild(1).gameObject;
        GameObject child3 = mirror3.transform.GetChild(1).gameObject;

        float RandZ1 = Random.Range(child1.transform.GetChild(0).gameObject.transform.position.z, child1.transform.GetChild(1).gameObject.transform.position.z);
        float RandZ2 = Random.Range(child2.transform.GetChild(0).gameObject.transform.position.z, child2.transform.GetChild(1).gameObject.transform.position.z);
        float RandZ3 = Random.Range(child3.transform.GetChild(0).gameObject.transform.position.z, child3.transform.GetChild(1).gameObject.transform.position.z);

        mirror1.transform.GetChild(0).gameObject.transform.position = new Vector3(mirror1.transform.GetChild(0).gameObject.transform.position.x, mirror1.transform.GetChild(0).gameObject.transform.position.y, RandZ1);
        mirror2.transform.GetChild(0).gameObject.transform.position = new Vector3(mirror2.transform.GetChild(0).gameObject.transform.position.x, mirror2.transform.GetChild(0).gameObject.transform.position.y, RandZ2);
        mirror3.transform.GetChild(0).gameObject.transform.position = new Vector3(mirror3.transform.GetChild(0).gameObject.transform.position.x, mirror3.transform.GetChild(0).gameObject.transform.position.y, RandZ3);

        ChooseRotation(mirror1.transform.GetChild(0).gameObject, mirror2.transform.GetChild(0).gameObject, mirror3.transform.GetChild(0).gameObject);
    }

    private void ChooseRotation(GameObject cart1, GameObject cart2, GameObject cart3)
    {
        GameObject mirror1 = cart1.transform.GetChild(0).gameObject;
        GameObject mirror2 = cart2.transform.GetChild(0).gameObject;
        GameObject mirror3 = cart3.transform.GetChild(0).gameObject;

        int index = GetIndex();

        mirror1.transform.Rotate(0.0f, angleList[index], 0.0f);
        mirror2.transform.Rotate(0.0f, angleList[index + 1], 0.0f);
        mirror3.transform.Rotate(0.0f, angleList[index + 2], 0.0f);

    }

    private int GetIndex()
    {
        int choose =  Random.Range(0, 3);

        if(choose == 0)
        {
            return 0;
        } else if(choose == 1)
        {
            return 3;
        } else
        {
            return 6;
        }
    }

    IEnumerator DestroyLaser ()
    {
        yield return new WaitForSeconds(1f);
        Destroy(GameObject.Find("Laser Beam"));
    }
}
