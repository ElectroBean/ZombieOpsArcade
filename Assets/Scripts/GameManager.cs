using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    private GameObject[] ammoBoxes;
    private GameObject[] enemies;
    public GameObject m_PlayerPrefab;
    public GameObject m_Player;
    public Text m_MessageText;
    public Text m_TimerText;
    public GameObject PlayerSpawn;
    public GameObject gameManager;
    public GameObject MainCamera;
    public GameObject MenuCamera;
    public GameObject Title;
    public Button Guide;
    public Button NewGame;
    public Image GuideImage1;
    public Image GuideImage2;
    public Image GuideImage3;
    public GameObject GuideText1;
    public GameObject GuideText2;
    public GameObject GuideText3;
    public Image GunsImage1;
    public Image GunsImage2;
    public GameObject GunsText1;
    public GameObject GunsText2;
    public Button Menu;
    public Button Pause;
    public Button resume;

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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MainCamera.SetActive(true);
        MenuCamera.SetActive(true);
        GunsImage1.gameObject.SetActive(false);
        GunsImage2.gameObject.SetActive(false);
        GunsText1.SetActive(false);
        GunsText2.SetActive(false);

        m_GameState = GameState.Start;
    }

    private void Start()
    {
        Pause.gameObject.SetActive(false);
        MainCamera.SetActive(false);
        MenuCamera.SetActive(true);
        Time.timeScale = 0;
        m_TimerText.gameObject.SetActive(false);
        m_MessageText.gameObject.SetActive(false);
        Title.SetActive(true);
        Guide.gameObject.SetActive(true);
        NewGame.gameObject.SetActive(true);
        Instantiate(m_PlayerPrefab, PlayerSpawn.transform.position, PlayerSpawn.transform.rotation);
        resume.gameObject.SetActive(false);
    }

    void Update()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        switch (m_GameState)
        {
            case GameState.Start:
                if(m_Player == null)
                {
                    Instantiate(m_PlayerPrefab, PlayerSpawn.transform.position, PlayerSpawn.transform.rotation);
                }
                Time.timeScale = 0;
                m_MessageText.gameObject.SetActive(false);
                m_TimerText.gameObject.SetActive(false);
                Points.currentPoints = 0;
                break;

            case GameState.Playing:

               
                Pause.gameObject.SetActive(true);
                Title.SetActive(false);
                NewGame.gameObject.SetActive(false);
                Guide.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                bool isGameOver = false;
                m_gameTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(m_gameTime);
                m_TimerText.text = string.Format("{0:D2}:{1:D2}",
                            (seconds / 60), (seconds % 60));

                if (Input.GetMouseButton(1))
                {
                    Time.timeScale = 0.5f;
                }
                if (Input.GetMouseButtonUp(1))
                {
                    Time.timeScale = 1;
                }
                if (m_Player.GetComponent<PlayerHealth>().CurrentHealth <= 0)
                {
                    isGameOver = true;
                }
                if (isGameOver == true)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    m_GameState = GameState.GameOver;
                    m_TimerText.gameObject.SetActive(false);
                    Time.timeScale = 0;
                }
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                m_MessageText.gameObject.SetActive(true);
                m_MessageText.text = "You Died, Please Try Again.";
                
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
    
    public void OnGuide()
    {
        Title.SetActive(false);
        NewGame.gameObject.SetActive(false);
        Guide.gameObject.SetActive(false);
        GuideImage1.gameObject.SetActive(true);
        GuideImage2.gameObject.SetActive(true);
        GuideImage3.gameObject.SetActive(true);
        GuideText1.SetActive(true);
        GuideText2.SetActive(true);
        GuideText3.SetActive(true);
    }

    public void OnNewGame()
    {
            Time.timeScale = 1;
            m_TimerText.gameObject.SetActive(true);
            m_MessageText.text = "";
            MainCamera.SetActive(true);
            MenuCamera.SetActive(false);
            GunsImage1.gameObject.SetActive(true);
            GunsImage2.gameObject.SetActive(true);
            GunsText1.SetActive(true);
            GunsText2.SetActive(true);
        Points.currentPoints = 0;

        m_GameState = GameState.Playing;
    }

    public void onMenu()
    {
    
        m_GameState = GameState.Start;
        MainCamera.SetActive(false);
        MenuCamera.SetActive(true);
        Time.timeScale = 0;
        m_TimerText.gameObject.SetActive(false);
        m_MessageText.gameObject.SetActive(false);
        Title.SetActive(true);
        Guide.gameObject.SetActive(true);
        NewGame.gameObject.SetActive(true);
        GunsImage1.gameObject.SetActive(false);
        GunsImage2.gameObject.SetActive(false);
        GunsText1.SetActive(false);
        GunsText2.SetActive(false);
        GuideImage1.gameObject.SetActive(false);
        GuideImage2.gameObject.SetActive(false);
        GuideImage3.gameObject.SetActive(false);
        GuideText1.SetActive(false);
        GuideText2.SetActive(false);
        GuideText3.SetActive(false);
        Destroy(m_Player);
    }

    public void onPause()
    {
        Pause.gameObject.SetActive(false);
        resume.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void onResume()
    {
        Pause.gameObject.SetActive(true);
        resume.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    
}

