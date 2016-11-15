using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    private GameObject[] ammoBoxes;
    private GameObject[] enemies;
    public GameObject m_Player;
    public Text m_MessageText;
    public Text m_TimerText;
    public GameObject PlayerSpawn;
    public GameObject gameManager;

    private float m_gameTime = 0;
    public float GameTime { get { return m_gameTime; } }
    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };
    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        
        m_GameState = GameState.Start;
    }

    private void Start()
    {
        m_TimerText.gameObject.SetActive(false);
        m_MessageText.gameObject.SetActive(true);
        m_MessageText.text = "Press Enter to Begin";
        Instantiate(m_Player, PlayerSpawn.transform.position, PlayerSpawn.transform.rotation);
    }

    void Update()
    {

        switch (m_GameState)
        {
            case GameState.Start:
                Time.timeScale = 0;
                m_MessageText.text = "Press Enter to Begin";
                m_MessageText.gameObject.SetActive(true);
                m_TimerText.gameObject.SetActive(false);
               // m_Player.GetComponent<Shooting>().MaxAmmo = 30f;
                //m_Player.GetComponent<Shooting>().CurrentAmmo = m_Player.GetComponent<Shooting>().MaxAmmo;
                Points.currentPoints = 0;
                

                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    Time.timeScale = 1;
                    m_TimerText.gameObject.SetActive(true);
                    m_MessageText.text = "";


                    m_GameState = GameState.Playing;
                    
                }
                break;
            case GameState.Playing:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                bool isGameOver = false;
                m_gameTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(m_gameTime);
                m_TimerText.text = string.Format("{0:D2}:{1:D2}",
                            (seconds / 60), (seconds % 60));
                
                if (Input.GetButtonDown("escape"))
                {
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    if (Input.GetButtonDown("enter"))
                    {
                        Time.timeScale = 1;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                }

                
                if (IsPlayerDead() == true)
                {
                    isGameOver = true;

                }
                if (isGameOver == true)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    m_GameState = GameState.GameOver;
                    m_TimerText.gameObject.SetActive(false);
                    
                    
                }
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                m_MessageText.text = "You Died, Press Enter To Try Again!";
                
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_gameTime = 0;
                    DestroyAllObjects();
                    m_Player.GetComponent<PlayerHealth>().CurrentHealth = 100f;
                    m_Player.transform.position = PlayerSpawn.transform.position;
                    m_GameState = GameState.Start;
                    
                }
                break;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    
    
    private bool IsPlayerDead()
    {
        float playerHealth = m_Player.GetComponent<PlayerHealth>().CurrentHealth;

        if(playerHealth <= 0)
        {
            return true;
        }
        return false;
    }

    private void DestroyAllObjects()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(var i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }

        ammoBoxes = GameObject.FindGameObjectsWithTag("ammo");
        for (var i = 0; i < ammoBoxes.Length; i++)
        {
            Destroy(ammoBoxes[i]);
        }
    }
    
}

