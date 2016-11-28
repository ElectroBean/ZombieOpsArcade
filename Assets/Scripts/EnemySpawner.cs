using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour {

    public float SpawnRate = 3f;
    public float SpawnTimer = 0f;
    public GameObject enemyPrefab;
    public GameObject[] m_enemySpawns;
    public float MaxEnemies = 15f;
    
    private void Awake()
    {
       
    }

    private void OnEnable()
    {
           
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
                    m_enemySpawns = GameObject.FindGameObjectsWithTag("enemySpawn");

        //finds how many enemies are up
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int EnemyCount = Enemies.Length;
          //15 or less enemies unless its on le secret scene
          if(SceneManager.GetActiveScene().name == "Prototype_1 - FPS")
        {
            if (EnemyCount <= 30)
            {
                if (SpawnTimer <= 0)
                {
                    if (SceneManager.GetActiveScene().name == "Prototype_1 - FPS")
                    {
                        SpawnTimer += 0.5f;
                    }
                    else SpawnTimer += SpawnRate;

                    int randomSpawn = Random.Range(0, m_enemySpawns.Length);
                    Instantiate(enemyPrefab, m_enemySpawns[randomSpawn].transform.position, m_enemySpawns[randomSpawn].transform.rotation);
                }
            }
        }
          if(SceneManager.GetActiveScene().name == "Prototype_1")
        {
            if (EnemyCount <= MaxEnemies)
            {
                if (SpawnTimer <= 0)
                {

                    SpawnTimer += SpawnRate;

                    int randomSpawn = Random.Range(0, m_enemySpawns.Length);
                    Instantiate(enemyPrefab, m_enemySpawns[randomSpawn].transform.position, m_enemySpawns[randomSpawn].transform.rotation);
                }
            }
        }
          
        if (SpawnTimer > 0)
        {
            SpawnTimer -= Time.deltaTime;
        }
    }
}
