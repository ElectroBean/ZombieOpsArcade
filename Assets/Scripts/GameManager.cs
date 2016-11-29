using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject[] ammoBoxes;
    private GameObject[] enemies;
    public GameObject[] Doors;
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
    public Button m_NewGame;
    private string[] cheatCode;
    private int index;
    public Button returnNormal;
    public Button Exit;

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
        m_NewGame.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MainCamera.SetActive(true);
        MenuCamera.SetActive(true);
        GunsImage1.gameObject.SetActive(false);
        GunsImage2.gameObject.SetActive(false);
        GunsText1.SetActive(false);
        GunsText2.SetActive(false);
        returnNormal.gameObject.SetActive(false);
        Exit.gameObject.SetActive(true);

        m_GameState = GameState.Start;
    }

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log("active scene is: " + scene.name + ".");

        Pause.gameObject.SetActive(false);
        MainCamera.SetActive(false);
        MenuCamera.SetActive(true);
        Time.timeScale = 0;
        m_TimerText.gameObject.SetActive(false);
        m_MessageText.gameObject.SetActive(false);
        Title.SetActive(true);
        Guide.gameObject.SetActive(true);
        NewGame.gameObject.SetActive(true);

        //checks to see if the current scene name is Prototype_1
        if (SceneManager.GetActiveScene().name == "Prototype_1")
        {
            Instantiate(m_PlayerPrefab, PlayerSpawn.transform.position, PlayerSpawn.transform.rotation);
        }
        resume.gameObject.SetActive(false);
        
    }

    void Update()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        switch (m_GameState)
        {
            case GameState.Start:
              if(SceneManager.GetActiveScene().name == "Prototype_1")
                if(m_Player == null)
                {
                    Instantiate(m_PlayerPrefab, PlayerSpawn.transform.position, PlayerSpawn.transform.rotation);
                }
                Time.timeScale = 0;
                m_MessageText.gameObject.SetActive(false);
                m_TimerText.gameObject.SetActive(false);
                Points.currentPoints = 0;

                // defines the code
                cheatCode = new string[] { "up", "up", "down", "down", "left", "right", "left", "right", "b", "a", "return" };
                index = 0;
                break;

            case GameState.Playing:
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                ammoBoxes = GameObject.FindGameObjectsWithTag("ammo");
               
                Exit.gameObject.SetActive(false);
                if (SceneManager.GetActiveScene().name == "Prototype_1")
                Pause.gameObject.SetActive(true);
                Title.SetActive(false);
                NewGame.gameObject.SetActive(false);
                Guide.gameObject.SetActive(false);
                
                if (SceneManager.GetActiveScene().name == "Prototype_1")
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                
                bool isGameOver = false;
                m_gameTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(m_gameTime);
                m_TimerText.text = string.Format("{0:D2}:{1:D2}",
                            (seconds / 60), (seconds % 60));

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
                    //returns the index to 0 and loads the fps scene
                    index = 0;
                    SceneManager.LoadScene("Prototype_1 - FPS");
                }
                


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
                m_NewGame.gameObject.SetActive(true);
                Menu.gameObject.SetActive(false);
                Pause.gameObject.SetActive(false);
               
                break;
        }

        if(SceneManager.GetActiveScene().name == "Prototype_1 - FPS")
        {
            if (Input.GetButtonDown("escape"))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Menu.gameObject.SetActive(true);
                resume.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
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
    
    //called when guide button is pressed
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
        returnNormal.gameObject.SetActive(false);
    }

    //called when start button is pressed
    public void OnNewGame()
    {
            Time.timeScale = 1;
        returnNormal.gameObject.SetActive(false);
            m_TimerText.gameObject.SetActive(true);
            m_MessageText.text = "";
        if(SceneManager.GetActiveScene().name == "Prototype_1")
        {
            GunsImage1.gameObject.SetActive(true);
            GunsImage2.gameObject.SetActive(true);
            GunsText1.SetActive(true);
            GunsText2.SetActive(true);
        }

        if(SceneManager.GetActiveScene().name == "Prototype_1 - FPS")
        {
            Menu.gameObject.SetActive(false);
            Pause.gameObject.SetActive(false);
        }

        MainCamera.SetActive(true);
        MenuCamera.SetActive(false);

        Points.currentPoints = 0;
        if (SceneManager.GetActiveScene().name == "Prototype_1 - FPS")
        {
            //locks the cursor and makes it invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        m_GameState = GameState.Playing;
    }

    public void onMenu()
    {
        
            Time.timeScale = 0;
            m_GameState = GameState.Start;
            MainCamera.SetActive(false);
            MenuCamera.SetActive(true);
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
            resume.gameObject.SetActive(false);
            Exit.gameObject.SetActive(true);

        if(m_GameState == GameState.Playing)
        {
            if (m_Player != null)
                m_Player.transform.position = PlayerSpawn.transform.position;

            if (enemies.Length != 0)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    Destroy(enemies[i]);
                }
            }

            for (int i = 0; i < ammoBoxes.Length; i++)
            {
                Destroy(ammoBoxes[i]);
            }
            for (int i = 0; i < Doors.Length; i++)
            {
                Doors[i].gameObject.SetActive(true);
            }
        }
            if (SceneManager.GetActiveScene().name == "Prototype_1 - FPS")
            {
                returnNormal.gameObject.SetActive(true);
            }
        
        
    }

    public void onPause()
    {
        Pause.gameObject.SetActive(false);
        resume.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void onResume()
    {
        if(SceneManager.GetActiveScene().name == "Prototype_1")
        Pause.gameObject.SetActive(true);
        resume.gameObject.SetActive(false);

        if(SceneManager.GetActiveScene().name == "Prototype_1 - FPS")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Time.timeScale = 1;
    }

    public void onNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void onReturnNormal()
    {
        SceneManager.LoadScene("Prototype_1");
    }

    public void onExit()
    {
        Application.Quit();
    }
}

