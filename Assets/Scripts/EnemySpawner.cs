using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    private float SpawnRate = 3f;
    private float SpawnTimer = 0f;
    public GameObject enemyPrefab;
    public GameObject[] m_enemySpawns;

    private void Awake()
    {
        // sets any gameobject with the "enemyBase" tag, to an enemy base for future functions.
        m_enemySpawns = GameObject.FindGameObjectsWithTag("enemySpawn");
    }

    private void OnEnable()
    {
           
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //finds how many enemies are up
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int EnemyCount = Enemies.Length;

        //only spawns if there are 15 or less enemies
	if(EnemyCount <= 14) { 
        if(SpawnTimer <= 0)
        {
            SpawnTimer += SpawnRate;

            int randomSpawn = Random.Range(0, m_enemySpawns.Length);
            Instantiate(enemyPrefab, m_enemySpawns[randomSpawn].transform.position, m_enemySpawns[randomSpawn].transform.rotation);
        }
         if(SpawnTimer > 0)
         {
            SpawnTimer -= Time.deltaTime;
         }
	   }
    }
}
