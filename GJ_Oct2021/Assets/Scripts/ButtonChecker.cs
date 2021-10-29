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
            switch (SceneManager.GetActiveScene().buildIndex) 
            {
                case 0:
                    Lvlloader.LoadNextLevel(); // Go to main menu
                    break;
                case 1:
                    Mainmenuenter.SetTrigger("EnterPressed");
                    Lvlloader.LoadNextLevel(); // Go to controls menu
                    break;
                case 2:
                    Mainmenuenter.SetTrigger("Continue");
                    Lvlloader.LoadLevelAtIndex(3); // Go to gameplay
                    break;
                case 4:
                    manager.Play("Continue");
                    Lvlloader.LoadLevelAtIndex(1); // Return to main menu
                    break;
                case 5:
                    manager.Play("Continue");
                    Lvlloader.LoadLevelAtIndex(1); // Return to main menu
                    break;
            }
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            switch (SceneManager.GetActiveScene().buildIndex) 
            {
                case 1:
                    Mainmenuexit.SetTrigger("EscapePressed");
                    Application.Quit(); // Quit application
                    break;
                case 2:
                    Mainmenuexit.SetTrigger("Return");
                    Lvlloader.LoadLevelAtIndex(1); // Return to main menu
                    break;
            }
        }
    }
}
