using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Keys = 0;
    public int Pebbles = 0;
    public int Ammo = 3;
    public bool Ispaused = false;
    public List<GameObject> Monsters = new List<GameObject>();
    public GameObject Player;
    private PlayerController playerController;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] LevelLoader loader;
    [SerializeField] AudioListener listener;

    void Start()
    {
        playerController = Player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(togglePause());
        }
    }

    public bool togglePause()
    {
        Ispaused = !Ispaused;
        AudioListener.volume = (Ispaused ? 0f : 1f);
        return Ispaused;
    }

    public int setAmmo(int NewNum)
    {
        Ammo = NewNum;
        return Ammo;
    }

    public int setKeys(int NewNum)
    {
        Keys = NewNum;
        return Keys;
    }

    public int setPebbles(int NewNum)
    {
        Pebbles = NewNum;
        return Pebbles;
    }

    public bool getPause()
    {
        return Ispaused;
    }

    public int getAmmo()
    {
        return Ammo;
    }

    public int getKeys()
    {
        return Keys;
    }

    public int getPebbles()
    {
        return Pebbles;
    }

    public void VictoryEvent()
    {
        Debug.Log("Win");
        loader.LoadLevelAtIndex(5);
    }

    public void DeathEvent()
    {
        Debug.Log("you died lol");
        Ispaused = true;
        playerController.Die();
        loader.LoadLevelAtIndex(4);
    }
}
