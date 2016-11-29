using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CrateSpawns : MonoBehaviour
{

    public GameObject cratePrefab;
    public GameObject[] m_CrateSpawns;
    public GameObject[] Crates;

    // Use this for initialization
    void Start()
    {
        //calls the SpawnEnemy function after 10 seconds, then again every 10 seconds
        if(SceneManager.GetActiveScene().name == "Prototype_1")
        {
            InvokeRepeating("SpawnEnemy", 10, 10);
        }
       
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