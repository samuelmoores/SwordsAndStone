using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWinController : MonoBehaviour
{
    public GUIStyle gs;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.P))
        {
            SceneManager.LoadScene("Dungeon");
        }

    }

    private void OnGUI()
    {
        GUI.Box(new Rect(250, 70, 500, 25), "You escaped the dungeon!", gs);
        GUI.Box(new Rect(Screen.width / 2 + 200, Screen.height / 2 + 0, 500, 25), "Press [P] to play again", gs);
        GUI.Box(new Rect(Screen.width / 2 + 200, Screen.height / 2 + 200, 500, 25), "Press [Q] to quit", gs);



    }
}
