﻿using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStay(Collision col)
    {
        
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerHealth>().CurrentHealth -= 1;
        }
    }
}