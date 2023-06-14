using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalController : MonoBehaviour
{
    GameManager gameManager;
    PlayerController Player;
    bool foundChemical = false;

    //adjust this to change speed
    float speed = 5f;
    //adjust this to change how high it goes
    float height = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Player = GameObject.Find("CastleGuard").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.position;

        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed);

        //set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, newY * height, pos.z);

        if(Input.GetKeyDown(KeyCode.E) && foundChemical)
        {
            Player.chemicals++;
            gameManager.interactMessage = false;
            Destroy(this.gameObject);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            foundChemical = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foundChemical = false;

        }
    }
}
