﻿using UnityEngine;
using System.Collections;

public class CrateSpawns : MonoBehaviour
{

    public GameObject cratePrefab;
    public GameObject[] m_CrateSpawns;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 10, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        int randomSpawn = Random.Range(0, m_CrateSpawns.Length);
        Instantiate(cratePrefab, m_CrateSpawns[randomSpawn].transform.position, m_CrateSpawns[randomSpawn].transform.rotation);
    }
}