using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ColdronController : MonoBehaviour
{
    public GameObject Player;
    PlayerController playerController;
    GameManager gameManager;

    bool changeColor = false;
    Color[] colors = { Color.red, Color.blue, Color.magenta, Color.yellow, Color.cyan, Color.black };
    GameObject coldrunLight;
    int chemicalRequirment = 5;

    public GameObject particles;
    GameObject particlesInst;
    ParticleSystem ps;
    ParticleSystem.MainModule pm;
    Vector3 particlesOffset = new Vector3(0.0f, 0.83f, 0.0f);

    bool replinishHealth = false;
    public bool cauldronUsed = false;
    public bool madePotion = false;


    // Start is called before the first frame update
    void Start()
    {
        playerController = Player.GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        particlesInst = Instantiate(particles, transform.position + particlesOffset, Quaternion.identity, transform);
        ps = particlesInst.GetComponent<ParticleSystem>();
        pm = ps.main;
        pm.startColor = Color.green;
        coldrunLight = GameObject.Find("ColdrunLight");

    }

    // Update is called once per frame
    void Update()
    {

        if(changeColor && Input.GetKeyDown(KeyCode.E))
        {
            for(int i = 0; i <= chemicalRequirment; i++)
            {
                if(playerController.chemicals == i)
                {
                    coldrunLight.GetComponent<Light>().color = colors[i];
                    pm.startColor = colors[i];
                    cauldronUsed = true;

                    if (playerController.chemicals == 5)
                    {
                        madePotion = true;
                    }
                }
            }
        }

        if(replinishHealth)
        {
            if (playerController.health >= 100.0f)
            {
                playerController.health = 100.0f;

            }
            else
            {
                playerController.health += Time.deltaTime * 5.0f;

            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            changeColor = true;
            gameManager.interactMessage = true;
            replinishHealth = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            changeColor = false;
            gameManager.interactMessage = false;
            replinishHealth = false;


        }
    }

}
