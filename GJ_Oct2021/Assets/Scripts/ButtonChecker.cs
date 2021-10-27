using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonChecker : MonoBehaviour
{
    public LevelLoader Lvlloader;
    public GameController GameCon;
    public GameObject Pausemenu;
    public Animator Mainmenuenter;
    public Animator Mainmenuexit;
    public AudioManager manager;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                Mainmenuenter.SetTrigger("EnterPressed");
                Lvlloader.LoadLevelAtIndex(2); // Go to controls menu
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                Mainmenuenter.SetTrigger("Continue");
                Lvlloader.LoadLevelAtIndex(3); // Go to gameplay
            }
            else if (SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 5) 
            {
                manager.Play("Continue");
                Lvlloader.LoadLevelAtIndex(1); // Return to main menu
            }
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                Mainmenuexit.SetTrigger("EscapePressed");
                Application.Quit(); // Quit application
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                Mainmenuexit.SetTrigger("Return");
                Lvlloader.LoadLevelAtIndex(1); // Return to main menu
            }
        }
    }
}
