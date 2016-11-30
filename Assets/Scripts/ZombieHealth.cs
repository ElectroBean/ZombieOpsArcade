using UnityEngine;
using System.Collections;

public class ZombieHealth : MonoBehaviour {
    // The amount of health each tank starts with
    public float m_StartingHealth = 100f;

    private float m_CurrentHealth;
    public bool m_Dead;
    public GameObject mySpawn;
    public GameObject myPrefab;
    public GameObject[] m_enemySpawns;
    public Animator anim;
    public int index;
    public string[] cheatCode;
    private Collider col;
    private Rigidbody rig;
    
    

    private void Awake()
    {
        // sets any gameobject with the "enemyBase" tag, to an enemy base for future functions.
        m_enemySpawns = GameObject.FindGameObjectsWithTag("enemySpawn");
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        
    }

    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
        rig = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }


    public void TakeDamage(float amount)
    {
        //reduce current health by the amount of damage done.
        m_CurrentHealth -= amount;
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
        anim.Play("back_fall");
        Destroy(gameObject, 10);
        gameObject.GetComponent<ZombieDamage>().attacking = false;
        
    }
    // Use this for initialization
    void Start()
    {
        // defines the code
        cheatCode = new string[] { "up", "left", "down", "right", "up", "left", "down", "right", "up", "left", "down", "right" };
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        rig = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        //cheat code 
        // Check if any key is pressed
        if (Input.anyKeyDown)
        {
            // Check if the next key in the code is pressed
            if (Input.GetKeyDown(cheatCode[index]))
            {
                // Add 1 to index to check the next key in the code
                index++;
            }
            // if wrong key is entered, reset the code
            else
            {
                index = 0;
            }
        }
        // If index reaches the length of the cheatCode string, 
        // the entire code was correctly entered
        if (index == cheatCode.Length)
        {
            //returns the index to 0
            index = 0;

            //makes all enemies alive again with full health, and re-enables rigidbody and collider
            m_Dead = false;
            m_CurrentHealth = m_StartingHealth;
            col.enabled = true;
            rig.freezeRotation = false;
        }
    }

    void FixedUpdate()
    {
        if(m_Dead == true)
        {
            anim.Play("back_fall");
        }

        if(m_Dead != true)
        {
            col.enabled = true;
            rig.freezeRotation = false;
        }
    }
}
