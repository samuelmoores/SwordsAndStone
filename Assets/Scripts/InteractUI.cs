using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{
    GameObject interact;
    GameObject journalEntry;
    GameObject chemicalsUI;
    GameObject pauseMenu;
    PlayerController playerController;

    Text chemicalsText;

    GameManager gameManager;
    JournalController journalController;
    ChemicalController chemicalController;

    // Start is called before the first frame update
    void Start()
    {
        interact = GameObject.Find("Interact");
        journalEntry = GameObject.Find("JournalUI");
        chemicalsUI = GameObject.Find("ChemicalsUI");
        pauseMenu = GameObject.Find("PauseMenu");
        chemicalsText = GameObject.Find("ChemicalsText").GetComponent<Text>();
        chemicalController = GameObject.Find("Chemicals").GetComponent<ChemicalController>();

        playerController = GameObject.Find("CastleGuard").GetComponent<PlayerController>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        journalController = GameObject.Find("Journal").GetComponent<JournalController>();

        interact.SetActive(false);
        journalEntry.SetActive(false);
        chemicalsUI.SetActive(false);
        pauseMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
       
        if(gameManager.interactMessage || playerController.canExit)
        {
            interact.SetActive(true);
        }
        else
        {
            interact.SetActive(false);
        }

        if(journalController.readJournal)
        {
            journalEntry.SetActive(true);

        }
        else
        {
            journalEntry.SetActive(false);

        }

        if(journalController.showChemicals)
        {
            chemicalsUI.SetActive(true);
            chemicalsText.text = "Chemicals  " + gameManager.Dungeon.chemicals + " / 5";

        }else
        {
            chemicalsUI.SetActive(false);

        }

        if(gameManager.gamePaused)
        {
            pauseMenu.SetActive(true);

        }
        else
        {
            pauseMenu.SetActive(false);

        }

    }
}
