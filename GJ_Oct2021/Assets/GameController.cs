using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int keys = 0;
    public int pebbles = 0;
    public bool IsPaused = false;
    public List<GameObject> Monsters = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool setPause()
    {
        IsPaused = !IsPaused;
        return IsPaused;
    }

    public bool getPause()
    {
        return IsPaused;
    }

    public int setKeys(int NewNum)
    {
        keys = NewNum;
        return keys;
    }

    public int setPebbles(int NewNum)
    {
        pebbles = NewNum;
        return pebbles;
    }

    public int getKeys()
    {
        return keys;
    }

    public int getPebbles()
    {
        return pebbles;
    }


}
