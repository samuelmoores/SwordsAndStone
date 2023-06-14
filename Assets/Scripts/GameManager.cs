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
    JournalController journalController;
    ColdronController coldronController;
    bool gamePaused = false;
    bool youDied = false;

    public bool interactMessage = false;
    public string[] interactMessages = { "read journal", "use cauldrun", "collect chemical" };


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("CastleGuard").GetComponent<PlayerController>();
        journalController = GameObject.Find("Journal").GetComponent<JournalController>();
        enemyController = GameObject.Find("Goblin").GetComponent<EnemyController>();
        coldronController = GameObject.Find("Coldrun").GetComponent<ColdronController>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
           
            if(!gamePaused)
            {
                Time.timeScale = 0.0f;
                gamePaused = true;

            }
            else
            {
                Time.timeScale = 1.0f;
                gamePaused = false;
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
        GUI.Box(new Rect(50, 25, 250, 25), "Player Health: " + playerController.health);

        GUI.Box(new Rect(1760, 25, 125, 25), "Hold [esc] to pause");

        if(gamePaused)
        {
            GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 500, 25), "Press [W, A, S, D] to move", gs);
            GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 500, 25), "Press [E] to interact", gs);
            GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 0, 500, 25), "Hold [F] to block", gs);
            GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 100, 500, 25), "Press [E] to attack", gs);
            GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 200, 500, 25), "Press [Q] to quit", gs);


        }


        for (int i = 0; i < interactMessages.Length; i++)
        {
            if (interactMessage && i == playerController.interactTag)
            {
                GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 500, 25), "Press [E] to " + interactMessages[i], gs);
            }
        }

        if((playerController.interactTag == 0) && journalController.readJournal)
        {
            GUI.Box(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 400, 800, 800), journalController.journalEntry, gsJournal);

        }


        if(coldronController.cauldronUsed)
        {
            GUI.Box(new Rect(50, 75, 250, 25), "Goblin Health: " + enemyController.health);

        }

        if (playerController.showChemicals)
        {
            GUI.Box(new Rect(50, 150, 250, 40), "Potion Ingredients\nChemicals " + playerController.chemicals + " / 5");

        }

        if(playerController.canExit)
        {
            GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 500, 25), "Press [E] to exit", gs);

        }

        if(youDied)
        {
            GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 500, 25), "You Died\n Play again [P] or [Q] to exit", gs);

        }

    }
}
