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


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                Mainmenuenter.SetTrigger("EnterPressed");
                Lvlloader.LoadLevelAtIndex(2);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                Lvlloader.LoadLevelAtIndex(3);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 5) 
            {
                Lvlloader.LoadLevelAtIndex(1);
            }
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                Mainmenuexit.SetTrigger("EscapePressed");
                Application.Quit();
            }
        }
    }

    public void  resume_Game(bool Unimportant)
    {
        GameCon.setPause();
    }

}
