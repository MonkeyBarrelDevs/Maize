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
    PlayerController playerController;
    AudioManager manager;
    bool isDying = false;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] LevelLoader loader;
    [SerializeField] float deathHoldTimer = 2.5f;

    void Start()
    {
        FindReferences();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            manager.Play("Resume");
            pauseMenu.SetActive(Ispaused);
        }
    }

    public void TogglePause()
    {
        Ispaused = !Ispaused;
        AudioListener.volume = (Ispaused ? 0f : 1f);
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
        // Debug.Log("Win");
        loader.LoadLevelAtIndex(5);
    }

    public void DeathEvent()
    {
        // Debug.Log("you died lol");
        Ispaused = true;
        if (!isDying)
            isDying = true;
            StartCoroutine(DeathEvent(deathHoldTimer));
    }

    IEnumerator DeathEvent(float delay) 
    {
        playerController.Die();
        yield return new WaitForSeconds(delay);
        loader.LoadLevelAtIndex(4);
    }

    void FindReferences() 
    {
        playerController = Player.GetComponent<PlayerController>();
        manager = GetComponent<AudioManager>();
    }
}
