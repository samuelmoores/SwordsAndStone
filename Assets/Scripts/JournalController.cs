using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalController : MonoBehaviour
{
    GameManager gameManager;
    PlayerController playerController;
    public bool readJournal = false;
    public bool foundJournal = false;
    public bool showChemicals = false;


    public string journalEntry =
    "I've lost track of days in this dungeon\n\n" +
    "I must brew a potion\n\n" +
    "There are goblins beneath\n\n" +
    "They hide my chemicals at witching hour\n\n" +
    "The coldrun is the exit\n\n" +
    "And their return\n\n" +
    "-the alchemist\n September 1693";


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("CastleGuard").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && gameManager.interactMessage && foundJournal)
        {
            gameManager.interactMessage = false;
            readJournal = true;
            showChemicals = true;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameManager.interactMessage = true;
            foundJournal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.interactMessage = false;
            readJournal = false;
            foundJournal = false;

        }
    }
}
