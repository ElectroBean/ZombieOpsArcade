﻿using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
    // The amount of health each tank starts with
    public float m_StartingHealth = 100f;

    
    
    private float m_CurrentHealth;
    private bool m_Dead;
    public GameObject mySpawn;
    public GameObject myPrefab;
    public GameObject[] m_enemySpawns;



    private void Awake()
    {
        // sets any gameobject with the "enemyBase" tag, to an enemy base for future functions.
        m_enemySpawns = GameObject.FindGameObjectsWithTag("enemySpawn");
    }

    private void OnEnable()
    {
        // when the tank is enabled, reset the tanks health and whether or not it's dead
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
        SetHealthUI();
        if (gameObject.tag != "Player")
        {
            // chooses a spawn point randomly between 0 and the length of m_enemySpawns
            // "int" makes sure that the random function is an integer instead of a float
            // had to use UnityEngine because "system" has it's own Random function
            int randomSpawn = (int)UnityEngine.Random.Range(0, m_enemySpawns.Length);
            // places the tank that the position of the randomly chosen (number) spawn
            gameObject.transform.position = m_enemySpawns[randomSpawn].transform.position;
        }
        else
        {
            gameObject.transform.position = mySpawn.transform.position;
        }
    }

    private void SetHealthUI()
    {
        //todo
    }

    public void TakeDamage(float amount)
    {
        //reduce current health by the amount of damage done.
        m_CurrentHealth -= amount;

        //change the UI elements appropriately
        SetHealthUI();

        //if the current health is at or beloq zero and it has not yet been registered, call OnDeath.
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        //set the flag so that this function is only called once.
        m_Dead = true;
        // Adds 100 points to player score - Angus
        Points.currentPoints += 100;
        
        Destroy(gameObject);
    }

    private void respawn()
    {
        gameObject.SetActive(true);
        OnEnable(); 
        m_enemySpawns = GameObject.FindGameObjectsWithTag("enemySpawn");
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}