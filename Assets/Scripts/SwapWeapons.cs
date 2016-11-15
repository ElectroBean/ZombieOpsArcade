﻿using UnityEngine;
using System.Collections;

public class SwapWeapons : MonoBehaviour {

    public GameObject Rifle;
    public GameObject Shotgun;

    public float weaponNumber;

	// Use this for initialization
	void Start () {
        Shotgun.SetActive(false);
        Rifle.SetActive(true);
        weaponNumber = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp("1"))
        {
            Rifle.SetActive(true);
            Shotgun.SetActive(false);
            weaponNumber = 1;
        }

        if (Input.GetButtonUp("2"))
        {
            Rifle.SetActive(false);
            Shotgun.SetActive(true);
            weaponNumber = 2;
        }
	}
}