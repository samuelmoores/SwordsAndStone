using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GUIStyle gs;
    public GUIStyle gsJournal;

    PlayerController playerController;
    EnemyController enemyController;
    AudioSource audioSource;


    public bool gamePaused = false;
    bool youDied = false;

    public bool interactMessage = false;

    public struct LevelOne
    {
        public JournalController journalController;
        public ColdronController coldronController;

        public int chemicals;

    }

    public LevelOne Dungeon;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("CastleGuard").GetComponent<PlayerController>();
        enemyController = GameObject.Find("Goblin").GetComponent<EnemyController>();
        audioSource = GameObject.Find("Sound").GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "Dungeon")
        {
            Dungeon.journalController = GameObject.Find("Journal").GetComponent<JournalController>();
            Dungeon.coldronController = GameObject.Find("Coldrun").GetComponent<ColdronController>();
            Dungeon.chemicals = 0;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(!gamePaused)
            {
                gamePaused = true;
                Time.timeScale = 0.0f;
                audioSource.volume = 0.0f;
            }
            else
            {
                gamePaused = false;
                Time.timeScale = 1.0f;
                audioSource.volume = 1.0f;

            }
        }


        if(playerController.isDead)
        {
            youDied = true;

            if(Input.GetKey(KeyCode.P))
            {
                SceneManager.LoadScene("Dungeon");
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }

        }

        if(gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }

    }

    private void OnGUI()
    {
        //GUI.Box(new Rect(50, 25, 125, 25), "Player Health: " + playerController.health);

        //GUI.Box(new Rect(Screen.width - 160, 25, 125, 25), "Press [tab] to pause");

        
        //GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 500, 25), "Press [E]", gs);
        
        if(youDied)
        {
            GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 500, 25), "You Died\n Play again [P] or [Q] to exit", gs);

        }

    }
}
