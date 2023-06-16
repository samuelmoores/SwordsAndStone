using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    ColdronController coldronController;
    public bool canExit = false;


    // Start is called before the first frame update
    void Start()
    {
        coldronController = GameObject.Find("Coldrun").GetComponent<ColdronController>();

        canExit = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(coldronController.madePotion)
            {
                canExit = true;
            }
        }
    }

}
